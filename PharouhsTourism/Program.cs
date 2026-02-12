using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PharouhsTourism.Extensions;
using PharouhsTourism.Infrastructure;
using System.Text;

namespace PharouhsTourism
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Correct declaration of the builder variable
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // If you intend to call AddSwaggerGen make sure Swashbuckle.AspNetCore is referenced
            // and the appropriate using/assembly reference is present.

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options
                .UseLazyLoadingProxies()
                .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            // set the implementation of the rate limiter.
            builder.Services.AddRateLimiter(options =>
            {
                options.AddFixedWindowLimiter("FixedWindowPolicy", opt =>
                {
                    opt.Window = TimeSpan.FromSeconds(5);
                    opt.PermitLimit = 5;
                    opt.QueueLimit = 10;
                    opt.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
                }).RejectionStatusCode = 429;
            });


            // add authentication
            
            builder.Services.AddAuthentication(options =>
            {
                // check jwt token header 
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

                //[authorize]
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // unauthorize
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => // verified key
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:securityKey"]))
                };
            });
            
            // register services
            builder.Services.InjectServices();



            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                 app.UseSwagger();
                 app.UseSwaggerUI();
            }
            app.UseRateLimiter();

            using (var scope = app.Services.CreateScope())
            {
                await DataSeeder.Seed(scope.ServiceProvider);
            }

            //app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}

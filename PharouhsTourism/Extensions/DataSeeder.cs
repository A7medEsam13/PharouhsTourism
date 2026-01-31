using Microsoft.AspNetCore.Identity;

namespace PharouhsTourism.Extensions
{
    public class DataSeeder
    {
        public static async Task Seed(IServiceProvider service)
        {
            var userManager = service.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

            var email = "admin@gmail.com";
            var userName = "admin";
            var pass = "Admin@123";


            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });


            var adminUser = await userManager.FindByEmailAsync(email);
            if (adminUser is null)
            {
                var admin = new IdentityUser
                {
                    UserName = userName,
                    Email = email
                };

                var result = await userManager.CreateAsync(admin, pass);

                if (result.Succeeded)
                    await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}

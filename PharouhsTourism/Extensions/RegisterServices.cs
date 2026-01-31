using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PharouhsTourism.Application.Interfaces;
using PharouhsTourism.Application.Services;
using PharouhsTourism.Domain.Interfaces;
using PharouhsTourism.Infrastructure;
using PharouhsTourism.Infrastructure.Repositories;
using System.Reflection.Metadata.Ecma335;

namespace PharouhsTourism.Extensions
{
    public static class RegisterServices
    {
        extension( IServiceCollection services)
        {
            public void InjectServices()
            {
                // register repositories.
                services.AddScoped<ICustomerRepository, CustomerRepository>();
                services.AddScoped<IHotelRepository, HotelRepository>();
                services.AddScoped<IHoneymoonRepository, HoneymoonRepository>();
                services.AddScoped<ITripRepository, TripRepository>();
                services.AddScoped<IBookRepository, BookRepository>();
                services.AddScoped<IDestinationRepository, DestinationRepository>();
                services.AddScoped<IUnitOfWork, UnitOfWork>();


                // register services.
                services.AddScoped<IBookServices, BookServices>();
                services.AddScoped<IHotelServices, HotelServices>();
                services.AddScoped<IHoneymoonServices, HoneymoonServices>();
                services.AddScoped<ITripServices, TripServices>();
                services.AddScoped<IDestinationServices, DestinationServices>();
                
            }

            

            

        }
    }
}

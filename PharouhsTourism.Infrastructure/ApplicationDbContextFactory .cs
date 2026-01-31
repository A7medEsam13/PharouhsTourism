using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace PharouhsTourism.Infrastructure
{
    internal class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(
                    "Server=.;Database=PharouhsTourismDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
                );

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    
    }
}

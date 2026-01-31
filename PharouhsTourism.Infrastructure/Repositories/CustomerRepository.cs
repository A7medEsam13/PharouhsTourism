using PharouhsTourism.Domain.Entities;
using PharouhsTourism.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PharouhsTourism.Infrastructure.Repositories
{
    public class CustomerRepository : GenericRepository<Customer> , ICustomerRepository 
    {
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
}

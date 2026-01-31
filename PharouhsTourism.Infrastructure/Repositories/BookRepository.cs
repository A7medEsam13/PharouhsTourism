using Microsoft.EntityFrameworkCore;
using PharouhsTourism.Domain.Entities;
using PharouhsTourism.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PharouhsTourism.Infrastructure.Repositories
{
    public class BookRepository : ToursRepository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext context) : base(context)
        {
            
        }

       
    }
}

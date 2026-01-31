using PharouhsTourism.Domain.Entities;
using PharouhsTourism.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PharouhsTourism.Infrastructure.Repositories
{
    public class HoneymoonRepository : ToursRepository<Honeymoon>, IHoneymoonRepository
    {
        public HoneymoonRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

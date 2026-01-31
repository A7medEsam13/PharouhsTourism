using Microsoft.EntityFrameworkCore;
using PharouhsTourism.Domain.Entities;
using PharouhsTourism.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PharouhsTourism.Infrastructure.Repositories
{
    public class ToursRepository<T> : GenericRepository<T>, IToursRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public ToursRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<T>> GetAllbyDestination(int destinationId)
        {
            return await _context.Set<T>()
                .Where(e => EF.Property<int>(e, "DestinationId") == destinationId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<T>> GetAllIncludeDestination()
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .Include(e=>EF.Property<Object>(e,"Destination"))
                .ToListAsync();
        }

        public  async Task<T> GetByIDIncludeDestination(int id)
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .Where(e => EF.Property<int>(e, "Id") == id)
                .FirstOrDefaultAsync();
        }
    }
}

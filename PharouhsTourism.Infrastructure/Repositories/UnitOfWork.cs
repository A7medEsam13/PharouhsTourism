using PharouhsTourism.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PharouhsTourism.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Destinations = new DestinationRepository(_context);
            Hotels = new HotelRepository(_context);
            Honeymoons = new HoneymoonRepository(_context);
            Trips = new TripRepository(_context);
            Books = new BookRepository(_context);
            Customers = new CustomerRepository(_context);
        }



        public IDestinationRepository Destinations { get; }

        public IHotelRepository Hotels { get; }

        public IHoneymoonRepository Honeymoons { get; }

        public ITripRepository Trips {  get; }

        public IBookRepository Books { get; }

        public ICustomerRepository Customers { get; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
                    
        }
    }
}

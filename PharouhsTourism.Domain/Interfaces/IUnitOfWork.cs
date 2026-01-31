using System;
using System.Collections.Generic;
using System.Text;

namespace PharouhsTourism.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IDestinationRepository Destinations { get; }
        IHotelRepository Hotels { get; }
        IHoneymoonRepository Honeymoons { get; }
        ITripRepository Trips { get; }
        IBookRepository Books { get; }
        ICustomerRepository Customers { get; }
        Task<int> CompleteAsync();
    }
}

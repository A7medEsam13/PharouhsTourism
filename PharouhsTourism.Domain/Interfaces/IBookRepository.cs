using PharouhsTourism.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PharouhsTourism.Domain.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>, IToursRepository<Book>
    {
        
    }
}

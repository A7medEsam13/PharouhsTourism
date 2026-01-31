using PharouhsTourism.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PharouhsTourism.Domain.Interfaces
{
    public interface IHoneymoonRepository : IGenericRepository<Honeymoon>, IToursRepository<Honeymoon>
    {
    }
}

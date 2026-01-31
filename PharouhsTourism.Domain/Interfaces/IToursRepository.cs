using System;
using System.Collections.Generic;
using System.Text;

namespace PharouhsTourism.Domain.Interfaces
{
    public interface IToursRepository<T> where T : class
    {
        public Task<List<T>> GetAllbyDestination(int destinationId);
        public Task<List<T>> GetAllIncludeDestination();
        public Task<T> GetByIDIncludeDestination(int id);
    }
}

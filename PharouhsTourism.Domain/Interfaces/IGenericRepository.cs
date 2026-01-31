using System;
using System.Collections.Generic;
using System.Text;

namespace PharouhsTourism.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<List<T>> GetAll();
        public Task<T> GetById(int id);
        public Task<T> Add(T entity);
        public Task<T> Update(T entity);
        public Task Delete(int id);

    }
}

using PharouhsTourism.Application.DTOs.Honeymoon;
using System;
using System.Collections.Generic;
using System.Text;

namespace PharouhsTourism.Application.Interfaces
{
    public interface IHoneymoonServices
    {
        public Task Add(AddHoneymoonDto dto, List<string> photosPaths);
        public Task Update(int id, DisplayHoneymoonDto dto);
        public Task Delete(int id);
        public Task<List<DisplayHoneymoonDto>> GetAllDestinationhoneymoons(int destinationId);
        public Task<DisplayHoneymoonDto> GetById(int id);
    }
}

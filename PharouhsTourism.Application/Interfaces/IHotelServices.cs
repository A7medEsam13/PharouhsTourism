using PharouhsTourism.Application.DTOs.Hotel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PharouhsTourism.Application.Interfaces
{
    public interface IHotelServices
    {
        public Task Add(AddHotelDto dto, List<string> photosPaths);
        public Task Update(int id, DisplayHotelDto dto);
        public Task Delete(int id);
        public Task<List<DisplayHotelDto>> GetAllDestinationHotels(int destinationId);
        public Task<DisplayHotelDto> GetById(int id);
        public Task<ICollection<DisplayHotelDto>> GetAll();
    }
}

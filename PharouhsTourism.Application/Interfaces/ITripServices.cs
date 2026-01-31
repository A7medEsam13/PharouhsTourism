using PharouhsTourism.Application.DTOs.Trip;
using System;
using System.Collections.Generic;
using System.Text;

namespace PharouhsTourism.Application.Interfaces
{
    public interface ITripServices
    {
        public Task Add(AddTripDto dto, List<string> photosPaths);
        public Task Update(int id, DisplayTripDto dto);
        public Task Delete(int id);
        public Task<List<DisplayTripDto>> GetAllDestinationTrips(int destinationId);
        public Task<DisplayTripDto> GetById(int id);    
    }
}

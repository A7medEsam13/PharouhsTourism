using PharouhsTourism.Application.DTOs.Destination;
using System;
using System.Collections.Generic;
using System.Text;

namespace PharouhsTourism.Application.Interfaces
{
    public interface IDestinationServices
    {
        public Task Add(AddDestinationDto dto);
        public Task Delete(int id);
        public Task Update(int id, AddDestinationDto dto);
        public Task<DisplayDestinationDto> GetById(int id);
    }
}

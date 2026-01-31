using PharouhsTourism.Application.DTOs.Destination;
using PharouhsTourism.Application.Interfaces;
using PharouhsTourism.Domain.Entities;
using PharouhsTourism.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PharouhsTourism.Application.Services
{
    public class DestinationServices : IDestinationServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public DestinationServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Add(AddDestinationDto dto)
        {
            var destination = new Destination
            {
                Name = dto.Name,
                PhotoURL = dto.PhotoURL
            };

            await _unitOfWork.Destinations.Add(destination);
            await _unitOfWork.CompleteAsync();
        }

        public async Task Delete(int id)
        {
            await _unitOfWork.Destinations.Delete(id);
            await _unitOfWork.CompleteAsync();
        }

        

        public async Task<DisplayDestinationDto> GetById(int id)
        {
            var dest = await _unitOfWork.Destinations.GetById(id);

            var destinationHoneymonns = await _unitOfWork.Honeymoons.GetAllbyDestination(id);
            var numsOfhoneymonns = destinationHoneymonns.Count;

            var destinationHotels = await _unitOfWork.Hotels.GetAllbyDestination(id);
            var numsOfHotels = destinationHotels.Count;

            var destinationTrips = await _unitOfWork.Trips.GetAllbyDestination(id);
            var numsOfTips = destinationTrips.Count;

            var result = new DisplayDestinationDto
            {
                Name = dest.Name,
                PhotoURL = dest.PhotoURL,
                NumberOfHoneymoons = numsOfhoneymonns,
                NumberOfHotels = numsOfHotels,
                NumberOfTrips = numsOfTips
            };

            return result;
        }

        public async Task Update(int id, AddDestinationDto dto)
        {
            var dest = await _unitOfWork.Destinations.GetById(id);


            await _unitOfWork.Destinations.Update(dest);
            await _unitOfWork.CompleteAsync();
        }
    }
}

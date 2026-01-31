using PharouhsTourism.Application.DTOs.Trip;
using PharouhsTourism.Application.Interfaces;
using PharouhsTourism.Domain.Entities;
using PharouhsTourism.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PharouhsTourism.Application.Services
{
    public class TripServices : ITripServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public TripServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Add(AddTripDto dto, List<string> photosPaths)
        {
            var trip = new Trip
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                DestinationId = dto.DestinationId,
                PhotosURL = photosPaths
            };
            await _unitOfWork.Trips.Add(trip);
            await _unitOfWork.CompleteAsync();
        }

        public async Task Delete(int id)
        {
            await _unitOfWork.Trips.Delete(id);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<DisplayTripDto>> GetAllDestinationTrips(int destinationId)
        {
            var trips = await _unitOfWork.Trips.GetAllbyDestination(destinationId);
            var result = trips.Select(t => new DisplayTripDto
            {
                Description = t.Description,
                Price = t.Price,
                Rate = t.Rate,
                DestinationName = t.Destination.Name,
                Name = t.Name
            });

            return result.ToList();
        }

        public async Task<DisplayTripDto> GetById(int id)
        {
            var trip = await _unitOfWork.Trips.GetById(id);
            var dto = new DisplayTripDto
            {
                Name = trip.Name,
                Description = trip.Description,
                DestinationName = trip.Destination.Name,
                Price = trip.Price,
                Rate = trip.Rate
            };

            return dto;
        }

        public async Task Update(int id, DisplayTripDto dto)
        {
            var trip = await _unitOfWork.Trips.GetById(id);
            trip.Description = dto.Description;
            trip.Price = dto.Price;
            trip.Rate = dto.Rate;

            await _unitOfWork.Trips.Update(trip);
            await _unitOfWork.CompleteAsync();
        }
    }
}

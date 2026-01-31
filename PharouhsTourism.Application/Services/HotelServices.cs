using PharouhsTourism.Application.DTOs.Hotel;
using PharouhsTourism.Application.Interfaces;
using PharouhsTourism.Domain.Entities;
using PharouhsTourism.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PharouhsTourism.Application.Services
{
    public class HotelServices : IHotelServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public HotelServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Add(AddHotelDto dto, List<string> photosPaths)
        {
            var hotel = new Hotel
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                PhotosURL = photosPaths,
                DestinationId = dto.DestinationId
            };

            await _unitOfWork.Hotels.Add(hotel);
            await _unitOfWork.CompleteAsync();
        }

        public async Task Delete(int id)
        {
            await _unitOfWork.Hotels.Delete(id);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<ICollection<DisplayHotelDto>> GetAll()
        {
            var hotels = await _unitOfWork.Hotels.GetAllIncludeDestination();
            var result = hotels.Select(h => new DisplayHotelDto
            {
                Description = h.Description,
                Price = h.Price,
                PhotosURL = h.PhotosURL,
                DestinationName = h.Destination.Name,
                Name = h.Name
            }).ToList();

            return result;
            
        }

        public async Task<List<DisplayHotelDto>> GetAllDestinationHotels(int destinationId)
        {
            var hotels = await _unitOfWork.Hotels.GetAllbyDestination(destinationId);
            var result = hotels.Select(h => new DisplayHotelDto
            {
                Description = h.Description,
                Price = h.Price,
                PhotosURL = h.PhotosURL,
                DestinationName = h.Destination.Name,
                Name = h.Name
            }).ToList();

            return result;
        }

        public async Task<DisplayHotelDto> GetById(int id)
        {
            var hotel = await _unitOfWork.Hotels.GetByIDIncludeDestination(id);

            var result = new DisplayHotelDto
            {
                Description = hotel.Description,
                Price = hotel.Price,
                PhotosURL = hotel.PhotosURL,
                DestinationName = hotel.Destination.Name,
                Name = hotel.Name
            };

            return result;
        }

        public async Task Update(int id, DisplayHotelDto dto)
        {
            var hotel = await _unitOfWork.Hotels.GetById(id);
            hotel.Description = dto.Description;
            hotel.Price = dto.Price;
            hotel.PhotosURL = dto.PhotosURL;
            hotel.Name = dto.Name;

            await _unitOfWork.Hotels.Update(hotel);

            await _unitOfWork.CompleteAsync();
        }
    }
}
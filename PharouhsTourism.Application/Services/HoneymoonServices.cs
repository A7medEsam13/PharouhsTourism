using PharouhsTourism.Application.DTOs.Honeymoon;
using PharouhsTourism.Application.Interfaces;
using PharouhsTourism.Domain.Entities;
using PharouhsTourism.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PharouhsTourism.Application.Services
{
    public class HoneymoonServices : IHoneymoonServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public HoneymoonServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Add(AddHoneymoonDto dto, List<string> photosPaths)
        {
            var honeymoon = new Honeymoon
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                DestinationId = dto.DestinationId,
                PhotosURL = photosPaths
            };

            await _unitOfWork.Honeymoons.Add(honeymoon);
            await _unitOfWork.CompleteAsync();
        }

        public async Task Delete(int id)
        {
            await _unitOfWork.Honeymoons.Delete(id);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<DisplayHoneymoonDto>> GetAllDestinationhoneymoons(int destinationId)
        {
            var honeymoons = await _unitOfWork.Honeymoons.GetAllbyDestination(destinationId);

            var results = honeymoons.Select(h => new DisplayHoneymoonDto
            {
                Name = h.Name,
                Description = h.Description,
                Price = h.Price,
                Rate = h.Rate,
                DestinationName = h.Destination.Name
            }).ToList();

            return results;
        }

        public async Task<DisplayHoneymoonDto> GetById(int id)
        {
            var honeymoon = await _unitOfWork.Honeymoons.GetById(id);

            var result = new DisplayHoneymoonDto
            {
                Name = honeymoon.Name,
                Description = honeymoon.Description,
                Price = honeymoon.Price,
                Rate = honeymoon.Rate,
                DestinationName = honeymoon.Destination.Name
            };

            return result;
        }

        public async Task Update(int id, DisplayHoneymoonDto dto)
        {
            var honeymoon = await _unitOfWork.Honeymoons.GetById(id);
            honeymoon.Description = dto.Description;
            honeymoon.Price = dto.Price;
            honeymoon.Rate = dto.Rate;

            await _unitOfWork.Honeymoons.Update(honeymoon);
            await _unitOfWork.CompleteAsync();
        }
    }
}

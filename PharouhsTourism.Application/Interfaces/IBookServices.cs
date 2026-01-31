using PharouhsTourism.Application.DTOs.Book;
using System;
using System.Collections.Generic;
using System.Text;

namespace PharouhsTourism.Application.Interfaces
{
    public interface IBookServices
    {

        public Task AddBook(int? hotelId, int? tripId, int? honeymoonId, AddBookDto dto);
        public Task UpdateBook(int id, AddBookDto dto);
        public Task DeleteBook(int id);
        public Task<DisplayBookDto> GetByID(int id);
        public Task<List<DisplayBookDto>> GetAllDestinationBooks(int destinationId);
    }
}

using PharouhsTourism.Application.DTOs.Book;
using PharouhsTourism.Application.Interfaces;
using PharouhsTourism.Domain.Entities;
using PharouhsTourism.Domain.Interfaces;

namespace PharouhsTourism.Application.Services
{
    public class BookServices : IBookServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddBook(int? hotelId, int? tripId, int? honeymoonId, AddBookDto dto)
        {
            decimal total;
            int diffDays = dto.CheckOut.DayNumber - dto.CheckIn.DayNumber;
            int destId;
            if (hotelId != null)
            {
                var hotel = await _unitOfWork.Hotels.GetById(hotelId.Value);
                total = (diffDays) * hotel.Price;
                destId = hotel.DestinationId;
            }
            else if(tripId != null)
            {
                var trip = await _unitOfWork.Trips.GetById(tripId.Value);
                total = (diffDays) * trip.Price;
                destId = trip.DestinationId;
            }
            else
            {
                var honeymoon = await _unitOfWork.Honeymoons.GetById(honeymoonId.Value);
                total = (diffDays) * honeymoon.Price;
                destId= honeymoon.DestinationId;
            }


            var book = new Book
            {
                CheckIn = dto.CheckIn,
                CheckOut = dto.CheckOut,
                Children = dto.ChildrenNumber,
                RoomType = dto.RoomType,
                Notes = dto.Notes,
                Payment = dto.Payment,
                HotelId = hotelId,
                TripId = tripId,
                HoneyMoonId = honeymoonId,
                Total = total,
                Nights = diffDays,
                DestinationId = destId
            };


            var customer = new Customer
            {
                Email = dto.Email,
                Phone = dto.Phone,
                Name = dto.CustomerFullname
            };

            await _unitOfWork.Books.Add(book);
            await _unitOfWork.Customers.Add(customer);

            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteBook(int id)
        {
            await _unitOfWork.Books.Delete(id);

            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<DisplayBookDto>> GetAllDestinationBooks(int destinationId)
        {
            var books = await _unitOfWork.Books.GetAllbyDestination(destinationId);

            var result = books.Select(b => new DisplayBookDto
            {
                Id = b.Id,
                Children = b.Children,
                Total = b.Total,
                Nights = b.Nights,
                RoomType = b.RoomType,
                CheckIn = b.CheckIn,
                CheckOut = b.CheckOut,
                IsPaied = b.Payment,
                HoneyMoonName = b.HoneyMoon?.Name?? "unknown",
                HotelName = b.Hotel?.Name ?? "unknown",
                TripName = b.Trip?.Name ?? "unknown"
            }).ToList();

            return result;
        }

        public async Task<DisplayBookDto> GetByID(int id)
        {
            var book = await _unitOfWork.Books.GetById(id);

            var result = new DisplayBookDto
            {
                Id = book.Id,
                Children = book.Children,
                Total = book.Total,
                Nights = book.Nights,
                RoomType = book.RoomType,
                CheckIn = book.CheckIn,
                CheckOut = book.CheckOut,
                IsPaied = book.Payment,
                HoneyMoonName = book.HoneyMoon?.Name ?? "unknown",
                HotelName = book.Hotel?.Name ?? "unknown",
                TripName = book.Trip?.Name ?? "unknown"
            };

            return result;
        }

        public async Task UpdateBook(int id, AddBookDto dto)
        {
            var book = await _unitOfWork.Books.GetById(id);

            book.CheckOut = dto.CheckOut;
            book.CheckIn = dto.CheckIn;
            book.Children = dto.ChildrenNumber;
            book.RoomType = dto.RoomType;

            int diffDays = dto.CheckOut.DayNumber - dto.CheckIn.DayNumber;
            book.Nights = diffDays;

            if (book.HotelId != null)
            {
                var hotel = await _unitOfWork.Hotels.GetById(book.HotelId.Value);
                book.Total = (diffDays) * hotel.Price;
            }
            else if (book.TripId != null)
            {
                var trip = await _unitOfWork.Trips.GetById(book.TripId.Value);
                book.Total = (diffDays) * trip.Price;
            }
            else
            {
                var honeymoon = await _unitOfWork.Honeymoons.GetById(book.HoneyMoonId.Value);
                book.Total = (diffDays) * honeymoon.Price;
            }

            await _unitOfWork.Books.Update(book);
            await _unitOfWork.CompleteAsync();
        }
    }
}

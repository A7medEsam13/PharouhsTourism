using PharouhsTourism.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PharouhsTourism.Application.DTOs.Book
{
    public class DisplayBookDto
    {
        public int Id { get; set; }
        public int Children { get; set; }
        public decimal Total { get; set; }
        public int Nights { get; set; } 
        public RoomType RoomType { get; set; }
        public DateOnly CheckIn { get; set; }
        public DateOnly CheckOut { get; set; }
        public bool IsPaied { get; set; }

        public string? HotelName { get; set; }
        public string? TripName { get; set; }
        public string? HoneyMoonName { get; set; }
    }
}

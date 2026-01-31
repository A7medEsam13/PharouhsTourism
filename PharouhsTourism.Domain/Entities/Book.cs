using PharouhsTourism.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PharouhsTourism.Domain.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public int Children { get; set; }
        public string Notes { get; set; }
        public decimal Total { get; set; } = 0;
        public int Nights { get; set; } = 0;
        public RoomType RoomType { get; set; }
        public DateOnly CheckIn { get; set; }
        public DateOnly CheckOut { get; set; }
        public bool Payment { get; set; }

        public int DestinationId { get; set; }
        public int? HotelId { get; set; }
        public int? TripId { get; set; }
        public int? HoneyMoonId { get; set; }


        public virtual Hotel? Hotel { get; set; }
        public virtual Trip? Trip { get; set; }
        public virtual Honeymoon? HoneyMoon { get; set; }
        public virtual Destination Destination { get; set; }
    }
}

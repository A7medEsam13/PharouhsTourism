using System;
using System.Collections.Generic;
using System.Text;

namespace PharouhsTourism.Application.DTOs.Hotel
{
    public class DisplayHotelDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string DestinationName { get; set; }

        public ICollection<string> PhotosURL { get; set; }
    }
}

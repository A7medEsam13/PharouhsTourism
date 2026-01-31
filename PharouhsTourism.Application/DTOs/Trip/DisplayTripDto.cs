using System;
using System.Collections.Generic;
using System.Text;

namespace PharouhsTourism.Application.DTOs.Trip
{
    public class DisplayTripDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string DestinationName { get; set; }
        public decimal Rate { get; set; }
    }
}

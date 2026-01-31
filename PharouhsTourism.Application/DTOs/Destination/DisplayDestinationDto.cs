using System;
using System.Collections.Generic;
using System.Text;

namespace PharouhsTourism.Application.DTOs.Destination
{
    public class DisplayDestinationDto
    {
        public string Name { get; set; }
        public string PhotoURL { get; set; }
        public int NumberOfHotels { get; set; }
        public int NumberOfTrips { get; set; }
        public int NumberOfHoneymoons { get; set; }
    }
}

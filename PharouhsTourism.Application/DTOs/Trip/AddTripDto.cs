using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PharouhsTourism.Application.DTOs.Trip
{
    public class AddTripDto
    {
        [Required]
        [MaxLength(40,ErrorMessage = "Name length must be less than 40 characters")]
        public string Name { get; set; }
        [Required]
        [MaxLength(650,ErrorMessage = "Max Length for Description is 650 characters")]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int DestinationId { get; set; }
    }
}

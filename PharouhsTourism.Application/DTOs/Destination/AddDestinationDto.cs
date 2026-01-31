using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PharouhsTourism.Application.DTOs.Destination
{
    public class AddDestinationDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string PhotoURL { get; set; }
    }
}

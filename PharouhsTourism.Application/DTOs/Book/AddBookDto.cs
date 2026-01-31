using PharouhsTourism.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PharouhsTourism.Application.DTOs.Book
{
    public class AddBookDto
    {
        [Required]
        [DataType(DataType.Date)]
        public DateOnly CheckIn { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly CheckOut { get; set; }
        [Required]
        public int ChildrenNumber { get; set; }
        [Required]
        public RoomType RoomType { get; set; }
        [Required]
        [MaxLength(650)]
        public string? Notes { get; set; }
        [Required]
        public string CustomerFullname { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required]        
        public bool Payment { get; set; }
    }
}

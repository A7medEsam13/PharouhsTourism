using System;
using System.Collections.Generic;
using System.Text;

namespace PharouhsTourism.Application.DTOs.Customer
{
    public class AddCustomerDto
    {
        public string Name { get; set; }
        public string? Email { get; set; }
        public string Phone { get; set; }
    }
}

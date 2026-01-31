using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PharouhsTourism.Domain.Entities
{
    public class Destination
    {
        public Destination()
        {
            Hotels = new HashSet<Hotel>();
            Trips = new HashSet<Trip>();
            Honeymones = new HashSet<Honeymoon>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhotoURL { get; set; }
        public virtual ICollection<Hotel> Hotels { get; set; }
        public virtual ICollection<Trip> Trips { get; set; }
        public virtual ICollection<Honeymoon> Honeymones { get; set; }

    }
}

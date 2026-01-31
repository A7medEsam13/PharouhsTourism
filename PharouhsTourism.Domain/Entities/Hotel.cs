using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PharouhsTourism.Domain.Entities
{
    [Table("Hotels")]
    public class Hotel : BaseEntity
    {
        public Hotel() : base()
        {
        }

        public int DestinationId { get; set; }

        [ForeignKey("DestinationId")]
        public virtual Destination Destination { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PharouhsTourism.Domain.Entities
{
    [Table("Trips")]
    public class Trip : BaseEntity
    {
        public Trip() : base()
        {
        }

        public int DestinationId { get; set; }

        [ForeignKey("DestinationId")]
        public virtual Destination Destination { get; set; }
    }
}

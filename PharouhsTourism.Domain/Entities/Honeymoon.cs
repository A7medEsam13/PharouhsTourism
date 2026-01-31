using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PharouhsTourism.Domain.Entities
{
    [Table("Honeymoons")]
    public class Honeymoon : BaseEntity
    {

        public Honeymoon() : base()
        {
            
        }


        public int DestinationId { get; set; }

        [ForeignKey("DestinationId")]
        public virtual Destination Destination { get; set; }
    }
}

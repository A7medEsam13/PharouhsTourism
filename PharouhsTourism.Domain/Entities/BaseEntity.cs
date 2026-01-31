using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PharouhsTourism.Domain.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            PhotosURL = new List<string>();
            Packages = new List<List<string>>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; } = 0;
        public decimal Price { get; set; }

        public ICollection<string> PhotosURL { get; set; }
        public List<List<string>> Packages { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Database.Entity
{
    public class Product
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public int categoryId { get; set; }
        public string categoryName { get; set; }
        public float averageRating { get; set; }
        public int numberOfRaters { get; set; }
    }
}

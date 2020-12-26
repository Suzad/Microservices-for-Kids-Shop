using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Database
{
    public class Rating
    {
        public string product_id { get; set; }
        public float average { get; set; }
        public int count { get; set; }
    }
}

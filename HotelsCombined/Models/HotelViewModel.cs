using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelsCombined.Models
{  
    public class HotelViewModel
    {
        public int ID { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public int NumberOfReviews { get; set; }
        public decimal Rate { get; set; }
        public int StarRating { get; set; }
    }
}

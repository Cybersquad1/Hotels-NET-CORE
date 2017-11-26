using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelsCombined.Models
{
    public class PlaceViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public IEnumerable<HotelViewModel> Hotels { get;set;}
    }
}

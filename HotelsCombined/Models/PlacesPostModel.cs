using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelsCombined.Models
{
    public class PlacesPostModel
    {
        [Required]
        public IEnumerable<string> PlaceNames { get; set; }
    }
}

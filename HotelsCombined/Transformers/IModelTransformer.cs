using HotelsCombined.Entities;
using HotelsCombined.Models;
using System.Collections.Generic;

namespace HotelsCombined.Transformers
{
    public interface IModelTransformer
    {
        PlaceViewModel PlaceToViewModel(Place place);
        HotelViewModel HotelToViewModel(Hotel hotel);
        IList<PlaceViewModel> PlacesToViewModels(IList<Place> places);
    }
}

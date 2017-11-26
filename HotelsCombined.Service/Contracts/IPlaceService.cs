using System.Collections.Generic;
using HotelsCombined.Entities;

namespace HotelsCombined.Service.Contracts
{
    public interface IPlaceService
    {
        IList<Place> GetPlaces(List<string> placeNames, int numberOfHotels, OrderByQuery orderByQuery);
    }
}

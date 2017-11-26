using HotelsCombined.Service.Contracts;
using System;
using HotelsCombined.Entities;
using HotelsCombined.Repository.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace HotelsCombined.Service.Implementations
{
    public class PlaceService : IPlaceService
    {
        private IPlaceRepository _placeRepository;

        public PlaceService(IPlaceRepository placeRepository)
        {
            _placeRepository = placeRepository ?? throw new ArgumentNullException(nameof(placeRepository));
        }

        public IList<Place> GetPlaces(List<string> placeNames, int numberOfHotels, OrderByQuery orderByQuery)
        {
            try
            {
                if (placeNames == null || placeNames.Count() < 1 || numberOfHotels < 1 || !Enum.IsDefined(typeof(OrderByQuery), orderByQuery)) return null;

                IList<Place> places = new List<Place>();

                placeNames.ForEach(placeName =>
                {
                    Place place = _placeRepository.Get(placeName);

                    if (place == null) return;
                    if (orderByQuery == OrderByQuery.Cheapest)
                    {
                        var filteredHotels = place.Hotels.OrderBy(hotel => hotel.Rate).Take(numberOfHotels);
                        place.Hotels = filteredHotels.ToList();
                    }

                    places.Add(place);
                });
                return places;
            }
            catch (Exception ex)
            {
                //TODO: Log error
                return null;
            }
        }
    }
}

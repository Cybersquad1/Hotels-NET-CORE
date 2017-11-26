using HotelsCombined.Entities;
using HotelsCombined.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HotelsCombined.Transformers
{
    public class ModelTransformer: IModelTransformer
    {
        protected readonly IHostingEnvironment IHostingEnvironment;
        public ModelTransformer(IHostingEnvironment iHostingEnvironment)
        {
            IHostingEnvironment = iHostingEnvironment ?? throw new ArgumentNullException(nameof(iHostingEnvironment));
        }

       public PlaceViewModel PlaceToViewModel(Place place)
        {
            if (place == null) return null;
            var placeViewModel = new PlaceViewModel();
            placeViewModel.ID = place.ID;
            placeViewModel.Name = place.Name;

            var hotelViewModels = new List<HotelViewModel>();

            if (place.Hotels == null || place.Hotels.Count < 1) return placeViewModel;
            place.Hotels.ToList().ForEach(hotel =>
            {
                var hotelViewModel = HotelToViewModel(hotel);
                hotelViewModels.Add(hotelViewModel);
            });
            placeViewModel.Hotels = hotelViewModels;

            return placeViewModel;
        }

        public HotelViewModel HotelToViewModel(Hotel hotel)
        {
            if (hotel == null) return null;
            return new HotelViewModel
            {
                ID = hotel.ID,
                Name = hotel.Name,
                Image =  File.Exists(Path.Combine(IHostingEnvironment.ContentRootPath, $"wwwroot\\images\\Hotels\\{hotel.Image}")) ? hotel.Image: string.Empty,
                NumberOfReviews = hotel.NumberOfReviews,
                Rate = hotel.Rate,
                StarRating = hotel.StarRating
            };
        }

        public IList<PlaceViewModel> PlacesToViewModels(IList<Place> places)
        {
            if (places == null) return null;
            //Create new list of PlaceViewModels
            var placeViewModels = new List<PlaceViewModel>();
            //Convert List of Place to List of PlaceViewModels
            places.ToList().ForEach(place =>
            {
                //Convert place to place view model
                var placeViewModel = PlaceToViewModel(place);
                placeViewModels.Add(placeViewModel);
            });
            return placeViewModels;
        }
    }
}

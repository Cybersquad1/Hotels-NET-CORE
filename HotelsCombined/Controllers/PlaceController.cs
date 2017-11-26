using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using HotelsCombined.Models;
using HotelsCombined.Service.Contracts;
using HotelsCombined.Entities;
using HotelsCombined.ActionFilters;
using HotelsCombined.Transformers;

namespace HotelsCombined.Controllers
{
    [Route("[controller]")]
    public class PlaceController : Controller
    {
        protected IPlaceService IPlaceService { get; }
        protected IModelTransformer IModelTransformer { get; }

        public PlaceController(IPlaceService iPlaceService, IModelTransformer iModelTransformer)
        {
            IPlaceService = iPlaceService ?? throw new ArgumentNullException(nameof(iPlaceService));
            IModelTransformer = iModelTransformer ?? throw new ArgumentNullException(nameof(iModelTransformer));
        }

        [HttpPost]
        [ServiceFilter(typeof(PlacesPostModelValidation))]
        [ProducesResponseType(typeof(IEnumerable<PlaceViewModel>), 200)]
        public IActionResult GetPlaces([FromBody]PlacesPostModel placesPostModel)
        {
            //Check if place names was sent from the client
            if (placesPostModel == null || placesPostModel.PlaceNames == null || placesPostModel.PlaceNames.Count() < 1) return new BadRequestObjectResult(ModelState);

            //Get the places information
            var places = IPlaceService.GetPlaces(placesPostModel.PlaceNames.ToList(), 5, OrderByQuery.Cheapest);

            if(places == null || places.Count < 1) return new NoContentResult();

            //Convert Place models to view models
            var placeViewModels = IModelTransformer.PlacesToViewModels(places);

            //Check if converstion returned a view model
            if(placeViewModels == null) return new NoContentResult();

            //Return list of place view models to client
            return new OkObjectResult(placeViewModels);
        }
    }
}
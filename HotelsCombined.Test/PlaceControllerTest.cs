using HotelsCombined.Controllers;
using HotelsCombined.Models;
using HotelsCombined.Repository.Contracts;
using HotelsCombined.Repository.Implementations;
using HotelsCombined.Service.Contracts;
using HotelsCombined.Service.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.IO;
using Xunit;
using System.Collections.Generic;
using HotelsCombined.Transformers;

namespace HotelsCombined.Test
{
    public class PlaceControllerTest
    {
        protected IPlaceService IPlaceService { get; }
        protected IPlaceRepository IPlaceRepository { get; }
        protected IModelTransformer IModelTransformer { get; }
        protected PlaceController PlaceController { get; }
      
        public PlaceControllerTest()
        {
            var hostingEnvironment = new Mock<IHostingEnvironment>();
            var projectRootDirectory = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName).FullName;
            hostingEnvironment.Setup(m => m.ContentRootPath).Returns(projectRootDirectory);
            IPlaceRepository = new DummyPlaceRepository(hostingEnvironment.Object);
            IPlaceService = new PlaceService(IPlaceRepository);
            IModelTransformer = new ModelTransformer(hostingEnvironment.Object);
            PlaceController = new PlaceController(IPlaceService, IModelTransformer);
        }

        public class GetPlaces : PlaceControllerTest
        {
            /* Given:  I have a GetPlaces Place Endpoint and a VALID list of place names
             * When:   I send the place names to it
             * Then:   I should have a result of type OkObjectResult with a list of objects with the value of type PlaceViewModel
             */
            [Fact]
            public void Send_Valid_PlaceName_Should_Return_OkObjectResult_With_A_List_Of_PlaceViewModels()
            {
                // Given
                var placesList = new PlacesPostModel{ PlaceNames = new List<string>() { "New_York_City", "Tokyo" } };

                // When
                var result = PlaceController.GetPlaces(placesList);

                // Then
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.IsType<List<PlaceViewModel>>(okResult.Value);
            }

            /* Given:  I have a GetPlaces Place Endpoint and a list of only 1 VALID place name
             * When:   I send the place names to it
             * Then:   I should have a result of type OkObjectResult with a list of only 1 PlaceViewModel
             */
            [Fact]
            public void Send_Only_1_PlaceName_Should_Return_OkObjectResult_With_A_List_Of_Only_1_PlaceViewModel()
            {
                // Given
                var placesList = new PlacesPostModel { PlaceNames = new List<string>() { "New_York_City", "AussieLand" } };

                // When
                var result = PlaceController.GetPlaces(placesList);

                // Then
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.IsType<List<PlaceViewModel>>(okResult.Value);
                var placeViewModels = (List<PlaceViewModel>)okResult.Value;
                Assert.True(placeViewModels.Count == 1);
            }

            /* Given:  I have a GetPlaces Place Endpoint and a list of only  INVALID place names
             * When:   I send the place names to it
             * Then:   I should have a result of type NoContentResult
             */
            [Fact]
            public void Send_No_Valid_PlaceName_Should_Return_NoContentResult()
            {
                // Given
                var placesList = new PlacesPostModel { PlaceNames = new List<string>() { "KiwiLand", "AussieLand" } };

                // When
                var result = PlaceController.GetPlaces(placesList);

                // Then
                Assert.IsType<NoContentResult>(result);
            }

            /* Given:  I have a GetPlaces Place Endpoint and no place names
             * When:   I send the NULL to it
             * Then:   I should have a result of type BadRequestObjectResult
            */
            [Fact]
            public void Send_Null_Should_Return_NoContentResult()
            {
                // When
                var result = PlaceController.GetPlaces(null);

                // Then
                Assert.IsType<BadRequestObjectResult>(result);
            }
        }
    }
}

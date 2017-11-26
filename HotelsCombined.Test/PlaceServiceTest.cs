using HotelsCombined.Entities;
using HotelsCombined.Repository.Contracts;
using HotelsCombined.Repository.Implementations;
using HotelsCombined.Service.Contracts;
using HotelsCombined.Service.Implementations;
using Microsoft.AspNetCore.Hosting;
using Moq;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace HotelsCombined.Test
{
    public class PlaceServiceTest
    {
        protected IPlaceService IPlaceService { get; }
        protected IPlaceRepository IPlaceRepository { get; }
        public PlaceServiceTest()
        {
            var env = new Mock<IHostingEnvironment>();
            var projectRootDirectory = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName).FullName;
            env.Setup(m => m.ContentRootPath).Returns(projectRootDirectory);
            IPlaceRepository = new DummyPlaceRepository(env.Object);
            IPlaceService = new PlaceService(IPlaceRepository);
        }

        public class GetPlaces : PlaceServiceTest
        {
            /* Given:  I have a Place Service GetPlaces method and NO place names to send,
             *         but a VALID number of hotels and orderbyquery enum
             * When:   I send the parameters to it
             * Then:   I should receive NULL
             */
            [Fact]
            public void Invalid_List_Of_Place_Names_Of_Type_Null_Returns_Null()
            {
                // Given 
                int numberOfHotels = 5;
                OrderByQuery orderByQuery = OrderByQuery.Cheapest;
                // When
                var result = IPlaceService.GetPlaces(null, numberOfHotels, orderByQuery);
                // Then
                Assert.Null(result);
            }
            /* Given:  I have a Place Service GetPlaces method and a list of INVALID place names,
             *         but a VALID number of hotels and orderbyquery enum
             * When:   I send parameters to it
             * Then:   I should receive an EMPTY list of places
             */
            [Fact]
            public void Invalid_List_Of_Place_Names_Returns_Empty_List_Of_Type_Person()
            {
                // Given 
                var placeNames = new List<string> { "Unite Saffer", "North Africa" };
                int numberOfHotels = 5;
                OrderByQuery orderByQuery = OrderByQuery.Cheapest;
                // When
                var result = IPlaceService.GetPlaces(placeNames, numberOfHotels, orderByQuery);
                // Then
                Assert.IsType<List<Place>>(result);
                Assert.True(result.Count == 0);
            }
            /* Given:  I have a Place Service GetPlaces method and a list with only 1 VALID place name,
             *         but a VALID number of hotels and orderbyquery enum
             * When:   I send parameters to it
             * Then:   I should receive an list of only 1 place
            */
            [Fact]
            public void List_Of_Only_1_Valid_Place_Name_Returns_List_Of_Only_1_Place_Object()
            {
                // Given 
                var placeNames = new List<string> { "Unite Saffer", "Tokyo" };
                int numberOfHotels = 5;
                OrderByQuery orderByQuery = OrderByQuery.Cheapest;
                // When
                var result = IPlaceService.GetPlaces(placeNames, numberOfHotels, orderByQuery);
                // Then
                Assert.IsType<List<Place>>(result);
                Assert.True(result.Count == 1);
            }
            /* Given:  I have a Place Service GetPlaces method and an INVALID number of hotels,
             *         but a VALID list of place names and orderbyquery enum
             * When:   I send parameters to it
             * Then:   I should receive NULL
             */
            [Fact]
            public void Invalid_Number_Of_Hotels_Returns_Null()
            {
                //Given
                var placeNames = new List<string> { "New_York_City", "Tokyo" };
                int numberOfHotels = 0;
                OrderByQuery orderByQuery = OrderByQuery.Cheapest;
                // When
                var result = IPlaceService.GetPlaces(placeNames, numberOfHotels, orderByQuery);
                // Then
                Assert.Null(result);
            }
            /* Given:  I have a Place Service GetPlaces method and VALID parameters,
             * When:   I send them to it
             * Then:   I should receive a list of Place objects
             */
            [Fact]
            public void Valid_Params_Returns_List_Of_Places()
            {
                //Given
                var placeNames = new List<string> { "New_York_City", "Tokyo" };
                int numberOfHotels = 5;
                OrderByQuery orderByQuery = OrderByQuery.Cheapest;
                // When
                var result = IPlaceService.GetPlaces(placeNames, numberOfHotels, orderByQuery);
                // Then
                Assert.IsType<List<Place>>(result);
                Assert.True(result.Count > 0);
            }
            /* Given:  I have a Place Service GetPlaces method and VALID parameters,
             * When:   I send them to it
             * Then:   I should receive a list of Place objects, each with a specified number of ordered Hotels 
             */
            [Fact]
            public void Valid_Params_Returns_List_Of_Places_With_Specified_Number_Of_Ordered_Hotels()
            {
                //Given
                var placeNames = new List<string> { "New_York_City", "Tokyo" };
                int numberOfHotels = 5;
                OrderByQuery orderByQuery = OrderByQuery.Cheapest;
                // When
                var result = IPlaceService.GetPlaces(placeNames, numberOfHotels, orderByQuery);
                // Then
                Assert.All(result, place =>
                {
                    Assert.True(place.Hotels.Count == numberOfHotels);
                });
            }
            /* Given:  I have a Place Service GetPlaces method and  VALID parameters,
             * When:   I send them to it
             * Then:   I should receive a list of Place objects, each with lists of hotels of type Hotel 
             */
            [Fact]
            public void Valid_Params_Returns_List_Of_Places_With_Lists_Of_Type_Hotel()
            {
                //Given
                var placeNames = new List<string> { "New_York_City", "Tokyo" };
                int numberOfHotels = 5;
                OrderByQuery orderByQuery = OrderByQuery.Cheapest;
                // When
                var result = IPlaceService.GetPlaces(placeNames, numberOfHotels, orderByQuery);
                // Then
                Assert.All(result, place =>
                {
                    Assert.IsType<List<Hotel>>(place.Hotels);
                });
            }
        }
    }
}
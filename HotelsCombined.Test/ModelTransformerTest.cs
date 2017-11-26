using HotelsCombined.Entities;
using HotelsCombined.Models;
using HotelsCombined.Transformers;
using Microsoft.AspNetCore.Hosting;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace HotelsCombined.Test
{
    public class ModelTransformerTest
    {
        protected IModelTransformer IModelTransformer { get; }

        public ModelTransformerTest()
        {
            var hostingEnvironment = new Mock<IHostingEnvironment>();
            var projectRootDirectory = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName).FullName;
            hostingEnvironment.Setup(m => m.ContentRootPath).Returns(projectRootDirectory);
            IModelTransformer = new ModelTransformer(hostingEnvironment.Object);
        }

        public class HotelToViewModel : ModelTransformerTest
        {
            /* Given:  I have a Hotel to Hotel View Model Converter method and NO hotel model
             * When:   I send null to it
             * Then:   I should have a result of null
             */
            [Fact]
            public void Send_Null_Should_Return_Null()
            {
                // Given // When
                var result = IModelTransformer.HotelToViewModel(null);

                // Then
                Assert.Null(result);
            }

            /* Given:  I have a Hotel to Hotel View Model Converter method and a hotel model
             * When:   I send the hotel model to it
             * Then:   I should have a result of hotel view model
             */
            [Fact]
            public void Send_Hotel_Model_Should_Return_Hotel_View_Model()
            {
                // Given
                var hotel = new Hotel();

                // When
                var result = IModelTransformer.HotelToViewModel(hotel);

                // Then
                Assert.IsType<HotelViewModel>(result);
            }

            /* Given:  I have a Hotel to Hotel View Model Converter method and a hotel model which has an image that does not exist
             * When:   I send the hotel model to it
             * Then:   I should have a result of hotel view model with an empty Image property
             */
            [Fact]
            public void Send_Hotel_Model_With_Image_Does_Not_Exist_Should_Return_Hotel_View_Model_With_Empty_Image_Property()
            {
                // Given
                var hotel = new Hotel
                {
                    Image = "SomeInvalidImageName.jpg"
                };
                // When
                var result = IModelTransformer.HotelToViewModel(hotel);

                // Then
                Assert.Equal(result.Image, string.Empty);
            }

            /* Given:  I have a Hotel to Hotel View Model Converter method and a hotel model
             * When:   I send the hotel model to it
             * Then:   I should have a result of hotel view model with certain properties matching
             */
            [Fact]
            public void Send_Hotel_Model_Should_Return_Hotel_View_Model_With_Properties_Matching()
            {
                // Given
                var hotel = new Hotel
                {
                    ID = 1,
                    Name = "Hotel1",
                    Image = "A110517480.jpg",
                    Rate = 0,
                    StarRating = 5
                };
                // When
                var result = IModelTransformer.HotelToViewModel(hotel);

                // Then
                Assert.Equal(hotel.ID, result.ID);
                Assert.Equal(hotel.Name, result.Name);
                Assert.Equal(hotel.Image, result.Image);
                Assert.Equal(hotel.Rate, result.Rate);
                Assert.Equal(hotel.StarRating, result.StarRating);
            }
        }

        public class PlaceToViewModel : ModelTransformerTest
        {
            /* Given:  I have a Place to Place View Model Converter method and NO place model
             * When:   I send null to it
             * Then:   I should have a result of null
             */
            [Fact]
            public void Send_Null_Should_Return_Null()
            {
                // Given // When
                var result = IModelTransformer.PlaceToViewModel(null);

                // Then
                Assert.Null(result);
            }

            /* Given:  I have a Place to Place View Model Converter method and a place model
             * When:   I send the place model to it
             * Then:   I should have a result of place view model
             */
            [Fact]
            public void Send_Place_Model_Should_Return_Place_View_Model()
            {
                // Given
                var place = new Place();

                // When
                var result = IModelTransformer.PlaceToViewModel(place);

                // Then
                Assert.IsType<PlaceViewModel>(result);
            }

            /* Given:  I have a Place to Place View Model Converter method and a place model
             * When:   I send the place model to it
             * Then:   I should have a result of place view model with certain properties matching
             */
            [Fact]
            public void Send_Place_Model_Should_Return_Place_View_Model_With_Properties_Matching()
            {
                // Given
                var place = new Place { ID = 1, Name = "Place", Hotels = new List<Hotel> { new Hotel(), new Hotel() } };

                // When
                var result = IModelTransformer.PlaceToViewModel(place);

                // Then
                Assert.Equal(place.ID, result.ID);
                Assert.Equal(place.Name, result.Name);
                Assert.Equal(place.Hotels.Count, result.Hotels.ToList().Count);
            }
        }

        public class PlacesToViewModels : ModelTransformerTest
        {
            /* Given:  I have a Places to Place View Models Converter method and NO list of places
             * When:   I send null to it
             * Then:   I should have a result of null
             */
            [Fact]
            public void Send_Null_Should_Return_Null()
            {
                // Given // When
                var result = IModelTransformer.PlacesToViewModels(null);

                // Then
                Assert.Null(result);
            }

            /* Given:  I have a Places to Place View Models Converter method and a list of places
             * When:   I send a list of places to it
             * Then:   I should have a result of a list of place view models
             */
            [Fact]
            public void Send_List_Of_Places_Returns_List_Of_Place_View_Models()
            {
                // Given
                var places = new List<Place> { new Place() };

                // When
                var result = IModelTransformer.PlacesToViewModels(places);

                // Then
                Assert.IsType<List<PlaceViewModel>>(result);
            }

            /* Given:  I have a Places to Place View Models Converter method and a list of places
             * When:   I send a list of places to it
             * Then:   I should have a result of a list of place view models with the same number of items as the list of places
             */
            [Fact]
            public void Send_List_Of_Places_Returns_List_Of_Place_View_Models_With_Same_Quanity()
            {
                // Given
                var places = new List<Place> { new Place() };

                // When
                var result = IModelTransformer.PlacesToViewModels(places);

                // Then
                Assert.True(places.Count == result.Count);
            }
        }
    }
}

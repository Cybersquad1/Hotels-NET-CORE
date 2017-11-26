using HotelsCombined.Entities;
using HotelsCombined.Repository.Contracts;
using HotelsCombined.Repository.Implementations;
using Microsoft.AspNetCore.Hosting;
using Moq;
using System;
using System.IO;
using Xunit;

namespace HotelsCombined.Test
{
    public class PlaceRepositoryTest
    {
        protected IPlaceRepository IPlaceRepository { get; }
        public PlaceRepositoryTest()
        {
            var hostingEnvironment = new Mock<IHostingEnvironment>();
            //TODO: Find a cleaner way to get the project root path
            var projectRootDirectory = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName).FullName;
            hostingEnvironment.Setup(m => m.ContentRootPath).Returns(projectRootDirectory);
            IPlaceRepository = new DummyPlaceRepository(hostingEnvironment.Object);
        }

        public class Get : PlaceRepositoryTest
        {
            /* Given:  I have a Place Repository Get method and NO place file name to send
             * When:   I send NULL to it
             * Then:   I should receive NULL
             */
            [Fact]
            public void Empty_Place_File_Name_Throws_a_Null_Exception()
            {
                // Given // When // Then
                Assert.Throws<ArgumentNullException>(() => { IPlaceRepository.Get(null); } );
            }


            /* Given:  I have a Place Repository Get method and an INVALID place file name
             * When:   I send the INVALID place file name to it
             * Then:   I should receive NULL
             */
            [Fact]
            public void Invalid_Place_File_Name_Returns_Null()
            {
                // Given
                var invalidPlaceFileName = "SafferVille";
                // When 
                var place = IPlaceRepository.Get(invalidPlaceFileName);
                // Then
                Assert.Null(place);
            }

            /* Given:  I have a Place Repository Get method and a VALID place file name
             * When:   I send the VALID place file name to it
             * Then:   I should receive a Place Object with at least one or more Hotels
             */
            [Fact]
            public void Valid_Place_File_Name_Returns_Place()
            {
                // Given 
                var validPlaceFileName = "Tokyo";
                // When
                var place = IPlaceRepository.Get(validPlaceFileName);
                // Then
                Assert.IsType<Place>(place);
                Assert.True(place.Hotels.Count > 0);
             
            }

        }
    }
}

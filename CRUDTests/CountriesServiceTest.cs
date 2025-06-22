using CountryService;
using ServiceContracts;
using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDTests
{
    public class CountriesServiceTest
    {
        private readonly ICountriesService countriesService;
        public CountriesServiceTest ()
        {
            countriesService = new CountriesService ();
        }

        //When CountryAddRequest is null, it should throw ArgumentNullException

        [Fact]
        public void AddCountry_NullCountry()
        {
            CountryAddRequest countryAddRequest = null;

            Assert.Throws<ArgumentNullException>(() => 
            //Act
            countriesService.AddCountry(countryAddRequest));


        }

        //When the CountryName is null, it should throw ArgumentException

        [Fact]
        public void AddCountry_CountryNameIsNull()
        {
            CountryAddRequest countriesAddRequest = new CountryAddRequest()
            {
                CountryName = null
            };

            Assert.Throws<ArgumentException> (() =>
            countriesService.AddCountry(countriesAddRequest));

        }
        //When the CountryName is duplicate, it should throw ArgumentException
        [Fact]
        public void AddCountry_CountryNameIsDuplicate()
        {
            CountryAddRequest countriesAddRequest1 = new CountryAddRequest()
            {
                CountryName = "USA"
            };

             CountryAddRequest countriesAddRequest2 = new CountryAddRequest()
            {
                CountryName = "USA"
            };

            Assert.Throws<ArgumentException>(()   => {
                countriesService.AddCountry(countriesAddRequest1);
                countriesService.AddCountry(countriesAddRequest2);
            });

        }
        //When you supply proper country name, it should insert (add) the country to the existing list of countries

        [Fact]
      
        public void AddCountry_ProperCountryDetails()
        {
            //Arrange
            CountryAddRequest? request = new CountryAddRequest() { CountryName = "Japan" };

            //Act
            CountryResponse response = countriesService.AddCountry(request);

            //Assert
            Assert.True(response.CountryId != Guid.Empty);
        }
    }
}

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
        #region AddCountry
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
            List<CountryResponse> countriesFromGetAll = countriesService.GetAllCountries(); 

            //Assert
            Assert.True(response.CountryId != Guid.Empty);
            Assert.Contains(response, countriesFromGetAll);
        }

        #endregion

        #region GetAllCountries
        //The list of countries should be empty by default (before adding any countries)
        [Fact]
        public void GetAllCountries_EmptyList()
        {
            List<CountryResponse> actual_country_response_list = countriesService.GetAllCountries();
            Assert.Empty(actual_country_response_list);
        }

        [Fact]
        public void GetAllCountries_AddFewCountries()
        {
            //Arrange
            List<CountryAddRequest> country_request_list = new List<CountryAddRequest>() {
        new CountryAddRequest() { CountryName = "USA" },
        new CountryAddRequest() { CountryName = "UK" }
      };

            //Act
            List<CountryResponse> countries_list_from_add_country = new List<CountryResponse>();

            foreach (var country in country_request_list)
            {
                countries_list_from_add_country.Add(countriesService.AddCountry(country));
            }

            List<CountryResponse> actualCountryResponseList = countriesService.GetAllCountries();   

            foreach (var expected_country in countries_list_from_add_country)
            {
                Assert.Contains(expected_country, actualCountryResponseList);
            }
        }


        #endregion

        #region GetCountryByCountryId

        //If we supply null as CountryID, it should return null as CountryResponse
        [Fact]
        public void GetCountryByCountryID_NullCountryID()
        {
            //Arrange
            Guid? countryID = null;

            //Act 
            CountryResponse ? countryResponse = countriesService.GetCountryByCountryId(countryID);

            //Assert

            Assert.Null(countryResponse);

        }

        //If we supply a valid country id, it should return the matching country details as CountryResponse object
        [Fact]
        public void GetCountryByCountryID_ValidCountryID()
        {
            //Arrange
            CountryAddRequest countryAddRequest = new CountryAddRequest() { CountryName = "Cairo" };
            CountryResponse countryResponseFromAdd = countriesService.AddCountry(countryAddRequest);

            //Act 

            CountryResponse? countryResponseFromGet = countriesService.GetCountryByCountryId(countryResponseFromAdd.CountryId);

            //Arrang 

            countryResponseFromAdd.Equals(countryResponseFromGet);

        }


        #endregion
    }
}

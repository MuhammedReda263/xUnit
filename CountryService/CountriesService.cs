using Entitys;
using ServiceContracts;
using ServiceContracts.DTO;

namespace CountryService
{
    public class CountriesService : ICountriesService
    {
        private readonly List<Country> _countries;

        public CountriesService()
        {
            _countries = new List<Country>();
        }
        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
        {
            // Validation: countryAddRequest parameter can't be null
            if (countryAddRequest == null)
            {
                throw new ArgumentNullException(nameof(countryAddRequest));
            }
            //Validation: CountryName can't be null
            if (countryAddRequest.CountryName == null)
            {
                throw new ArgumentException(nameof(countryAddRequest.CountryName));
            }
            //Validation: CountryName can't be duplicate
            if (_countries.Where(u=>u.CountryName == countryAddRequest.CountryName).Count()>0)
            {
                throw new ArgumentException("Given country name already exists");
            }
            Country country = countryAddRequest.ToCountery();
            country.CountryId = Guid.NewGuid();
            _countries.Add(country);
            return country.ToCountryresponse();

        }

        public List<CountryResponse> GetAllCountries()
        {
            return _countries.Select(c => c.ToCountryresponse()).ToList();
        }

        public CountryResponse? GetCountryByCountryId(Guid? countryId)
        {
            if (countryId == null)
            {
                return null;
            }
            Country? country = _countries.FirstOrDefault(c => c.CountryId == countryId);

            if (country == null)
            {
                return null;
            }

            return country.ToCountryresponse();
        }
    }
}

using Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ServiceContracts.DTO
{
    public class CountryResponse
    {
        public Guid CountryId { get; set; }
        public string ?CountryName { get; set; }

        public override bool Equals(object? obj)
        {
             if (obj == null) { return false; }
             if (obj.GetType() != typeof(CountryResponse)) { return false; }
             CountryResponse countryResponse = (CountryResponse) obj;
            return countryResponse.CountryId == CountryId && countryResponse.CountryName == CountryName;
            
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CountryId, CountryName);
            
        }
    }
     
    public static class CountryExtentions
    {
        public static CountryResponse ToCountryresponse(this Country country)
        {
            return new CountryResponse()
            {
                CountryId = country.CountryId,
                CountryName = country.CountryName,
            };
        }
    }
}

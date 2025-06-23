using Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class PersonResponse
    {
        public Guid PersonID { get; set; }
        public string? PersonName { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public Guid? CountryID { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }
        public double? Age { get; set; }

        public override bool Equals(object? obj)
        {
           if (obj == null) return false;
           if(obj.GetType() != typeof(PersonResponse)) return false;
            PersonResponse PersonResponse = (PersonResponse) obj;
           return PersonID == PersonResponse.PersonID && PersonName == PersonResponse.PersonName && Email == PersonResponse.Email && DateOfBirth == PersonResponse.DateOfBirth && Gender == PersonResponse.Gender && CountryID == PersonResponse.CountryID && Address == PersonResponse.Address && ReceiveNewsLetters == PersonResponse.ReceiveNewsLetters;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PersonID + PersonName + DateOfBirth);
        }
    }

    public static class PersonExtention
    {
        public static PersonResponse ToPersonResponse(this Person person) //Inject To Person Class
        {
            return new PersonResponse()
            {
                PersonID = person.PersonID,
                PersonName = person.PersonName,
                Email = person.Email,
                DateOfBirth = person.DateOfBirth,
                ReceiveNewsLetters = person.ReceiveNewsLetters,
                Address = person.Address,
                CountryID = person.CountryID,
                Gender = person.Gender,
                Age = (person.DateOfBirth != null) ? Math.Round((DateTime.Now - person.DateOfBirth.Value).TotalDays / 365.25) : null
            };
        }
    }
}

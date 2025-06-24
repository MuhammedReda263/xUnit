using CountryService;
using Entitys;
using Service.Helpers;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class PersonService : IPersonsService
    {

        private readonly List<Person> _personsList;
        private readonly CountriesService _countriesService;

        public PersonService()
        {
            _personsList = new List<Person>();
            _countriesService = new CountriesService();
        }

        private PersonResponse ConvertPersonToPersonResponse(Person person)
        {
            PersonResponse personResponse = person.ToPersonResponse();
            personResponse.Country = _countriesService.GetCountryByCountryId(person.CountryID)?.CountryName;
            return personResponse;
        }
        public PersonResponse AddPerson(PersonAddRequest? personAddRequest)
        {
            if (personAddRequest == null)
            {
                throw new ArgumentNullException(nameof(personAddRequest));
            }

            // Model Validations
            ValidationHelper.ModelValidation(personAddRequest);
            //------------------------------------------------

            Person person = personAddRequest.ToPerson();

            person.PersonID = Guid.NewGuid();
            _personsList.Add(person);
           return ConvertPersonToPersonResponse(person);

        }

        public List<PersonResponse> GetAllPersons()
        {
            return _personsList.Select(temp=>temp.ToPersonResponse()).ToList();
        }

        public PersonResponse? GetPersonByPersonID(Guid? personId)
        {
            if (personId == null) return null;
            Person? person = _personsList.FirstOrDefault(u => u.PersonID == personId);
            if(person ==null) return null;
            return person.ToPersonResponse();

        }

        public List<PersonResponse> GetFilteredPersons(string SearchBy, string? SerchString)
        {
            List<PersonResponse> allPersons = GetAllPersons();
            List<PersonResponse> matchingPersons = allPersons;
            if (string.IsNullOrEmpty(SerchString) || string.IsNullOrEmpty(SearchBy)) return matchingPersons;

            switch (SearchBy)
            {
                case nameof(Person.PersonName):
                    matchingPersons = allPersons.Where(temp => 
                     (!string.IsNullOrEmpty(temp.PersonName)?    
                    temp.PersonName.Contains(SerchString,StringComparison.OrdinalIgnoreCase) :true)).ToList();
                    break;
                case nameof(Person.Email):
                    matchingPersons = allPersons.Where(temp => 
                     (!string.IsNullOrEmpty(temp.Email)?    
                    temp.Email.Contains(SerchString, StringComparison.OrdinalIgnoreCase) :true)).ToList();
                    break;
                case nameof(Person.DateOfBirth):
                    matchingPersons = allPersons.Where(temp =>
                    (temp.DateOfBirth != null) ?
                    temp.DateOfBirth.Value.ToString("dd MMMM yyyy").Contains(SerchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;

                case nameof(Person.Gender):
                    matchingPersons = allPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Gender) ?
                    temp.Gender.Contains(SerchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(Person.CountryID):
                    matchingPersons = allPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Country) ?
                    temp.Country.Contains(SerchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(Person.Address):
                    matchingPersons = allPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Address) ?
                    temp.Address.Contains(SerchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                  default:
                    return matchingPersons;
                    
            }
             return matchingPersons;

        }

        public List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string SortBy, SortOrderOptions option)
        {
            throw new NotImplementedException();
        }
    }
}

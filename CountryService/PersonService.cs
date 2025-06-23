using CountryService;
using Entitys;
using Service.Helpers;
using ServiceContracts;
using ServiceContracts.DTO;
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
            throw new NotImplementedException();
        }

        public PersonResponse? GetPersonByPersonID(Guid? personId)
        {
            if (personId == null) return null;
            Person? person = _personsList.FirstOrDefault(u => u.PersonID == personId);
            if(person ==null) return null;
            return person.ToPersonResponse();

        }
    }
}

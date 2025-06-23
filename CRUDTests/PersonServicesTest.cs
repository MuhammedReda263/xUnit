using Service;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDTests
{
    public class PersonServicesTest
    {
        private readonly IPersonsService _personsService;
        public PersonServicesTest() 
        {
        _personsService = new PersonService();
        }

        #region AddPerson 
        //When we supply null value as PersonAddRequest, it should throw ArgumentNullException
        [Fact]
        public void AddPerson_NullPerson()
        {
            PersonAddRequest? personAddRequest = null;
            Assert.Throws<ArgumentNullException>(() => { _personsService.AddPerson(personAddRequest); });
        }

        //When we supply null value as PersonName, it should throw ArgumentException
        [Fact]
        public void AddPerson_PersonNameIsNull()
        {
            PersonAddRequest personAddRequest = new PersonAddRequest() { PersonName=null };

            Assert.Throws<ArgumentException>(() =>
            {
                _personsService.AddPerson(personAddRequest);
            });
        }

        //When we supply proper person details, it should insert the person into the persons list; and it should return an object of PersonResponse, which includes with the newly generated person id
        [Fact]
        public void AddPerson_ProperPersonDetails()
        { 
            PersonAddRequest personAddRequest = new PersonAddRequest() { PersonName = "Person name...", Email = "person@example.com", Address = "sample address", CountryID = Guid.NewGuid(), Gender = GenderOptions.Male, DateOfBirth = DateTime.Parse("2000-01-01"), ReceiveNewsLetters = true };

            PersonResponse personResponse = _personsService.AddPerson(personAddRequest);
            List<PersonResponse> PersonResponseList = _personsService.GetAllPersons(); 
            Assert.True (personResponse.PersonID != Guid.Empty);
            Assert.Contains(personResponse, PersonResponseList);
        
        }
        #endregion

        #region GetPersonByPersonID

        //If we supply null as PersonID, it should return null as PersonResponse
        [Fact]
        public void GetPersonByPersonID_NullPersonID()
        {
            Guid? personID = null;

            PersonResponse personResponse = _personsService.GetPersonByPersonID(personID);

            Assert.Null(personResponse);
        }

        //If we supply a valid person id, it should return the valid person details as PersonResponse object
        [Fact]
        public void GetPersonByPersonID_WithPersonID()
        {

        }
            #endregion
        }
}

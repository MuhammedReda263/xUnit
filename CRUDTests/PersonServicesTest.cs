﻿using CountryService;
using Entitys;
using Service;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace CRUDTests
{
    public class PersonServicesTest
    {
        private readonly IPersonsService _personsService;
        private readonly ICountriesService _countriesService;
        private readonly ITestOutputHelper _output;
        public PersonServicesTest(ITestOutputHelper output) 
        {
        _personsService = new PersonService();
        _countriesService = new CountriesService();
        _output = output;
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

            PersonResponse? personResponse = _personsService.GetPersonByPersonID(personID);

            Assert.Null(personResponse);
        }

        //If we supply a valid person id, it should return the valid person details as PersonResponse object
        [Fact]
        public void GetPersonByPersonID_WithPersonID()
        {
            CountryAddRequest country_request = new CountryAddRequest() { CountryName = "Canada" };
            CountryResponse countryResponse =  _countriesService.AddCountry(country_request);
            PersonAddRequest person_request = new PersonAddRequest() { PersonName = "person name...", Email = "email@sample.com", Address = "address", CountryID = countryResponse.CountryId, DateOfBirth = DateTime.Parse("2000-01-01"), Gender = GenderOptions.Male, ReceiveNewsLetters = false };
            PersonResponse person_response_from_add = _personsService.AddPerson(person_request);
            PersonResponse? person_response_from_get = _personsService.GetPersonByPersonID(person_response_from_add.PersonID);
            Assert.Equal(person_response_from_add, person_response_from_get);
        }
        #endregion

        #region GetAllPersons

        //The GetAllPersons() should return an empty list by default
        [Fact]
        public void GetAllPersons_EmptyList()
        { 
            List<PersonResponse> personResponses = _personsService.GetAllPersons();
            Assert.Empty(personResponses);

        }



        //First, we will add few persons; and then when we call GetAllPersons(), it should return the same persons that were added
        [Fact]
        public void GetAllPersons_AddFewPersons()
        {
            CountryAddRequest country_request_1 = new CountryAddRequest() { CountryName = "USA" };
            CountryAddRequest country_request_2 = new CountryAddRequest() { CountryName = "India" };

            CountryResponse country_response_1 = _countriesService.AddCountry(country_request_1);
            CountryResponse country_response_2 = _countriesService.AddCountry(country_request_2);

            PersonAddRequest person_request_1 = new PersonAddRequest() { PersonName = "Smith", Email = "smith@example.com", Gender = GenderOptions.Male, Address = "address of smith", CountryID = country_response_1.CountryId, DateOfBirth = DateTime.Parse("2002-05-06"), ReceiveNewsLetters = true };

            PersonAddRequest person_request_2 = new PersonAddRequest() { PersonName = "Mary", Email = "mary@example.com", Gender = GenderOptions.Female, Address = "address of mary", CountryID = country_response_2.CountryId, DateOfBirth = DateTime.Parse("2000-02-02"), ReceiveNewsLetters = false };

            PersonAddRequest person_request_3 = new PersonAddRequest() { PersonName = "Rahman", Email = "rahman@example.com", Gender = GenderOptions.Male, Address = "address of rahman", CountryID = country_response_2.CountryId, DateOfBirth = DateTime.Parse("1999-03-03"), ReceiveNewsLetters = true };

            List<PersonAddRequest> personsAddRequestList = new List<PersonAddRequest>() { person_request_1, person_request_2, person_request_3 };
            List<PersonResponse> personResponsesFromAdd = new List<PersonResponse>();
            foreach (var personAddRequest in personsAddRequestList)
            {
                PersonResponse personResponse = _personsService.AddPerson(personAddRequest);
                personResponsesFromAdd.Add(personResponse);
            }

            List<PersonResponse> personResponsesFromGet = _personsService.GetAllPersons();

            foreach (var personResponse in personResponsesFromAdd)
            {
                Assert.Contains(personResponse, personResponsesFromGet);
            }

        }

        #endregion

        #region GetFilteredPersons

        //If the search text is empty and search by is "PersonName", it should return all persons
        [Fact]
        public void GetFilteredPersons_EmptySearchText()
        {
            CountryAddRequest country_request_1 = new CountryAddRequest() { CountryName = "USA" };
            CountryAddRequest country_request_2 = new CountryAddRequest() { CountryName = "India" };

            CountryResponse country_response_1 = _countriesService.AddCountry(country_request_1);
            CountryResponse country_response_2 = _countriesService.AddCountry(country_request_2);

            PersonAddRequest person_request_1 = new PersonAddRequest() { PersonName = "Smith", Email = "smith@example.com", Gender = GenderOptions.Male, Address = "address of smith", CountryID = country_response_1.CountryId, DateOfBirth = DateTime.Parse("2002-05-06"), ReceiveNewsLetters = true };

            PersonAddRequest person_request_2 = new PersonAddRequest() { PersonName = "Mary", Email = "mary@example.com", Gender = GenderOptions.Female, Address = "address of mary", CountryID = country_response_2.CountryId, DateOfBirth = DateTime.Parse("2000-02-02"), ReceiveNewsLetters = false };

            PersonAddRequest person_request_3 = new PersonAddRequest() { PersonName = "Rahman", Email = "rahman@example.com", Gender = GenderOptions.Male, Address = "address of rahman", CountryID = country_response_2.CountryId, DateOfBirth = DateTime.Parse("1999-03-03"), ReceiveNewsLetters = true };

            List<PersonAddRequest> personsAddRequestList = new List<PersonAddRequest>() { person_request_1, person_request_2, person_request_3 };
            List<PersonResponse> personResponsesFromAdd = new List<PersonResponse>();
            foreach (var personAddRequest in personsAddRequestList)
            {
                PersonResponse personResponse = _personsService.AddPerson(personAddRequest);
                personResponsesFromAdd.Add(personResponse);
            }

            List<PersonResponse> persons_list_from_search = _personsService.GetFilteredPersons(nameof(Person.PersonName),"");

            foreach (var personResponse in personResponsesFromAdd)
            {
                Assert.Contains(personResponse, persons_list_from_search);
            }
        }


        //First we will add few persons; and then we will search based on person name with some search string. It should return the matching persons
        [Fact]
        public void GetFilteredPersons_SearchByPersonName()
        {
            CountryAddRequest country_request_1 = new CountryAddRequest() { CountryName = "USA" };
            CountryAddRequest country_request_2 = new CountryAddRequest() { CountryName = "India" };

            CountryResponse country_response_1 = _countriesService.AddCountry(country_request_1);
            CountryResponse country_response_2 = _countriesService.AddCountry(country_request_2);

            PersonAddRequest person_request_1 = new PersonAddRequest() { PersonName = "Smith", Email = "smith@example.com", Gender = GenderOptions.Male, Address = "address of smith", CountryID = country_response_1.CountryId, DateOfBirth = DateTime.Parse("2002-05-06"), ReceiveNewsLetters = true };

            PersonAddRequest person_request_2 = new PersonAddRequest() { PersonName = "Mary", Email = "mary@example.com", Gender = GenderOptions.Female, Address = "address of mary", CountryID = country_response_2.CountryId, DateOfBirth = DateTime.Parse("2000-02-02"), ReceiveNewsLetters = false };

            PersonAddRequest person_request_3 = new PersonAddRequest() { PersonName = "Rahman", Email = "rahman@example.com", Gender = GenderOptions.Male, Address = "address of rahman", CountryID = country_response_2.CountryId, DateOfBirth = DateTime.Parse("1999-03-03"), ReceiveNewsLetters = true };

            List<PersonAddRequest> personsAddRequestList = new List<PersonAddRequest>() { person_request_1, person_request_2, person_request_3 };
            List<PersonResponse> personResponsesFromAdd = new List<PersonResponse>();
            foreach (var personAddRequest in personsAddRequestList)
            {
                PersonResponse personResponse = _personsService.AddPerson(personAddRequest);
                personResponsesFromAdd.Add(personResponse);
            }

            List<PersonResponse> persons_list_from_search = _personsService.GetFilteredPersons(nameof(Person.PersonName), "Ma");

            foreach (var personResponse in personResponsesFromAdd)
            {
                if (personResponse.PersonName != null)
                {
                    if (personResponse.PersonName.Contains("ma", StringComparison.OrdinalIgnoreCase))
                    {
                        Assert.Contains(personResponse, persons_list_from_search);
                    }
                }


            }

        }
        #endregion

        #region GetSortedPersons

        //When we sort based on PersonName in DESC, it should return persons list in descending on PersonName
        [Fact]
        public void GetSortedPersons()
        {
            CountryAddRequest country_request_1 = new CountryAddRequest() { CountryName = "USA" };
            CountryAddRequest country_request_2 = new CountryAddRequest() { CountryName = "India" };

            CountryResponse country_response_1 = _countriesService.AddCountry(country_request_1);
            CountryResponse country_response_2 = _countriesService.AddCountry(country_request_2);

            PersonAddRequest person_request_1 = new PersonAddRequest() { PersonName = "Smith", Email = "smith@example.com", Gender = GenderOptions.Male, Address = "address of smith", CountryID = country_response_1.CountryId, DateOfBirth = DateTime.Parse("2002-05-06"), ReceiveNewsLetters = true };

            PersonAddRequest person_request_2 = new PersonAddRequest() { PersonName = "Mary", Email = "mary@example.com", Gender = GenderOptions.Female, Address = "address of mary", CountryID = country_response_2.CountryId, DateOfBirth = DateTime.Parse("2000-02-02"), ReceiveNewsLetters = false };

            PersonAddRequest person_request_3 = new PersonAddRequest() { PersonName = "Rahman", Email = "rahman@example.com", Gender = GenderOptions.Male, Address = "address of rahman", CountryID = country_response_2.CountryId, DateOfBirth = DateTime.Parse("1999-03-03"), ReceiveNewsLetters = true };

            List<PersonAddRequest> personsAddRequestList = new List<PersonAddRequest>() { person_request_1, person_request_2, person_request_3 };
            List<PersonResponse> personResponsesFromAdd = new List<PersonResponse>();
            foreach (var personAddRequest in personsAddRequestList)
            {
                PersonResponse personResponse = _personsService.AddPerson(personAddRequest);
                personResponsesFromAdd.Add(personResponse);
            }
            List<PersonResponse> allPersons = _personsService.GetAllPersons();
            List<PersonResponse> persons_list_from_sorted = _personsService.GetSortedPersons(allPersons, nameof(Person.PersonName),SortOrderOptions.desc);
            personResponsesFromAdd = personResponsesFromAdd.OrderByDescending(temp=> temp.PersonName).ToList();

            for (int i = 0; i < personResponsesFromAdd.Count; i++)
            {
                Assert.Equal(personResponsesFromAdd[i], persons_list_from_sorted[i]);
            }


        }
        #endregion

        #region UpdatePerson

        //When we supply null as PersonUpdateRequest, it should throw ArgumentNullException
        [Fact]
        public void UpdatePerson_NullPerson()
        {
            PersonUpdateRequest? personUpdateRequest = null;
            Assert.Throws<ArgumentNullException>(() => { _personsService.UpdatePerson(personUpdateRequest); });

        }

        //When we supply invalid person id, it should throw ArgumentException
        [Fact]
        public void UpdatePerson_InvalidPersonID()
        {
            PersonUpdateRequest? personUpdateRequest = new PersonUpdateRequest();
            personUpdateRequest.PersonID = Guid.NewGuid();
            Assert.Throws<ArgumentException>(() =>
            {
                _personsService.UpdatePerson(personUpdateRequest);
            });
        }

        //When PersonName is null, it should throw ArgumentException
        [Fact]
        public void UpdatePerson_PersonNameIsNull()
        {
            CountryAddRequest country_add_request = new CountryAddRequest() { CountryName = "UK" };
            CountryResponse country_response_from_add = _countriesService.AddCountry(country_add_request);

            PersonAddRequest person_add_request = new PersonAddRequest() { PersonName = "John", CountryID = country_response_from_add.CountryId, Email = "john@example.com", Address = "address...", Gender = GenderOptions.Male };
            PersonResponse person_response_from_add = _personsService.AddPerson(person_add_request);

            PersonUpdateRequest personUpdateRequest = person_response_from_add.ToPersonUpdateRequest();

            personUpdateRequest.PersonName = null;

            Assert.Throws<ArgumentException>(() => {
                //Act
                _personsService.UpdatePerson(personUpdateRequest);
            });
        }

        //First, add a new person and try to update the person name and email
        [Fact]
        public void UpdatePerson_PersonFullDetailsUpdation()
        {
            CountryAddRequest country_add_request = new CountryAddRequest() { CountryName = "UK" };
            CountryResponse country_response_from_add = _countriesService.AddCountry(country_add_request);

            PersonAddRequest person_add_request = new PersonAddRequest() { PersonName = "John", CountryID = country_response_from_add.CountryId, Address = "Abc road", DateOfBirth = DateTime.Parse("2000-01-01"), Email = "abc@example.com", Gender = GenderOptions.Male, ReceiveNewsLetters = true };

            PersonResponse person_response_from_add = _personsService.AddPerson(person_add_request);

            PersonUpdateRequest personUpdateRequest = person_response_from_add.ToPersonUpdateRequest();
            personUpdateRequest.PersonName = "Muhammed";
            personUpdateRequest.Email = "Muhammed@gmail.com";

            PersonResponse personResponseFromUpdate = _personsService.UpdatePerson(personUpdateRequest);
            PersonResponse? personResponseFromGet = _personsService.GetPersonByPersonID(personResponseFromUpdate.PersonID);

            Assert.Equal(personResponseFromUpdate, personResponseFromGet);




        }

        #endregion

        #region DeletePerson

        //If you supply an valid PersonID, it should return true
        [Fact]
        public void DeletePerson_ValidPersonID()
        {
            //Arrange
            CountryAddRequest country_add_request = new CountryAddRequest() { CountryName = "USA" };
            CountryResponse country_response_from_add = _countriesService.AddCountry(country_add_request);

            PersonAddRequest person_add_request = new PersonAddRequest() { PersonName = "Jones", Address = "address", CountryID = country_response_from_add.CountryId, DateOfBirth = Convert.ToDateTime("2010-01-01"), Email = "jones@example.com", Gender = GenderOptions.Male, ReceiveNewsLetters = true };

            PersonResponse person_response_from_add = _personsService.AddPerson(person_add_request);

            bool exsist = _personsService.DeletePerson(person_response_from_add.PersonID);

            Assert.True(exsist);
        }

        
        //If you supply an valid PersonID, it should return true
        [Fact]
        public void DeletePerson_InValidPersonID()
        {

            bool exsist = _personsService.DeletePerson(Guid.NewGuid());

            Assert.False(exsist);
        }


        #endregion

    }
}

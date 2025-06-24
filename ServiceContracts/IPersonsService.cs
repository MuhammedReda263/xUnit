
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace ServiceContracts
{
    public interface IPersonsService
    {
        PersonResponse AddPerson(PersonAddRequest? personAddRequest);

        List<PersonResponse> GetAllPersons();

        PersonResponse? GetPersonByPersonID (Guid? personId);

        List<PersonResponse> GetFilteredPersons(string SearchBy,string? SerchString);
        List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string SortBy,SortOrderOptions option);

        PersonResponse UpdatePerson (PersonUpdateRequest? personUpdateRequest);

        bool DeletePerson (Guid? personId);

    }
}

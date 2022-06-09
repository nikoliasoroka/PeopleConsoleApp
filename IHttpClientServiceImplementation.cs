namespace PeopleConsoleApp;

public interface IHttpClientServiceImplementation
{
    Task<Tuple<int, string, People?>> GetPeople();
    Task<Tuple<int, string, Person?>> GetPerson(string userName);
    Task<Tuple<int, string, People?>> SearchPerson(string searchBy, string search);
}
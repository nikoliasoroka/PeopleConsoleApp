using System.Text.Json.Serialization;

namespace PeopleConsoleApp;

public class Person
{
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<string> Emails { get; set; }
}

public class People
{
    [JsonPropertyName("value")]
    public List<Person> PersonList { get; set; }
}
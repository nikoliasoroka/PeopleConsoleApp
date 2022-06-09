using Microsoft.Extensions.DependencyInjection;
using PeopleConsoleApp;

class Program
{
    static async Task Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IHttpClientServiceImplementation, HttpClientFactoryService>()
            .AddHttpClient()
            .BuildServiceProvider();

        var service = serviceProvider.GetService<IHttpClientServiceImplementation>();
        var list = await service.GetPeople();
        PrintResult(list);

        Console.WriteLine("Enter userName please: ");
        var search = Console.ReadLine();
        var result = await service.GetPerson(search);
        PrintResult(Tuple.Create(result.Item1, result.Item2, new People() { PersonList = new List<Person>() {result.Item3}}));


        Console.WriteLine("Enter field (FirstName/LastName) for searching please: ");
        var field = Console.ReadLine();
        Console.WriteLine("Enter search value please: ");
        var fieldSearch = Console.ReadLine();
        var poeple = await service.SearchPerson(field, fieldSearch);
        PrintResult(poeple);
    }

    static void PrintResult(Tuple<int, string, People> result)
    {
        if (result.Item1.Equals(200))
        {
            foreach (var person in result.Item3.PersonList)
                Console.WriteLine($"{person.UserName} - {person.FirstName} - {person.LastName} - {string.Join('|', person.Emails)}");
        }

        if (result.Item1.Equals(400))
            Console.WriteLine(result.Item2);
    }
}
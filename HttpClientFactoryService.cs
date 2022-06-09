using System.Text.Json;

namespace PeopleConsoleApp;

public class HttpClientFactoryService : IHttpClientServiceImplementation
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly JsonSerializerOptions _options;
    private readonly string _baseUrl;
    public HttpClientFactoryService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _baseUrl = "https://services.odata.org/TripPinRESTierService";
    }
    
    public async Task<Tuple<int, string, People?>> GetPeople()
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient();
            using var response = await httpClient.GetAsync($"{_baseUrl}/People", HttpCompletionOption.ResponseHeadersRead);
        
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStringAsync();
            var people = JsonSerializer.Deserialize<People>(stream, _options);

            return Tuple.Create((int)response.StatusCode, "success", people);
        }
        catch (Exception e)
        {
            return Tuple.Create<int, string, People?>(500, e.Message, null);
        }
    }

    public async Task<Tuple<int, string, Person?>> GetPerson(string userName)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient();
            using var response = await httpClient.GetAsync($"{_baseUrl}/People('{userName}')", HttpCompletionOption.ResponseHeadersRead);

            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStringAsync();
            var person = JsonSerializer.Deserialize<Person>(stream, _options);
            
            return Tuple.Create((int)response.StatusCode, "success", person);
        }
        catch (Exception e)
        {
            return Tuple.Create<int, string, Person?>(500, e.Message, null);
        }
    }

    public async Task<Tuple<int, string, People?>> SearchPerson(string field, string search)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient();
            using var response = await httpClient.GetAsync($"{_baseUrl}/People?$filter={field} eq '{search}'", HttpCompletionOption.ResponseHeadersRead);

            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStringAsync();
            var people = JsonSerializer.Deserialize<People>(stream, _options);

            return Tuple.Create((int)response.StatusCode, "success", people);
        }
        catch (Exception e)
        {
            return Tuple.Create<int, string, People?>(500, e.Message, null);
        }
    }
}
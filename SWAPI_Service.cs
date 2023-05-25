
using Newtonsoft.Json;

public class SWAPI_Service
{
    private readonly HttpClient _client = new HttpClient();

    private const string baseURL = "https://swapi.dev/api/";

    public string peopleURL = baseURL + "people/";

    public string vehicleURL = baseURL + "vehicles/";

    public async Task<Person> GetPersonAsync(string url)
    {
        HttpResponseMessage response = await _client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            Person person = JsonConvert.DeserializeObject<Person>(content);
            return person;
        }
        //if everything fails...
        return null;
    }

    public async Task<Vehicle> GetVehicleAsync(string url)
    {
        HttpResponseMessage vehResponse = await _client.GetAsync(url);

        //terinary operator -> one -liner if statement
        return (vehResponse.IsSuccessStatusCode) ? await vehResponse.Content.ReadAsAsync<Vehicle>() : null;
    }

    public async Task<T> GetAsync<T>(string url, int id) where T : class
    {
        var endPoint = url + id;
        var response = await _client.GetAsync(endPoint);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<T>(content);
            return obj;
        }
        return default;
    }

    public async Task<SearchResult<Person>> GetPersonSearchAsync(string query)
    {
        HttpResponseMessage response = await _client.GetAsync("https://swapi.dev/api/people?search=" + query);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var searchResult = JsonConvert.DeserializeObject<SearchResult<Person>>(content);
            return searchResult;
        }
        return null;
    }

    public async Task<SearchResult<T>> GetSearchResultAsync<T>(string query, string category) where T : class
    {
        HttpResponseMessage response = await _client.GetAsync($"https://swapi.dev/api/{category}?search={query}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var searchResult = JsonConvert.DeserializeObject<SearchResult<T>>(content);
            return searchResult;
        }
        return null;
    }
}


using Newtonsoft.Json;

public class SearchResult<T> where T : class
{
    [JsonProperty("count")]
    public int Count { get; set; }

    [JsonProperty("next")]
    public object Next { get; set; }

    [JsonProperty("previous")]
    public object Previous { get; set; }

    [JsonProperty("results")]
    public List<T> Results { get; set; }
}

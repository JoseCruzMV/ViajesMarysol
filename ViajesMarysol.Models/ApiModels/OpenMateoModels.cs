using System.Text.Json.Serialization;

namespace ViajesMarysol.Models.ApiModels;

public class OpenMateoResponse
{
    [JsonPropertyName("results")]
    public List<OpenMateoResults>? Results { get; set; }
}

public class OpenMateoResults
{
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }
}
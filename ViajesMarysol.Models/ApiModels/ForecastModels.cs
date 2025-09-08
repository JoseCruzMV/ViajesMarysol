using System.Text.Json.Serialization;

namespace ViajesMarysol.Models.ApiModels;

public class ForecastResponse
{
    [JsonPropertyName("daily")]
    public DailyForecastData? Daily {  get; set; }
}

public class DailyForecastData
{
    [JsonPropertyName("time")]
    public List<string> Time { get; set; }

    [JsonPropertyName("weathercode")]
    public List<int> WeatherCode { get; set; }

    [JsonPropertyName("temperature_2m_max")]
    public List<double> Temperature2mMax { get; set; }

    [JsonPropertyName("temperature_2m_min")]
    public List<double> Temperature2mMin { get; set; }
}
using ViajesMarysol.Models.ApiModels;
using ViajesMarysol.Services.Interfaces;
using ViajesMarysol.ViewModels;

namespace ViajesMarysol.Services.Implementations;

public class WeatherService(
        IHttpClientFactory httpClientFactory
    ) : IWeatherService
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    public async Task<CityForecastViewModel> GetFiveDayForecastAsync(string cityName)
    {
        var client = _httpClientFactory.CreateClient("OpenMateo");
        CityForecastViewModel forecastResult = new();

        try
        {
            var geocodeUrl = $"https://geocoding-api.open-meteo.com/v1/search?name={Uri.EscapeDataString(cityName)}&count=1";
            var geocodeResponse = await client.GetFromJsonAsync<OpenMateoResponse>(geocodeUrl);

            var location = geocodeResponse?.Results?.FirstOrDefault();
            if (location is null)
            {
                forecastResult.Succeeded = false;
                forecastResult.ErrorMessage = "No se encontró la ciudad";
                return forecastResult;
            }

            var weatherUrl = $"https://api.open-meteo.com/v1/forecast?latitude={location.Latitude}&longitude={location.Longitude}&daily=weathercode,temperature_2m_max,temperature_2m_min&past_days=5&forecast_days=1";
            var weatherResponse = await client.GetFromJsonAsync<ForecastResponse>(weatherUrl);
            
            if (weatherResponse?.Daily is not null)
            {
                for (int i = 0; i < weatherResponse.Daily.Time?.Count; i++)
                {
                    forecastResult.DailyForecasts.Add(new DailyForecastViewModel
                    {
                        Date = DateTime.Parse(weatherResponse.Daily.Time[i]).ToString("MMM dd"),
                        WeatherCode = weatherResponse.Daily.WeatherCode[i],
                        TempMax = weatherResponse.Daily.Temperature2mMax[i],
                        TempMin = weatherResponse.Daily.Temperature2mMin[i]
                    });
                }
            }
        }
        catch (Exception ex)
        {   
            Console.WriteLine(ex);
            forecastResult.Succeeded = false;
            forecastResult.ErrorMessage = "Error al obtener el pronóstico del tiempo";
        }
        return forecastResult;
    }
}

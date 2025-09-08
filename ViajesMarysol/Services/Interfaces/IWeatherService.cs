using ViajesMarysol.ViewModels;

namespace ViajesMarysol.Services.Interfaces;

public interface IWeatherService
{
    /// <summary>
    /// Obtiene el pronóstico del tiempo de los últimos cinco días en una ciudad específica.
    /// </summary>
    /// <param name="cityName"></param>
    /// <returns></returns>
    Task<CityForecastViewModel> GetFiveDayForecastAsync(string cityName);
}

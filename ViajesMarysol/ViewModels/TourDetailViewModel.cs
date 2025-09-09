namespace ViajesMarysol.ViewModels;

public class TourDetailsViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int DurationInDays { get; set; }
    public List<CityForecastViewModel>? Cities{ get; set; } = [];

    public Dictionary<string, CityForecastViewModel> CityWeather { get; set; } = [];

    public bool SavedbyUser { get; set; } = false;

}

public class CityForecastViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;

    public bool Succeeded { get; set; } = true;
    public string ErrorMessage { get; set; } = string.Empty;
    public List<DailyForecastViewModel> DailyForecasts { get; set; } = [];

}

public class DailyForecastViewModel
{
    public string Date { get; set; } = string.Empty;
    public int WeatherCode { get; set; }
    public double TempMax { get; set; }
    public double TempMin { get; set; }
}

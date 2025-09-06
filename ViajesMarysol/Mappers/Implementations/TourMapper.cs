using ViajesMarysol.Mappers.Interfaces;
using ViajesMarysol.Models;
using ViajesMarysol.ViewModels;

namespace ViajesMarysol.Mappers.Implementations;
public class TourMapper : ITourMapper
{
    public CityViewModel CityModelToViewModel(City cityModel)
    {
        return new CityViewModel
        {
            Id = cityModel.Id,
            Name = cityModel.Name,
            Country = cityModel.Country
        };
    }

    public TourViewModel TourModelToViewModel(Tour tourModel)
    {
        return new TourViewModel
        {
            Id = tourModel.Id,
            Name = tourModel.Name,
            Description = tourModel.Description,
            Price = tourModel.Price,
            DurationInDays = tourModel.DurationInDays
        };
    }

    public Tour TourViewModelToModel(TourViewModel tourViewModel)
    {
        return new Tour
        {
            Id = tourViewModel.Id,
            Name = tourViewModel.Name,
            Description = tourViewModel.Description,
            Price = tourViewModel.Price,
            DurationInDays = tourViewModel.DurationInDays
        };
    }
}

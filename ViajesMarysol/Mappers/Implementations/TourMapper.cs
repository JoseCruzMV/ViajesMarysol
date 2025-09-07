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
        TourViewModel tourViewModel_helper = new TourViewModel
        {
            Id = tourModel.Id,
            Name = tourModel.Name,
            Description = tourModel.Description,
            Price = tourModel.Price,
            DurationInDays = tourModel.DurationInDays,
            Cities = []
        };

        if (tourModel.Cities is not null && tourModel.Cities.Count > 0)
        {
            foreach (City cityModel in tourModel.Cities)
            {
                tourViewModel_helper.Cities.Add( CityModelToViewModel(cityModel) );
            }
        }

        return tourViewModel_helper;
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

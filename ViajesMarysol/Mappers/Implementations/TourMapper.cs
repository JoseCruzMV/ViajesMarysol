using ViajesMarysol.Mappers.Interfaces;
using ViajesMarysol.Models;
using ViajesMarysol.ViewModels;

namespace ViajesMarysol.Mappers.Implementations;
public class TourMapper : ITourMapper
{
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
}

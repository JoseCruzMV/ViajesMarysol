using ViajesMarysol.Models;
using ViajesMarysol.ViewModels;

namespace ViajesMarysol.Mappers.Interfaces;
public interface ITourMapper
{
    /// <summary>
    /// Mappea un modelo Tour a un TourViewModel
    /// </summary>
    /// <param name="tourModel"></param>
    /// <returns>Una instancia de <see cref="TourViewModel"/></returns>
    public TourViewModel TourModelToViewModel(Tour tourModel);

    /// <summary>
    /// Mappea un modelo City a un CityViewModel
    /// </summary>
    /// <param name="cityModel"></param>
    /// <returns>Una instancia de <see cref="CityViewModel"/></returns>
    public CityViewModel CityModelToViewModel(City cityModel);

    /// <summary>
    /// Mappea un TourViewModel a un modelo Tour
    /// </summary>
    /// <param name="tourViewModel"></param>
    /// <returns>Una instancia de <see cref="Tour"/></returns>
    public Tour TourViewModelToModel(TourViewModel tourViewModel);
}

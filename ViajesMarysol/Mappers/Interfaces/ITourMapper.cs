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
}

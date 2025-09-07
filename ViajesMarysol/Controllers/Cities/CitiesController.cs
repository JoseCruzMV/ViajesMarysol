using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViajesMarysol.Data;
using ViajesMarysol.Mappers.Interfaces;
using ViajesMarysol.ViewModels;

namespace ViajesMarysol.Controllers.Cities;

[Authorize(Roles = "Admin")]
public class CitiesController(
    ViajesMarysolDBContext context,
    ITourMapper tourMapper
    ) : Controller
{
    private readonly ViajesMarysolDBContext _context = context;
    private readonly ITourMapper _tourMapper = tourMapper;

    [HttpGet]
    public IActionResult Index()
    {
        List<CityViewModel> cities = _context.Cities
            .Select(c => _tourMapper.CityModelToViewModel(c))
            .ToList();
        return View(cities);
    }
}

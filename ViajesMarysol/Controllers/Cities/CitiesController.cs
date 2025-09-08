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

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CityViewModel cityViewModel)
    {
        if (ModelState.IsValid)
        {
            var cityModel = new Models.City
            {
                Name = cityViewModel.Name,
                Country = cityViewModel.Country
            };
            _context.Cities.Add(cityModel);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(cityViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var city = _context.Cities.Find(id);
        if (city == null)
        {
            return NotFound();
        }
        _context.Cities.Remove(city);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var city = _context.Cities.Find(id);
        if (city == null)
        {
            return NotFound();
        }
        var cityViewModel = _tourMapper.CityModelToViewModel(city);
        return View(cityViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(CityViewModel cityViewModel)
    {
        if (ModelState.IsValid)
        {
            var city = _context.Cities.Find(cityViewModel.Id);
            if (city == null)
            {
                return NotFound();
            }
            city.Name = cityViewModel.Name;
            city.Country = cityViewModel.Country;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(cityViewModel);
    }
}

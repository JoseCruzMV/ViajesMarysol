using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViajesMarysol.Data;
using ViajesMarysol.Mappers.Interfaces;
using ViajesMarysol.ViewModels;

namespace ViajesMarysol.Controllers.Tours;

[Authorize(Roles = "Admin")]
public class ToursController(
        ViajesMarysolDBContext context,
        ITourMapper tourMapper
    ) : Controller
{
    private readonly ViajesMarysolDBContext _context = context;
    private readonly ITourMapper _tourMapper = tourMapper;


    [HttpGet]
    public async Task<IActionResult> Index() {
        var tours = await _context.Tours.Include(t => t.Cities).ToListAsync();
        List<TourViewModel> tourViewModels = [];
        tours.ForEach(tour => 
            tourViewModels.Add( _tourMapper.TourModelToViewModel(tour) )
        );

        return View(tourViewModels);
    }

    [HttpGet]
    public IActionResult Create() {
        var cities = _context.Cities.ToList();
        TourViewModel tourViewModel = new();
        cities.ForEach(c => tourViewModel.Cities?.Add(_tourMapper.CityModelToViewModel(c))
        );

        return View( tourViewModel );
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TourViewModel tourViewModel) {
        if (ModelState.IsValid) {
            var tourModel = _tourMapper.TourViewModelToModel(tourViewModel);

            tourViewModel.Cities?.Where(c => c.Selected).ToList().ForEach(c => {
                var cityModel = _context.Cities.Find(c.Id);
                if (cityModel != null) {
                    tourModel.Cities.Add(cityModel);
                }
            });
            _context.Tours.Add(tourModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        var cities = _context.Cities.ToList();
        tourViewModel.Cities?.Clear();
        cities.ForEach(c => tourViewModel.Cities?.Add( _tourMapper.CityModelToViewModel(c) ));

        return View(tourViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id) 
    {
        var tourModel = await _context.Tours.FindAsync(id);
        if (tourModel == null) {
            return NotFound();
        }
        _context.Tours.Remove(tourModel);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}

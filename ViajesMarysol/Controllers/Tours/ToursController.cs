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

    [HttpGet]
    public async Task<IActionResult> Edit(int id) 
    {
        var tourModel = await _context.Tours
            .Include(t => t.Cities)
            .FirstOrDefaultAsync(t => t.Id == id);
        if (tourModel == null) 
        {
            return NotFound();
        }
        var cities = _context.Cities.ToList();
        var tourViewModel = _tourMapper.TourModelToViewModel(tourModel);
        tourViewModel.Cities?.Clear();
        cities.ForEach(c => {
            var cityViewModel = _tourMapper.CityModelToViewModel(c);
            if (tourModel.Cities.Any(tc => tc.Id == c.Id)) 
            {
                cityViewModel.Selected = true;
            }
            tourViewModel.Cities?.Add(cityViewModel);
        });
        return View(tourViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, TourViewModel tourViewModel) 
    {
        if (id != tourViewModel.Id) 
        {
            return NotFound();
        }
        if (ModelState.IsValid) 
        {
            var tourModel = await _context.Tours
                .Include(t => t.Cities)
                .FirstOrDefaultAsync(t => t.Id == id);
            if (tourModel == null) 
            {
                return NotFound();
            }
            tourModel.Name = tourViewModel.Name;
            tourModel.Description = tourViewModel.Description;
            tourModel.Price = tourViewModel.Price;
            tourModel.DurationInDays = tourViewModel.DurationInDays;

            tourModel.Cities.Clear();
            tourViewModel.Cities?.Where(c => c.Selected).ToList().ForEach(c => {
                var cityModel = _context.Cities.Find(c.Id);
                if (cityModel != null) 
                {
                    tourModel.Cities.Add(cityModel);
                }
            });
            _context.Tours.Update(tourModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        var cities = _context.Cities.ToList();
        tourViewModel.Cities?.Clear();
        cities.ForEach(c => tourViewModel.Cities?.Add( _tourMapper.CityModelToViewModel(c) ));
        return View(tourViewModel);
    }
}

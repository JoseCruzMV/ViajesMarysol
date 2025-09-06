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
        List<TourViewModel> tourViewModels = new();
        tours.ForEach(tour => 
            tourViewModels.Add( _tourMapper.TourModelToViewModel(tour) )
        );

        return View(tourViewModels);
    }
}

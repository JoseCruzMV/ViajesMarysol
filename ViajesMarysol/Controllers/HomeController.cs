using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViajesMarysol.Data;
using ViajesMarysol.Mappers.Interfaces;
using ViajesMarysol.Models;
using ViajesMarysol.ViewModels;

namespace ViajesMarysol.Controllers
{
    public class HomeController(
            ViajesMarysolDBContext context,
            ITourMapper tourMapper
        ) : Controller
    {
        private readonly ViajesMarysolDBContext _context = context;
        private readonly ITourMapper _tourMapper = tourMapper;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<TourViewModel> tours = await _context.Tours
                .Include(t => t.Cities)
                .Where(t => t.Cities.Any())
                .Select(t => _tourMapper.TourModelToViewModel(t))
                .ToListAsync();
            return View(tours);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tours == null)
            {
                return NotFound();
            }
            var tour = await _context.Tours
                .Include(t => t.Cities)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tour == null)
            {
                return NotFound();
            }
            var tourViewModel = _tourMapper.TourModelToViewModel(tour);
            return View(tourViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

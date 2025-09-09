using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViajesMarysol.Data;
using ViajesMarysol.Models.Users;

namespace ViajesMarysol.Controllers.UserDashboard;

[Authorize]
public class DashboardController(
        ViajesMarysolDBContext context,
        UserManager<ApplicationUser> userManager
    ) : Controller
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ViajesMarysolDBContext _context = context;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);

        var userTours = await _context.UserTours
            .Where(ut => ut.UserId == userId)
            .Include(ut => ut.Tour)
            .ThenInclude(t => t.Cities)
            .Select(ut => ut.Tour)
            .ToListAsync();

        return View(userTours);
    }
}

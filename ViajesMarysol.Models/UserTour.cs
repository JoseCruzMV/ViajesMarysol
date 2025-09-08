using ViajesMarysol.Models.Users;

namespace ViajesMarysol.Models;

public class UserTour
{
    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;
    public int TourId { get; set; } = 0;
    public Tour Tour { get; set; } = null!;
}

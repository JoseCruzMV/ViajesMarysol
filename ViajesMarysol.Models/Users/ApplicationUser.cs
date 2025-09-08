using Microsoft.AspNetCore.Identity;

namespace ViajesMarysol.Models.Users;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public virtual ICollection<UserTour> UserTours { get; set; } = [];     
}

using Microsoft.AspNetCore.Identity;

namespace ViajesMarysol.Models.Users;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}

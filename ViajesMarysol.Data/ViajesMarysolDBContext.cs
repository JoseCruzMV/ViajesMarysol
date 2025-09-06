using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ViajesMarysol.Models.Users;

namespace ViajesMarysol.Data;

public class ViajesMarysolDBContext : IdentityDbContext<ApplicationUser>
{
    public ViajesMarysolDBContext(DbContextOptions<ViajesMarysolDBContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}

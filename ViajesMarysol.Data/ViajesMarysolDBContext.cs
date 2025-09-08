using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ViajesMarysol.Models;
using ViajesMarysol.Models.Users;

namespace ViajesMarysol.Data;

public class ViajesMarysolDBContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<City> Cities { get; set; } = null!;
    public DbSet<Tour> Tours { get; set; } = null!;
    public DbSet<UserTour> UserTours { get; set; } = null!;


    public ViajesMarysolDBContext(DbContextOptions<ViajesMarysolDBContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Tour>().ToTable("Tours");
        builder.Entity<City>().ToTable("Cities");


        builder.Entity<Tour>()
            .HasMany(t => t.Cities)
            .WithMany(c => c.Tours)
            .UsingEntity(tc => tc.ToTable("TourCities"));

        builder.Entity<City>()
            .HasMany(c => c.Tours)
            .WithMany(t => t.Cities)
            .UsingEntity(tc => tc.ToTable("TourCities"));

        builder.Entity<UserTour>()
            .HasKey(ut => new { ut.UserId, ut.TourId });

        builder.Entity<UserTour>()
            .HasOne(ut => ut.User)
            .WithMany(u => u.UserTours)
            .HasForeignKey(ut => ut.UserId);

        builder.Entity<UserTour>()
            .HasOne(ut => ut.Tour)
            .WithMany(t => t.UserTours)
            .HasForeignKey(ut => ut.TourId);
    }
}

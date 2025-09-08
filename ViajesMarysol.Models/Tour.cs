using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViajesMarysol.Models;
public class Tour
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;
    [Required]
    [MaxLength(250)]
    public string Description { get; set; } = null!;
    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }
    [Required]
    [Range(1, 30)]
    public int DurationInDays { get; set; }
    

    public virtual ICollection<City> Cities { get; set; } = [];

    public virtual ICollection<UserTour> UserTours { get; set; } = [];
}

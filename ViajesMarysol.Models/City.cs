using System.ComponentModel.DataAnnotations;

namespace ViajesMarysol.Models;
public class City
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;
    [Required]
    [MaxLength(100)]
    public string Country { get; set; } = null!;


    public virtual ICollection<Tour> Tours { get; set; } = [];
}

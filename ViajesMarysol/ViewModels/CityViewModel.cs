using System.ComponentModel.DataAnnotations;

namespace ViajesMarysol.ViewModels;
public class CityViewModel
{
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [MaxLength(100)]
    public string Country { get; set; } = string.Empty;

    public bool Selected { get; set; } = false;
}

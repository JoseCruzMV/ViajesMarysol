using System.ComponentModel.DataAnnotations;

namespace ViajesMarysol.ViewModels;
public class TourViewModel
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    [Display(Name = "Nombre")]
    public string Name { get; set; } = string.Empty;
    [Required]
    [MaxLength(250)]
    [Display(Name = "Descripción")]
    public string Description { get; set; } = string.Empty;
    [Required]
    [Display(Name = "Precio")]
    public decimal Price { get; set; }
    [Required]
    [Range(1, 30)]
    [Display(Name = "Duración (días)")]
    public int DurationInDays { get; set; }

    public List<CityViewModel>? Cities{ get; set; } = [];
}

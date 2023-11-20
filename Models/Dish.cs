#pragma warning disable CS8618
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChefsNDishes.Models;

public class Dish
{
    [Key]
    public int DishId { get; set; }

    [Required]
    [DisplayName("Name of Dish")]
    [MinLength(2, ErrorMessage = "Name of dish must be at least 2 characters")]
    public string Name { get; set; }

    [Required]
    [Range(1, 10000)]
    public int? Calories { get; set; }

    [Required]
    [Range(1, 5)]
    public int Tastiness { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    [Required]
    [DisplayName("Chef")]
    public int? ChefId { get; set; }
    public Chef? Creator { get; set; }

}

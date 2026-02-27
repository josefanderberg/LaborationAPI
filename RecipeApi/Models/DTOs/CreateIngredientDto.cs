using System.ComponentModel.DataAnnotations;

namespace RecipeApi.Models.DTOs;

public class  CreateIngredientDto {
    [Required(ErrorMessage = "Ingredient name is required.")]
    [StringLength(100, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;

    [Range(0.001, 10000, ErrorMessage = "Quantity must be a positive number.")]
    public decimal Quantity { get; set; }

    [Required(ErrorMessage = "Unit is required.")]
    [StringLength(20)]
    public string Unit { get; set; } = string.Empty;
}
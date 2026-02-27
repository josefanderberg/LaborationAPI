using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Runtime.CompilerServices;

namespace RecipeApi.Models.DTOs;

public class CreateRecipeDto {
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters long.")]
    public string Name { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
    public string? Description { get; set; }

    [Range(1, 480, ErrorMessage = "PrepTimeMinutes must be between 1 and 480.")]
    public int PrepTimeMinutes { get; set; }

    [Range(0, 480, ErrorMessage = "CookTimeMinutes must be between 0 and 480.")]
    public int CookTimeMinutes { get; set; }

    [Range(1, 100, ErrorMessage = "Servings must be between 1 and 100.")]
    public int Servings { get; set; }

    [Required(ErrorMessage = "Difficulty is required.")]
    [RegularExpression("^(Easy|Medium|Hard)$", ErrorMessage = "Difficulty must be 'Easy', 'Medium', or 'Hard'.")]
    public string Difficulty { get; set; } = string.Empty;

    [Required(ErrorMessage = "At least one ingredient is required.")]
    [MinLength(1, ErrorMessage = "At least one ingredient is required.")]
    public List<CreateIngredientDto> Ingredients { get; set; } = new();

    [Required(ErrorMessage = "At least one instruction is required.")]
    [MinLength(1, ErrorMessage = "At least one instruction is required.")]
    public List<string> Instructions { get; set; } = new();
}


using System.Collections.Generic;

namespace RecipeApi.Models.DTOs;

public class  RecipeResponseDto {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int PrepTimeMinutes { get; set; }
    public int CookTimeMinutes { get; set; }
    public int TotalTimeMinutes => PrepTimeMinutes + CookTimeMinutes;
    public int Servings { get; set; }
    public string Difficulty { get; set; } = string.Empty;
    public List<Ingredient> Ingredients { get; set; } = new();
    public List<string> Instructions { get; set; } = new();
    public DateTime CreatedAt { get; set; }
}
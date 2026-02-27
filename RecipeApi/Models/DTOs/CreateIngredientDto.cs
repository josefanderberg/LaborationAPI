using System.ComponentModel.DataAnnotations;

namespace RecipeApi.Models.DTOs
{
    public class CreateIngredientDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Range(0.01, 10000)]
        public decimal? Quantity { get; set; }

        public string DisplayQuantity { get; set; } = string.Empty;

        [Required]
        public string Unit { get; set; } = string.Empty;
    }
}

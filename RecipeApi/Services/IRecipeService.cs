using RecipeApi.Models;
using RecipeApi.Models.DTOs;

namespace RecipeApi.Services
{
    public interface IRecipeService
    {
        Task<IEnumerable<RecipeResponseDto>> GetAllRecipesAsync();
        Task<RecipeResponseDto?> GetRecipeByIdAsync(int id);
        Task<IEnumerable<RecipeResponseDto>> SearchRecipesAsync(string q);
        Task<RecipeResponseDto> CreateRecipeAsync(CreateRecipeDto createRecipeDto);
        Task<bool> UpdateRecipeAsync(int id, CreateRecipeDto updateRecipeDto);
        Task<bool> DeleteRecipeAsync(int id);
        Task<IEnumerable<RecipeResponseDto>> GetRecipesByDifficultyAsync(string level);
    }
}

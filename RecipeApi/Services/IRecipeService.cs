using RecipeApi.Models;
using RecipeApi.Models.DTOs;

namespace RecipeApi.Services
{
    public interface IRecipeService
    {
        Task<IEnumerable<Recipe>> GetAllRecipesAsync();
        Task<Recipe?> GetRecipeByIdAsync(int id);
        Task<IEnumerable<Recipe>> SearchRecipesAsync(string q);
        Task<Recipe> CreateRecipeAsync(CreateRecipeDto createRecipeDto);
        Task<bool> UpdateRecipeAsync(int id, CreateRecipeDto updateRecipeDto);
        Task<bool> DeleteRecipeAsync(int id);
        Task<IEnumerable<Recipe>> GetRecipesByDifficultyAsync(string level);
    }
}

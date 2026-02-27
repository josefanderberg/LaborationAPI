

using RecipeApi.Models;

namespace RecipeApi.Repositories;

public interface IRecipeRepository
{
    Task<IEnumerable<Recipe>> GetAllAsync();
    Task<Recipe?> GetByIdAsync(int id);
    Task<IEnumerable<Recipe>> SearchAsync(string searchTerm);
    Task<IEnumerable<Recipe>> GetByDifficultyAsync(string difficulty);
    Task<Recipe> CreateAsync(Recipe recipe);
    Task<Recipe?> UpdateAsync(int id, Recipe recipe);
    Task<bool> DeleteAsync(int id);
}

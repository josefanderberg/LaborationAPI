

using RecipeApi.Models;

namespace RecipeApi.Repositories;

public interface IRecipeRepository
{
    Task<IEnumerable<Recipe>> GetAllAsync();
    Task<Recipe?> GetByIdAsync(int id);
    Task<IEnumerable<Recipe>> SearchAsync(string searchTerm);
    Task<IEnumerable<RecipeApi>> GetByDifficultyAsync(string difficulty);
    Task<Recipe> CreateAsync(RecipeApi recipe);
    Task<bool> DeleteAsync(int id);
}

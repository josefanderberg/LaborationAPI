

using RecipeApi.Models;

namespace RecipeApi.Repositories;

public class RecipeRepository : IRecipeRepository {
    private readonly List<Recipe> _recipes = new();
    private int _nextId = 1;
    private readonly object _lock = new();

    public Task<IEnumerable<Recipe>> GetAllAsync()
    {
        lock (_lock)
        {
            return Task.FromResult<IEnumerable<Recipe>>(_recipes.ToList());
        }
    }

    public Task<RecipeApi?> GetByIdAsync(int id)
    {
        lock(_lock)
        {
            return Task.FromResult(_recipes.FirstOrDefault(r => r.Id == id));
        }
    }

    public Task<IEnumerable<Recipe>> SearchAsync(string searchTerm)
    {
        lock(_lock)
        {
            var results = _recipes.Where(r =>
                r.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                r.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                r.Ingredients.Any(i => i.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                ).ToList();

            return Task.FromResult<IEnumerable<Recipe>>(results);
        }
    }

    public Task<IEnumerable<Recipe>> GetByDifficultyAsync(string difficulty)
    {
        lock(_lock)
        {
            var results = _recipes
                .Where(r => r.Difficulty.Equals(difficulty, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Task.FromResult<IEnumerable<Recipe>>(results);
        }
    }

    public Task<Recipe> CreateAsync(Recipe recipe)
    {
        lock(_lock)
        {
            recipe.Id = _nextId++;
            recipe.CreatedAt = DateTime.UtcNow;
            _recipes.Add(recipe);

            return Task.FromResult(recipe);
        }
    }

    public Task<Recipe?> UpdateAsync(int id, Recipe recipe)
    {
        lock(_lock)
        {
            var existing = _recipes.FirstOrDefault(r => r.Id == id);

            if (existing is null)
                return Task.FromResult<Recipe?>(null);

            existing.Name = recipe.Name;
            existing.Description = recipe.Description;
            existing.PrepTimeMinutes = recipe.PrepTimeMinutes;
            existing.CookTimeMinutes = recipe.CookTimeMinutes;
            existing.Difficulty = recipe.Difficulty;
            existing.Ingredients = recipe.Ingredients;
            existing.Instructions = recipe.Instructions;

            return Task.FromResult<Recipe?>(existing);
        }
    }

    public Task<bool> DeleteAsync(int id)
    {
        lock(_lock)
        {
            var recipe = _recipes.FirstOrDefault(r => r.Id == id);

            if (recipe is null)
                return Task.FromResult(false);

            _recipes.Remove(recipe);
            return Task.FromResult(true);
        }
    }
}
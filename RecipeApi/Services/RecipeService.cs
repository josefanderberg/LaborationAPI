using RecipeApi.Models;
using RecipeApi.Models.DTOs;
using RecipeApi.Repositories;

namespace RecipeApi.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipesAsync()
        {
            return await _recipeRepository.GetAllAsync();
        }

        public async Task<Recipe?> GetRecipeByIdAsync(int id)
        {
            return await _recipeRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Recipe>> SearchRecipesAsync(string q)
        {
            return await _recipeRepository.SearchAsync(q);
        }

        public async Task<Recipe> CreateRecipeAsync(CreateRecipeDto dto)
        {
            var recipe = new Recipe
            {
                Name = dto.Name,
                Description = dto.Description,
                PrepTimeMinutes = dto.PrepTimeMinutes,
                CookTimeMinutes = dto.CookTimeMinutes,
                Servings = dto.Servings,
                Difficulty = dto.Difficulty,
                Ingredients = dto.Ingredients.Select(i => new Ingredient
                {
                    Name = i.Name,
                    Quantity = decimal.TryParse(i.Quantity.ToString(), out var q) ? q : 0, 
                    Unit = i.Unit
                }).ToList(),
                Instructions = dto.Instructions
            };

            return await _recipeRepository.CreateAsync(recipe);
        }

        public async Task<bool> UpdateRecipeAsync(int id, CreateRecipeDto dto)
        {
            var recipeUpdate = new Recipe
            {
                Name = dto.Name,
                Description = dto.Description,
                PrepTimeMinutes = dto.PrepTimeMinutes,
                CookTimeMinutes = dto.CookTimeMinutes,
                Servings = dto.Servings,
                Difficulty = dto.Difficulty,
                Ingredients = dto.Ingredients.Select(i => new Ingredient
                {
                    Name = i.Name,
                    Quantity = decimal.TryParse(i.Quantity.ToString(), out var q) ? q : 0,
                    Unit = i.Unit
                }).ToList(),
                Instructions = dto.Instructions
            };

            var updatedRecipe = await _recipeRepository.UpdateAsync(id, recipeUpdate);
            return updatedRecipe != null;
        }

        public async Task<bool> DeleteRecipeAsync(int id)
        {
            return await _recipeRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Recipe>> GetRecipesByDifficultyAsync(string level)
        {
            return await _recipeRepository.GetByDifficultyAsync(level);
        }
    }
}

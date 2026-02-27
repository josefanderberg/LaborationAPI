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
            throw new NotImplementedException();
        }

        public async Task<Recipe?> GetRecipeByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Recipe>> SearchRecipesAsync(string q)
        {
            throw new NotImplementedException();
        }

        public async Task<Recipe> CreateRecipeAsync(CreateRecipeDto createRecipeDto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateRecipeAsync(int id, CreateRecipeDto updateRecipeDto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteRecipeAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Recipe>> GetRecipesByDifficultyAsync(string level)
        {
            throw new NotImplementedException();
        }
    }
}

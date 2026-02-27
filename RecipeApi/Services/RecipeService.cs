using RecipeApi.Models;
using RecipeApi.Models.DTOs;
using RecipeApi.Repositories;

namespace RecipeApi.Services {
    public class RecipeService : IRecipeService {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService( IRecipeRepository recipeRepository ) {
            _recipeRepository = recipeRepository;
        }

        public async Task<IEnumerable<RecipeResponseDto>> GetAllRecipesAsync() {
            var recipes = await _recipeRepository.GetAllAsync();
            return recipes.Select( MapToDto );
        }

        public async Task<RecipeResponseDto?> GetRecipeByIdAsync( int id ) {
            var recipe = await _recipeRepository.GetByIdAsync( id );
            return recipe is null ? null : MapToDto( recipe );
        }

        public async Task<IEnumerable<RecipeResponseDto>> SearchRecipesAsync( string q ) {
            var recipes = await _recipeRepository.SearchAsync( q );
            return recipes.Select( MapToDto );
        }

        public async Task<RecipeResponseDto> CreateRecipeAsync( CreateRecipeDto dto ) {
            var recipe = MapFromDto( dto );
            var created = await _recipeRepository.CreateAsync( recipe );
            return MapToDto( created );
        }

        public async Task<bool> UpdateRecipeAsync( int id, CreateRecipeDto dto ) {
            var recipe = MapFromDto( dto );
            var updated = await _recipeRepository.UpdateAsync( id, recipe );
            return updated != null;
        }

        public async Task<bool> DeleteRecipeAsync( int id ) {
            return await _recipeRepository.DeleteAsync( id );
        }

        public async Task<IEnumerable<RecipeResponseDto>> GetRecipesByDifficultyAsync( string level ) {
            var recipes = await _recipeRepository.GetByDifficultyAsync( level );
            return recipes.Select( MapToDto );
        }

        // --- Hjälpmetoder ---

        private static Recipe MapFromDto( CreateRecipeDto dto ) => new() {
            Name = dto.Name,
            Description = dto.Description ?? string.Empty,
            PrepTimeMinutes = dto.PrepTimeMinutes,
            CookTimeMinutes = dto.CookTimeMinutes,
            Servings = dto.Servings,
            Difficulty = dto.Difficulty,
            Ingredients = dto.Ingredients.Select( i => new Ingredient {
                Name = i.Name,
                Quantity = i.Quantity,
                Unit = i.Unit
            } ).ToList(),
            Instructions = dto.Instructions
        };

        private static RecipeResponseDto MapToDto( Recipe recipe ) => new() {
            Id = recipe.Id,
            Name = recipe.Name,
            Description = recipe.Description,
            PrepTimeMinutes = recipe.PrepTimeMinutes,
            CookTimeMinutes = recipe.CookTimeMinutes,
            Servings = recipe.Servings,
            Difficulty = recipe.Difficulty,
            Ingredients = recipe.Ingredients,
            Instructions = recipe.Instructions,
            CreatedAt = recipe.CreatedAt
        };
    }
}
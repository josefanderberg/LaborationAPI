using Microsoft.AspNetCore.Mvc;
using RecipeApi.Models;
using RecipeApi.Models.DTOs;
using RecipeApi.Services;

namespace RecipeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipesController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        // GET | /api/recipes | Hämta alla recept
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetAll()
        {
            var recipes = await _recipeService.GetAllRecipesAsync();
            return Ok(recipes);
        }

        // GET | /api/recipes/{id} | Hämta specifikt recept
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetById(int id)
        {
            var recipe = await _recipeService.GetRecipeByIdAsync(id);
            
            if (recipe == null)
            {
                return NotFound();
            }

            return Ok(recipe);
        }

        // GET | /api/recipes/search?q={term} | Sök recept
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Recipe>>> Search([FromQuery] string q)
        {
            if (string.IsNullOrWhiteSpace(q))
            {
                return BadRequest("Söktermen får inte vara tom.");
            }

            var recipes = await _recipeService.SearchRecipesAsync(q);
            return Ok(recipes);
        }

        // POST | /api/recipes | Skapa nytt recept
        [HttpPost]
        public async Task<ActionResult<Recipe>> Create([FromBody] CreateRecipeDto createRecipeDto)
        {
            var createdRecipe = await _recipeService.CreateRecipeAsync(createRecipeDto);
            return CreatedAtAction(nameof(GetById), new { id = createdRecipe.Id }, createdRecipe);
        }

        // PUT | /api/recipes/{id} | Uppdatera recept
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateRecipeDto updateRecipeDto)
        {
            var success = await _recipeService.UpdateRecipeAsync(id, updateRecipeDto);
            
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE | /api/recipes/{id} | Ta bort recept
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _recipeService.DeleteRecipeAsync(id);
            
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        // GET | /api/recipes/difficulty/{level} | Filtrera på svårighetsgrad
        [HttpGet("difficulty/{level}")]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetByDifficulty(string level)
        {
            var recipes = await _recipeService.GetRecipesByDifficultyAsync(level);
            return Ok(recipes);
        }
    }
}
using Moq;
using RecipeApi.Models;
using RecipeApi.Models.DTOs;
using RecipeApi.Repositories;
using RecipeApi.Services;

namespace RecipeApi.Tests.Services;

public class RecipeServiceTests
{
    private readonly Mock<IRecipeRepository> _mockRepo;
    private readonly RecipeService _service;

    public RecipeServiceTests()
    {
        _mockRepo = new Mock<IRecipeRepository>();
        _service = new RecipeService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetAllRecipesAsync_ReturnsAllRecipes()
    {
        // Arrange
        var recipes = new List<Recipe>
        {
            new Recipe { Id = 1, Name = "Recipe 1" },
            new Recipe { Id = 2, Name = "Recipe 2" }
        };
        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(recipes);

        // Act
        var result = await _service.GetAllRecipesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal("Recipe 1", result.First().Name);
    }

    [Fact]
    public async Task GetRecipeByIdAsync_ReturnsRecipe_WhenExists()
    {
        // Arrange
        var recipe = new Recipe { Id = 1, Name = "Recipe 1" };
        _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(recipe);

        // Act
        var result = await _service.GetRecipeByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Recipe 1", result.Name);
    }

    [Fact]
    public async Task GetRecipeByIdAsync_ReturnsNull_WhenNotExists()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetByIdAsync(999)).ReturnsAsync((Recipe?)null);

        // Act
        var result = await _service.GetRecipeByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task SearchRecipesAsync_ReturnsMatchingRecipes()
    {
        // Arrange
        var recipes = new List<Recipe>
        {
            new Recipe { Id = 1, Name = "Spaghetti" }
        };
        _mockRepo.Setup(repo => repo.SearchAsync("Spaghetti")).ReturnsAsync(recipes);

        // Act
        var result = await _service.SearchRecipesAsync("Spaghetti");

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Spaghetti", result.First().Name);
    }

    [Fact]
    public async Task DeleteRecipeAsync_ReturnsTrue_WhenSuccessful()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.DeleteAsync(1)).ReturnsAsync(true);

        // Act
        var result = await _service.DeleteRecipeAsync(1);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GetRecipesByDifficultyAsync_ReturnsMatchingRecipes()
    {
        // Arrange
        var recipes = new List<Recipe>
        {
            new Recipe { Id = 1, Name = "Hard Recipe", Difficulty = "Hard" }
        };
        _mockRepo.Setup(repo => repo.GetByDifficultyAsync("Hard")).ReturnsAsync(recipes);

        // Act
        var result = await _service.GetRecipesByDifficultyAsync("Hard");

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Hard", result.First().Difficulty);
    }

    [Fact]
    public async Task CreateRecipeAsync_CreatesAndReturnsRecipe()
    {
        // Arrange
        var dto = new CreateRecipeDto
        {
            Name = "New Recipe",
            Description = "A new test recipe",
            PrepTimeMinutes = 10,
            CookTimeMinutes = 20,
            Servings = 4,
            Difficulty = "Easy",
            Instructions = new List<string> { "Step 1" },
            Ingredients = new List<CreateIngredientDto>
            {
                new CreateIngredientDto { Name = "Flour", Quantity = 200, Unit = "g" }
            }
        };

        var expectedRecipe = new Recipe { Id = 1, Name = "New Recipe" };

        _mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<Recipe>()))
                 .ReturnsAsync(expectedRecipe);

        // Act
        var result = await _service.CreateRecipeAsync(dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("New Recipe", result.Name);
    }

    [Fact]
    public async Task UpdateRecipeAsync_ReturnsTrue_WhenSuccessful()
    {
        // Arrange
        var dto = new UpdateRecipeDto
        {
            Name = "Updated Recipe",
            Description = "Updated description",
            PrepTimeMinutes = 15,
            CookTimeMinutes = 25,
            Servings = 2,
            Difficulty = "Medium",
            Instructions = new List<string>(),
            Ingredients = new List<CreateIngredientDto>()
        };

        var updatedRecipe = new Recipe { Id = 1, Name = "Updated Recipe" };

        _mockRepo.Setup(repo => repo.UpdateAsync(1, It.IsAny<Recipe>()))
                 .ReturnsAsync(updatedRecipe);

        // Act
        var result = await _service.UpdateRecipeAsync(1, dto);

        // Assert
        Assert.True(result);
    }
}

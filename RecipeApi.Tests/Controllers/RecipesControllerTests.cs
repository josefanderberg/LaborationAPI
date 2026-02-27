using Microsoft.AspNetCore.Mvc;
using Moq;
using RecipeApi.Controllers;
using RecipeApi.Models;
using RecipeApi.Models.DTOs;
using RecipeApi.Services;

namespace RecipeApi.Tests.Controllers;

public class RecipesControllerTests
{
    private readonly Mock<IRecipeService> _mockService;
    private readonly RecipesController _controller;

    public RecipesControllerTests()
    {
        _mockService = new Mock<IRecipeService>();
        _controller = new RecipesController(_mockService.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult_WithRecipes()
    {
        // Arrange
        var recipes = new List<Recipe>
        {
            new Recipe { Id = 1, Name = "Recipe 1" },
            new Recipe { Id = 2, Name = "Recipe 2" }
        };
        _mockService.Setup(service => service.GetAllRecipesAsync()).ReturnsAsync(recipes);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedRecipes = Assert.IsAssignableFrom<IEnumerable<Recipe>>(okResult.Value);
        Assert.Equal(2, returnedRecipes.Count());
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenRecipeDoesNotExist()
    {
        // Arrange
        _mockService.Setup(service => service.GetRecipeByIdAsync(999)).ReturnsAsync((Recipe?)null);

        // Act
        var result = await _controller.GetById(999);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task Create_ReturnsCreatedAtAction_WithCreatedRecipe()
    {
        // Arrange
        var dto = new CreateRecipeDto { Name = "New Recipe" };
        var createdRecipe = new Recipe { Id = 1, Name = "New Recipe" };
        _mockService.Setup(service => service.CreateRecipeAsync(dto)).ReturnsAsync(createdRecipe);

        // Act
        var result = await _controller.Create(dto);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(nameof(RecipesController.GetById), createdAtActionResult.ActionName);
        Assert.Equal(createdRecipe.Id, ((dynamic)createdAtActionResult.RouteValues!)["id"]);
        Assert.Equal(createdRecipe, createdAtActionResult.Value);
    }
}

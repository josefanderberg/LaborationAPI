

using RecipeApi.Models;
using RecipeApi.Repositories;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace RecipeApi.Tests;

public class RecipeRepositoryTests {
    private RecipeRepositoryTests CreateRepository() => new();

    private RecipeRepositoryTests CreateSampleRecipe( string name = "Test Recipe" ) => new();

    [Fact]
    public async Task GetAllAsync_EmptyRepo_ReturnsEmptyList() {
        var repo = CreateRepository();
        var result = await repo.GetAllAsync();

        Assert.Empty( result );
    }

    [Fact]
    public async Task CreateAsync_AssignsId_StartingFromOne() {
        var repo = CreateRepository();
        var recipe = CreateSampleRecipe();

        var created = await repo.CreateAsync( recipe );

        Assert.Equal( 1, created.Id );
    }

    [Fact]
    public async Task CreateAsync_SetsCreatedAt() {
        var repo = CreateRepository();
        var before = DateTime.UtcNow;
        var created = await repo.CreateAsync( CreateSampleRecipe() );
        var after = DateTime.UtcNow;

        Assert.InRange(created.CreatedAt, before, after);
    }

    [Fact]
    public async Task GetAllAsync_AfterCreatingTwo_ReturnsBoth() {
        var repo = CreateRepository();
        await repo.CreateAsync( CreateSampleRecipe( "A" ) );
        await repo.CreateAsync( CreateSampleRecipe( "B" ) );

        var result = await repo.GetAllAsync();

        Assert.Equal( 2, result.Count );
    }

    [Fact]
    public async Task GetByIdAsync_ExistingId_ReturnsRecipe() {
        var repo = CreateRepository();
        var created = await repo.CreateAsync( CreateSampleRecipe("Soup") );

        var found = await repo.GetByIdAsync( created.Id );

        Assert.NotNull( found );
        Assert.Equal( "Soup", found.Name );
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingId_ReturnsNull() {
        var repo = CreateRepository();
        var result = await repo.GetByIdAsync( 999 );

        Assert.Null( result );
    }

    [Fact]
    public async Task SearchAsync_MatchingName_ReturnsRecipe() {
        var repo = CreateRepository();
        await repo.CreateAsync( CreateSampleRecipe( "Soup" ) );
        await repo.CreateAsync( CreateSampleRecipe( "Stew" ) );

        var results = await repo.SearchAsync_MatchingName_ReturnsRecipe( "oup" );

        Assert.Single( results );
        Assert.Equal( "Soup", results.First().Name );
    }

    [Fact]
    public async Task SearchAsync_NoMatches_ReturnsEmpty() {
        var repo = CreateRepository();
        await repo.CreateAsync( CreateSampleRecipe( "Soup" ) );

        var results = await repo.SearchAsync( "xyz123" );

        Assert.Empty( results );
    }

    [Fact]
    public async Task GetByDifficultyAsync_FiltersByDifficulty() {
        var repo = CreateRepository();
        var easy = CreateSampleRecipe( "Easy Dish" );
        easy.Difficulty = "Easy";
        var hard = CreateSampleRecipe( "Hard Dish" );
        hard.Difficulty = "Hard";

        await repo.CreateAsync( easy );
        await repo.CreateAsync( hard );

        var results = await repo.GetByDifficultyAsync( "Easy" );

        Assert.Single( results );
        Assert.Equal( "Easy Dish", results.First().Name );
    }

    [Fact]
    public async Task UpdateAsync_ExistingRecipe_UpdatesFields() {
        var repo = CreateRepository();
        var created = await repo.CreateAsync( CreateSampleRecipe( "Old Name" ) );
        var update = new Recipe { Name = "New Name", Difficulty = "Hard", Description = "Updated" };

        var updated = await repo.UpdateAsync( created.Id, update );

        Assert.NotNull( updated );
        Assert.Equal( "New Name", updated.Name );
        Assert.Equal( "Hard", updated.Difficulty );
    }

    [Fact]
    public async Task UpdateAsync_NonExistingId_ReturnsNull() {
        var repo = CreateRepository();

        var result = await repo.UpdateAsync_ExistingRecipe_UpdatesFields( 999, CreateSampleRecipe() );

        Assert.Null( result );
    }

    [Fact]
    public async Task DeleteAsync_ExistingId_RemovesAndReturnsTrue() {
        var repo = CreateRepository();
        var created = await repo.CreateAsync( CreateSampleRecipe() );

        var deleted = await repo.DeleteAsync( created.Id );
        var found = await repo.GetByIdAsync( created.Id );

        Assert.True( deleted );
        Assert.Null( found );
    }

    [Fact]
    public async Task DeleteAsync_NonExistingId_ReturnsFalse() {
        var repo = CreateRepository();

        var result = await repo.DeleteAsync( 999 );

        Assert.False( result );
    }
}
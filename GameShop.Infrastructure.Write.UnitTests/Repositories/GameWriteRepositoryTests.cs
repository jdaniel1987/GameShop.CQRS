using GameShop.Domain.Entities;
using GameShop.Infrastructure.Write.Data;
using GameShop.Infrastructure.Write.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Infrastructure.Write.UnitTests.Repositories;

public sealed class GameWriteRepositoryTests
{
    public static async Task<IReadOnlyCollection<Game>>
        CreateExistingGames(
        IFixture fixture,
        WriteReadDatabaseContext writeReadDbContext)
    {
        var existingGameConsoles = fixture.Build<GameConsole>()
            .Without(c => c.Games)
            .CreateMany()
            .ToArray();
        var existingGames = existingGameConsoles.SelectMany(c =>
            fixture.Build<Game>()
                .With(g => g.GameConsoleId, c.Id)
                .Without(g => g.GameConsole)
                .CreateMany())
            .ToArray();

        await writeReadDbContext.AddRangeAsync(existingGameConsoles);
        await writeReadDbContext.AddRangeAsync(existingGames);
        await writeReadDbContext.SaveChangesAsync();

        writeReadDbContext.ChangeTracker.Clear();

        return existingGames;
    }

    public sealed class GetGame : RepositoryTestsBase<GameWriteRepository>
    {
        [Fact]
        public async Task Should_get_game_by_id()
        {
            // Arrange
            var existingGames = await CreateExistingGames(Fixture, WriteReadDbContext);
            var gameToFind = existingGames.First();

            // Act
            var actual = await RepositoryUnderTesting.GetGame(gameToFind.Id, default);

            // Assert
            actual.Should().BeEquivalentTo(gameToFind, opts => opts
                .Excluding(e => e.GameConsole));
        }
    }

    public sealed class AddGameToConsole : RepositoryTestsBase<GameWriteRepository>
    {
        [Fact]
        public async Task Should_add_game_to_console()
        {
            // Arrange
            var existingGameConsole = Fixture.Build<GameConsole>()
                .Without(c => c.Games)
                .Create();
            var newGame = Fixture.Build<Game>()
                .With(g => g.GameConsoleId, existingGameConsole.Id)
                .Without(g => g.GameConsole)
                .Create();
            await WriteReadDbContext.GameConsoles.AddAsync(existingGameConsole);
            await WriteReadDbContext.SaveChangesAsync();

            var expected = newGame;

            // Act
            await RepositoryUnderTesting.AddGame(newGame, default);

            // Assert
            var actualWriteReadDbContext = await WriteReadDbContext.Games.SingleAsync();
            actualWriteReadDbContext.Should().BeEquivalentTo(expected, opts => opts.Excluding(e => e.GameConsole));
        }
    }

    public sealed class UpdateGame : RepositoryTestsBase<GameWriteRepository>
    {
        [Fact]
        public async Task Should_update_game()
        {
            // Arrange
            var existingGames = await CreateExistingGames(Fixture, WriteReadDbContext);

            var existingGameToUpdate = existingGames.First();
            var updatedGame = Fixture.Build<Game>()
                .With(g => g.Id, existingGameToUpdate.Id)
                .Without(g => g.GameConsole)
                .Create();

            var expected = updatedGame;

            // Act
            await RepositoryUnderTesting.UpdateGame(updatedGame, default);

            // Assert
            var actualWriteRead = await WriteReadDbContext.Games.FirstAsync();
            actualWriteRead.Should().BeEquivalentTo(expected, opts => opts.Excluding(e => e.GameConsole));
        }
    }

    public sealed class DeleteGame : RepositoryTestsBase<GameWriteRepository>
    {
        [Fact]
        public async Task Should_delete_game()
        {
            var existingGames = await CreateExistingGames(Fixture, WriteReadDbContext);

            var gameToDelete = existingGames.First();
            await RepositoryUnderTesting.DeleteGame(gameToDelete, default);

            var actualWriteRead = await WriteReadDbContext.Games.Where(g => g.Id == gameToDelete.Id).ToArrayAsync();
            actualWriteRead.Should().BeEmpty();
        }
    }
}

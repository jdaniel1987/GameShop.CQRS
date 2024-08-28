using AutoFixture;
using FluentAssertions;
using GameShop.Domain.Entities;
using GameShop.Infrastructure.Data;
using GameShop.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Infrastructure.UnitTests.Repositories;

public class GameRepositoryTests
{
    public static async Task<IReadOnlyCollection<Game>>
        CreateExistingGames(
        IFixture fixture,
        WriteReadDatabaseContext writeReadDbContext)
    {
        var existingGamesConsoles = fixture.Build<GamesConsole>()
            .Without(c => c.Games)
            .CreateMany()
            .ToArray();
        var existingGames = existingGamesConsoles.SelectMany(c =>
            fixture.Build<Game>()
                .With(g => g.GamesConsoleId, c.Id)
                .Without(g => g.GamesConsole)
                .CreateMany())
            .ToArray();

        await writeReadDbContext.AddRangeAsync(existingGamesConsoles);
        await writeReadDbContext.AddRangeAsync(existingGames);
        await writeReadDbContext.SaveChangesAsync();

        writeReadDbContext.ChangeTracker.Clear();

        return existingGames;
    }

    public sealed class GetAllGames : RepositoryTestsBase<GameRepository>
    {
        [Fact]
        public async Task Should_get_games()
        {
            // Arrange
            var existingGames = await CreateExistingGames(Fixture, WriteReadDbContext);
            var expected = existingGames;

            // Act
            var actual = await RepositoryUnderTesting.GetAllGames(default);

            // Assert
            actual.Should().BeEquivalentTo(expected, opts => opts
                .Excluding(e => e.GamesConsole!.Games));
        }
    }

    public sealed class GetAllGamesForConsole : RepositoryTestsBase<GameRepository>
    {
        [Fact]
        public async Task Should_get_games_for_console()
        {
            // Arrange
            var existingGames = await CreateExistingGames(Fixture, WriteReadDbContext);

            var gamesConsoleToFind = existingGames.First();
            var expected = existingGames.Where(g => g.GamesConsoleId == gamesConsoleToFind.GamesConsoleId);

            // Act
            var actual = await RepositoryUnderTesting.GetGamesForConsole(gamesConsoleToFind.GamesConsoleId, default);

            //Assert
            actual.Should().BeEquivalentTo(expected, opts => opts
                .Excluding(e => e.GamesConsole!.Games));
        }
    }

    public sealed class GetGamesByName : RepositoryTestsBase<GameRepository>
    {
        [Fact]
        public async Task Should_get_games_by_name()
        {
            // Arrange
            var existingGames = await CreateExistingGames(Fixture, WriteReadDbContext);
            var nameToFind = Fixture.Create<string>();
            var game1ToFind = Fixture.Build<Game>()
                .With(g => g.GamesConsoleId, existingGames.First().GamesConsoleId)
                .With(g => g.Name, $"{Fixture.Create<string>()}{nameToFind}{Fixture.Create<string>()}")
                .Without(g => g.GamesConsole)
                .Create();
            var game2ToFind = Fixture.Build<Game>()
                .With(g => g.GamesConsoleId, existingGames.First().GamesConsoleId)
                .With(g => g.Name, $"{Fixture.Create<string>()}{nameToFind}{Fixture.Create<string>()}")
                .Without(g => g.GamesConsole)
                .Create();
            await WriteReadDbContext.Games.AddAsync(game1ToFind);
            await WriteReadDbContext.Games.AddAsync(game2ToFind);
            await WriteReadDbContext.SaveChangesAsync();

            var expected = new[] { game1ToFind, game2ToFind };

            // Act
            var actual = await RepositoryUnderTesting.GetGamesByName(nameToFind, default);

            // Assert
            actual.Should().BeEquivalentTo(expected, opts => opts
                .Excluding(e => e.GamesConsole));
        }
    }

    public sealed class AddGameToConsole : RepositoryTestsBase<GameRepository>
    {
        [Fact]
        public async Task Should_add_game_to_console()
        {
            // Arrange
            var existingGamesConsole = Fixture.Build<GamesConsole>()
                .Without(c => c.Games)
                .Create();
            var newGame = Fixture.Build<Game>()
                .With(g => g.GamesConsoleId, existingGamesConsole.Id)
                .Without(g => g.GamesConsole)
                .Create();
            await WriteReadDbContext.GamesConsoles.AddAsync(existingGamesConsole);
            await WriteReadDbContext.SaveChangesAsync();

            var expected = newGame;

            // Act
            await RepositoryUnderTesting.AddGame(newGame, default);

            // Assert
            var actualReadOnlyDbContext = await ReadOnlyDbContext.Games.SingleAsync();
            var actualWriteReadDbContext = await WriteReadDbContext.Games.SingleAsync();
            actualReadOnlyDbContext.Should().BeEquivalentTo(expected, opts => opts.Excluding(e => e.GamesConsole));
            actualWriteReadDbContext.Should().BeEquivalentTo(expected, opts => opts.Excluding(e => e.GamesConsole));
        }
    }

    public sealed class UpdateGame : RepositoryTestsBase<GameRepository>
    {
        [Fact]
        public async Task Should_update_game()
        {
            // Arrange
            var existingGames = await CreateExistingGames(Fixture, WriteReadDbContext);

            var existingGameToUpdate = existingGames.First();
            var updatedGame = Fixture.Build<Game>()
                .With(g => g.Id, existingGameToUpdate.Id)
                .Without(g => g.GamesConsole)
                .Create();

            var expected = updatedGame;

            // Act
            await RepositoryUnderTesting.UpdateGame(updatedGame, default);

            // Assert
            var actualReadOnly = await ReadOnlyDbContext.Games.FirstAsync();
            var actualWriteRead = await ReadOnlyDbContext.Games.FirstAsync();
            actualReadOnly.Should().BeEquivalentTo(expected, opts => opts.Excluding(e => e.GamesConsole));
            actualWriteRead.Should().BeEquivalentTo(expected, opts => opts.Excluding(e => e.GamesConsole));
        }
    }

    public sealed class DeleteGame : RepositoryTestsBase<GameRepository>
    {
        [Fact]
        public async Task Should_delete_game()
        {
            var existingGames = await CreateExistingGames(Fixture, WriteReadDbContext);

            var gameToDelete = existingGames.First();
            await RepositoryUnderTesting.DeleteGame(gameToDelete, default);

            var actualReadOnly = await ReadOnlyDbContext.Games.Where(g => g.Id == gameToDelete.Id).ToArrayAsync();
            var actualWriteRead = await ReadOnlyDbContext.Games.Where(g => g.Id == gameToDelete.Id).ToArrayAsync();
            actualReadOnly.Should().BeEmpty();
            actualWriteRead.Should().BeEmpty();
        }
    }
}

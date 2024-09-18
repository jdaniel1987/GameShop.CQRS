using GameShop.Domain.Entities;
using GameShop.Infrastructure.Read.Data;
using GameShop.Infrastructure.Read.Repositories;

namespace GameShop.Infrastructure.Read.UnitTests.Repositories;

public sealed class GameReadRepositoryTests
{
    public static async Task<IReadOnlyCollection<Game>>
        CreateExistingGames(
        IFixture fixture,
        ReadOnlyDatabaseContext readOnlyDbContext)
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

        await readOnlyDbContext.AddRangeAsync(existingGameConsoles);
        await readOnlyDbContext.AddRangeAsync(existingGames);
        await readOnlyDbContext.SaveChangesAsync();

        readOnlyDbContext.ChangeTracker.Clear();

        return existingGames;
    }

    public sealed class GetAllGames : RepositoryTestsBase<GameReadRepository>
    {
        [Fact]
        public async Task Should_get_games()
        {
            // Arrange
            var existingGames = await CreateExistingGames(Fixture, ReadOnlyDbContext);
            var expected = existingGames;

            // Act
            var actual = await RepositoryUnderTesting.GetAllGames(default);

            // Assert
            actual.Should().BeEquivalentTo(expected, opts => opts
                .Excluding(e => e.GameConsole!.Games));
        }
    }

    public sealed class GetAllGamesForConsole : RepositoryTestsBase<GameReadRepository>
    {
        [Fact]
        public async Task Should_get_games_for_console()
        {
            // Arrange
            var existingGames = await CreateExistingGames(Fixture, ReadOnlyDbContext);

            var gameConsoleToFind = existingGames.First();
            var expected = existingGames.Where(g => g.GameConsoleId == gameConsoleToFind.GameConsoleId);

            // Act
            var actual = await RepositoryUnderTesting.GetGamesForConsole(gameConsoleToFind.GameConsoleId, default);

            //Assert
            actual.Should().BeEquivalentTo(expected, opts => opts
                .Excluding(e => e.GameConsole!.Games));
        }
    }

    public sealed class GetGamesByName : RepositoryTestsBase<GameReadRepository>
    {
        [Fact]
        public async Task Should_get_games_by_name()
        {
            // Arrange
            var existingGames = await CreateExistingGames(Fixture, ReadOnlyDbContext);
            var nameToFind = Fixture.Create<string>();
            var game1ToFind = Fixture.Build<Game>()
                .With(g => g.GameConsoleId, existingGames.First().GameConsoleId)
                .With(g => g.Name, $"{Fixture.Create<string>()}{nameToFind}{Fixture.Create<string>()}")
                .Without(g => g.GameConsole)
                .Create();
            var game2ToFind = Fixture.Build<Game>()
                .With(g => g.GameConsoleId, existingGames.First().GameConsoleId)
                .With(g => g.Name, $"{Fixture.Create<string>()}{nameToFind}{Fixture.Create<string>()}")
                .Without(g => g.GameConsole)
                .Create();
            await ReadOnlyDbContext.Games.AddAsync(game1ToFind);
            await ReadOnlyDbContext.Games.AddAsync(game2ToFind);
            await ReadOnlyDbContext.SaveChangesAsync();

            var expected = new[] { game1ToFind, game2ToFind };

            // Act
            var actual = await RepositoryUnderTesting.GetGamesByName(nameToFind, default);

            // Assert
            actual.Should().BeEquivalentTo(expected, opts => opts
                .Excluding(e => e.GameConsole));
        }
    }
}

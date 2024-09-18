using GameShop.Domain.Entities;
using GameShop.Infrastructure.Read.Data;
using GameShop.Infrastructure.Read.Repositories;

namespace GameShop.Infrastructure.Read.UnitTests.Repositories;

public sealed class GameConsoleReadRepositoryTests
{
    public static async Task<IReadOnlyCollection<GameConsole>>
        CreateExistingGameConsoles(
        IFixture fixture,
        ReadOnlyDatabaseContext readOnlyDbContext)
    {
        var existingGameConsoles = fixture.Build<GameConsole>()
            .Without(gc => gc.Games)
            .CreateMany()
            .ToArray();

        await readOnlyDbContext.AddRangeAsync(existingGameConsoles);
        await readOnlyDbContext.SaveChangesAsync();

        readOnlyDbContext.ChangeTracker.Clear();

        return existingGameConsoles;
    }

    public sealed class GetAllGameConsoles : RepositoryTestsBase<GameConsoleReadRepository>
    {
        [Fact]
        public async Task Should_get_all_game_consoles()
        {
            // Arrange
            var existingGameConsoles = await CreateExistingGameConsoles(Fixture, ReadOnlyDbContext);
            var expected = existingGameConsoles;

            // Act
            var actual = await RepositoryUnderTesting.GetAllGameConsoles(default);

            // Assert
            actual.Should().BeEquivalentTo(expected, opts => opts
                .Excluding(e => e.Games));
        }
    }

    public sealed class GetGameConsole : RepositoryTestsBase<GameConsoleReadRepository>
    {
        [Fact]
        public async Task Should_get_game_console_by_id()
        {
            // Arrange
            var existingGameConsoles = await CreateExistingGameConsoles(Fixture, ReadOnlyDbContext);
            var gameConsoleToFind = existingGameConsoles.First();

            // Act
            var actual = await RepositoryUnderTesting.GetGameConsole(gameConsoleToFind.Id, default);

            // Assert
            actual.Should().BeEquivalentTo(gameConsoleToFind, opts => opts
                .Excluding(e => e.Games));
        }
    }
}

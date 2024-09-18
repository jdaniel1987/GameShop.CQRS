using GameShop.Domain.Entities;
using GameShop.Infrastructure.Write.Data;
using GameShop.Infrastructure.Write.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Infrastructure.Write.UnitTests.Repositories;

public sealed class GameConsoleWriteRepositoryTests
{
    public static async Task<IReadOnlyCollection<GameConsole>>
        CreateExistingGameConsoles(
        IFixture fixture,
        WriteReadDatabaseContext writeReadDbContext)
    {
        var existingGameConsoles = fixture.Build<GameConsole>()
            .Without(gc => gc.Games)
            .CreateMany()
            .ToArray();

        await writeReadDbContext.AddRangeAsync(existingGameConsoles);
        await writeReadDbContext.SaveChangesAsync();

        writeReadDbContext.ChangeTracker.Clear();

        return existingGameConsoles;
    }

    public sealed class GetGameConsole : RepositoryTestsBase<GameConsoleWriteRepository>
    {
        [Fact]
        public async Task Should_get_game_console_by_id()
        {
            // Arrange
            var existingGameConsoles = await CreateExistingGameConsoles(Fixture, WriteReadDbContext);
            var gameConsoleToFind = existingGameConsoles.First();

            // Act
            var actual = await RepositoryUnderTesting.GetGameConsole(gameConsoleToFind.Id, default);

            // Assert
            actual.Should().BeEquivalentTo(gameConsoleToFind, opts => opts
                .Excluding(e => e.Games));
        }
    }

    public sealed class AddGameConsole : RepositoryTestsBase<GameConsoleWriteRepository>
    {
        [Fact]
        public async Task Should_add_game_console()
        {
            // Arrange
            var newGameConsole = Fixture.Build<GameConsole>()
                .Without(gc => gc.Games)
                .Create();
            var expected = newGameConsole;

            // Act
            await RepositoryUnderTesting.AddGameConsole(newGameConsole, default);

            // Assert
            var actualWriteReadDbContext = await WriteReadDbContext.GameConsoles.SingleAsync();
            actualWriteReadDbContext.Should().BeEquivalentTo(expected, opts => opts.Excluding(e => e.Games));
        }
    }

    public sealed class UpdateGameConsole : RepositoryTestsBase<GameConsoleWriteRepository>
    {
        [Fact]
        public async Task Should_update_game_console()
        {
            // Arrange
            var existingGameConsoles = await CreateExistingGameConsoles(Fixture, WriteReadDbContext);
            var existingGameConsoleToUpdate = existingGameConsoles.First();
            var updatedGameConsole = Fixture.Build<GameConsole>()
                .Without(gc => gc.Games)
                .With(gc => gc.Id, existingGameConsoleToUpdate.Id)
                .Create();

            var expected = updatedGameConsole;

            // Act
            await RepositoryUnderTesting.UpdateGameConsole(updatedGameConsole, default);

            // Assert
            var actualWriteRead = await WriteReadDbContext.GameConsoles.FirstAsync();
            actualWriteRead.Should().BeEquivalentTo(expected, opts => opts.Excluding(e => e.Games));
        }
    }

    public sealed class DeleteGameConsole : RepositoryTestsBase<GameConsoleWriteRepository>
    {
        [Fact]
        public async Task Should_delete_game_console()
        {
            // Arrange
            var existingGameConsoles = await CreateExistingGameConsoles(Fixture, WriteReadDbContext);
            var gameConsoleToDelete = existingGameConsoles.First();

            // Act
            await RepositoryUnderTesting.DeleteGameConsole(gameConsoleToDelete, default);

            // Assert
            var actualWriteRead = await WriteReadDbContext.GameConsoles.Where(gc => gc.Id == gameConsoleToDelete.Id).ToArrayAsync();
            actualWriteRead.Should().BeEmpty();
        }
    }
}

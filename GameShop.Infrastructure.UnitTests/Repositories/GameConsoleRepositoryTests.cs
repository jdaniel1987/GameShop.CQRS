using GameShop.Domain.Entities;
using GameShop.Infrastructure.Data;
using GameShop.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Infrastructure.UnitTests.Repositories;

public sealed class GameConsoleRepositoryTests
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

    public sealed class GetAllGameConsoles : RepositoryTestsBase<GameConsoleRepository>
    {
        [Fact]
        public async Task Should_get_all_game_consoles()
        {
            // Arrange
            var existingGameConsoles = await CreateExistingGameConsoles(Fixture, WriteReadDbContext);
            var expected = existingGameConsoles;

            // Act
            var actual = await RepositoryUnderTesting.GetAllGameConsoles(default);

            // Assert
            actual.Should().BeEquivalentTo(expected, opts => opts
                .Excluding(e => e.Games));
        }
    }

    public sealed class GetGameConsole : RepositoryTestsBase<GameConsoleRepository>
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

    public sealed class AddGameConsole : RepositoryTestsBase<GameConsoleRepository>
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
            var actualReadOnlyDbContext = await ReadOnlyDbContext.GameConsoles.SingleAsync();
            var actualWriteReadDbContext = await WriteReadDbContext.GameConsoles.SingleAsync();
            actualReadOnlyDbContext.Should().BeEquivalentTo(expected, opts => opts.Excluding(e => e.Games));
            actualWriteReadDbContext.Should().BeEquivalentTo(expected, opts => opts.Excluding(e => e.Games));
        }
    }

    public sealed class UpdateGameConsole : RepositoryTestsBase<GameConsoleRepository>
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
            var actualReadOnly = await ReadOnlyDbContext.GameConsoles.FirstAsync();
            var actualWriteRead = await WriteReadDbContext.GameConsoles.FirstAsync();
            actualReadOnly.Should().BeEquivalentTo(expected, opts => opts.Excluding(e => e.Games));
            actualWriteRead.Should().BeEquivalentTo(expected, opts => opts.Excluding(e => e.Games));
        }
    }

    public sealed class DeleteGameConsole : RepositoryTestsBase<GameConsoleRepository>
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
            var actualReadOnly = await ReadOnlyDbContext.GameConsoles.Where(gc => gc.Id == gameConsoleToDelete.Id).ToArrayAsync();
            var actualWriteRead = await WriteReadDbContext.GameConsoles.Where(gc => gc.Id == gameConsoleToDelete.Id).ToArrayAsync();
            actualReadOnly.Should().BeEmpty();
            actualWriteRead.Should().BeEmpty();
        }
    }
}

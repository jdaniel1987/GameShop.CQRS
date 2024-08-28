//using AutoFixture;
//using AutoFixture.Xunit2;
//using CleanArchitecture.Infrastructure.Repositories;
//using FluentAssertions;
//using Microsoft.EntityFrameworkCore;
//using GameConsoleDomain = CleanArchitecture.Domain.Entities.GameConsole;
//using GameConsoleModel = CleanArchitecture.Infrastructure.Models.GameConsole;

//namespace GameShop.Infrastructure.UnitTests.Repositories;

//public class GameConsoleRepositoryTests
//{
//    public sealed class GetAllGameConsoles : RepositoryTestsBase<GameConsoleRepository>
//    {
//        [Theory, AutoData]
//        public async Task Should_get_games_consoles(
//        IReadOnlyCollection<GameConsoleDomain> existingGameConsoles)
//        {
//            var existingGameModels = Mapper.Map<IReadOnlyCollection<GameConsoleModel>>(existingGameConsoles);
//            await DatabaseContext.GameConsoles.AddRangeAsync(existingGameModels);
//            await DatabaseContext.SaveChangesAsync();

//            var actual = await RepositoryUnderTesting.GetAllGameConsoles();

//            actual.Should().BeEquivalentTo(existingGameConsoles);
//        }
//    }

//    public sealed class GetGameConsole : RepositoryTestsBase<GameConsoleRepository>
//    {
//        [Theory, AutoData]
//        public async Task Should_get_games_console_by_id(
//            IReadOnlyCollection<GameConsoleDomain> existingGameConsoles)
//        {
//            var existingGameModels = Mapper.Map<IReadOnlyCollection<GameConsoleModel>>(existingGameConsoles);
//            await DatabaseContext.GameConsoles.AddRangeAsync(existingGameModels);
//            await DatabaseContext.SaveChangesAsync();

//            var expectedDomain = existingGameConsoles.First();
//            var actual = await RepositoryUnderTesting.GetGameConsole(expectedDomain.Id);
//            actual.Should().BeEquivalentTo(expectedDomain);
//        }
//    }

//    public sealed class UpdateGameConsole : RepositoryTestsBase<GameConsoleRepository>
//    {
//        [Theory, AutoData]
//        public async Task Should_update_games_console(
//        GameConsoleDomain updatedGameConsole)
//        {
//            var existingGameConsole = Fixture.Build<GameConsoleModel>()
//                .Without(c => c.Games)
//                .Create();
//            await DatabaseContext.GameConsoles.AddAsync(existingGameConsole);
//            await DatabaseContext.SaveChangesAsync();

//            var updatedGameConsoleFixed = updatedGameConsole with { Id = existingGameConsole.Id };
//            await RepositoryUnderTesting.UpdateGameConsole(updatedGameConsoleFixed);

//            var expected = Mapper.Map<GameConsoleModel>(updatedGameConsoleFixed);
//            var actual = await DatabaseContext.GameConsoles.SingleAsync();
//            actual.Should().BeEquivalentTo(expected);
//        }
//    }

//    public sealed class AddGameConsole : RepositoryTestsBase<GameConsoleRepository>
//    {
//        [Theory, AutoData]
//        public async Task Should_add_games_console(
//        GameConsoleDomain newGameConsoleDomain)
//        {
//            var expected = Mapper.Map<GameConsoleModel>(newGameConsoleDomain);

//            await RepositoryUnderTesting.AddGameConsole(newGameConsoleDomain);

//            var actual = await DatabaseContext.GameConsoles.SingleAsync();
//            actual.Should().BeEquivalentTo(expected);
//        }
//    }

//    public sealed class DeleteGameConsole : RepositoryTestsBase<GameConsoleRepository>
//    {
//        [Fact]
//        public async Task Should_delete_games_console()
//        {
//            var existingGameConsole = Fixture.Build<GameConsoleModel>()
//                .Without(c => c.Games)
//                .Create();
//            await DatabaseContext.GameConsoles.AddAsync(existingGameConsole);
//            await DatabaseContext.SaveChangesAsync();

//            await RepositoryUnderTesting.DeleteGameConsole(existingGameConsole.Id);

//            var actual = await DatabaseContext.GameConsoles.FirstOrDefaultAsync();
//            actual.Should().BeNull();
//        }
//    }
//}

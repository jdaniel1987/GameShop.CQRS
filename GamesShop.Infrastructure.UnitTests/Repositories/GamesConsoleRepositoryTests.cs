//using AutoFixture;
//using AutoFixture.Xunit2;
//using CleanArchitecture.Infrastructure.Repositories;
//using FluentAssertions;
//using Microsoft.EntityFrameworkCore;
//using GamesConsoleDomain = CleanArchitecture.Domain.Entities.GamesConsole;
//using GamesConsoleModel = CleanArchitecture.Infrastructure.Models.GamesConsole;

//namespace GamesShop.Infrastructure.UnitTests.Repositories;

//public class GamesConsoleRepositoryTests
//{
//    public sealed class GetAllGamesConsoles : RepositoryTestsBase<GamesConsoleRepository>
//    {
//        [Theory, AutoData]
//        public async Task Should_get_games_consoles(
//        IReadOnlyCollection<GamesConsoleDomain> existingGamesConsoles)
//        {
//            var existingGameModels = Mapper.Map<IReadOnlyCollection<GamesConsoleModel>>(existingGamesConsoles);
//            await DatabaseContext.GamesConsoles.AddRangeAsync(existingGameModels);
//            await DatabaseContext.SaveChangesAsync();

//            var actual = await RepositoryUnderTesting.GetAllGamesConsoles();

//            actual.Should().BeEquivalentTo(existingGamesConsoles);
//        }
//    }

//    public sealed class GetGamesConsole : RepositoryTestsBase<GamesConsoleRepository>
//    {
//        [Theory, AutoData]
//        public async Task Should_get_games_console_by_id(
//            IReadOnlyCollection<GamesConsoleDomain> existingGamesConsoles)
//        {
//            var existingGameModels = Mapper.Map<IReadOnlyCollection<GamesConsoleModel>>(existingGamesConsoles);
//            await DatabaseContext.GamesConsoles.AddRangeAsync(existingGameModels);
//            await DatabaseContext.SaveChangesAsync();

//            var expectedDomain = existingGamesConsoles.First();
//            var actual = await RepositoryUnderTesting.GetGamesConsole(expectedDomain.Id);
//            actual.Should().BeEquivalentTo(expectedDomain);
//        }
//    }

//    public sealed class UpdateGamesConsole : RepositoryTestsBase<GamesConsoleRepository>
//    {
//        [Theory, AutoData]
//        public async Task Should_update_games_console(
//        GamesConsoleDomain updatedGamesConsole)
//        {
//            var existingGamesConsole = Fixture.Build<GamesConsoleModel>()
//                .Without(c => c.Games)
//                .Create();
//            await DatabaseContext.GamesConsoles.AddAsync(existingGamesConsole);
//            await DatabaseContext.SaveChangesAsync();

//            var updatedGamesConsoleFixed = updatedGamesConsole with { Id = existingGamesConsole.Id };
//            await RepositoryUnderTesting.UpdateGamesConsole(updatedGamesConsoleFixed);

//            var expected = Mapper.Map<GamesConsoleModel>(updatedGamesConsoleFixed);
//            var actual = await DatabaseContext.GamesConsoles.SingleAsync();
//            actual.Should().BeEquivalentTo(expected);
//        }
//    }

//    public sealed class AddGamesConsole : RepositoryTestsBase<GamesConsoleRepository>
//    {
//        [Theory, AutoData]
//        public async Task Should_add_games_console(
//        GamesConsoleDomain newGamesConsoleDomain)
//        {
//            var expected = Mapper.Map<GamesConsoleModel>(newGamesConsoleDomain);

//            await RepositoryUnderTesting.AddGamesConsole(newGamesConsoleDomain);

//            var actual = await DatabaseContext.GamesConsoles.SingleAsync();
//            actual.Should().BeEquivalentTo(expected);
//        }
//    }

//    public sealed class DeleteGamesConsole : RepositoryTestsBase<GamesConsoleRepository>
//    {
//        [Fact]
//        public async Task Should_delete_games_console()
//        {
//            var existingGamesConsole = Fixture.Build<GamesConsoleModel>()
//                .Without(c => c.Games)
//                .Create();
//            await DatabaseContext.GamesConsoles.AddAsync(existingGamesConsole);
//            await DatabaseContext.SaveChangesAsync();

//            await RepositoryUnderTesting.DeleteGamesConsole(existingGamesConsole.Id);

//            var actual = await DatabaseContext.GamesConsoles.FirstOrDefaultAsync();
//            actual.Should().BeNull();
//        }
//    }
//}

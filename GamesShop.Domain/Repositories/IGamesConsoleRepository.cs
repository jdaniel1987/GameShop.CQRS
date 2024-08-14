using GamesShop.Domain.Entities;

namespace GamesShop.Domain.Repositories;

public interface IGamesConsoleRepository
{
    Task<IReadOnlyCollection<GamesConsole>> GetAllGamesConsoles(CancellationToken cancellationToken);
    Task<GamesConsole?> GetGamesConsole(int gamesConsoleId, CancellationToken cancellationToken);
    Task UpdateGamesConsole(GamesConsole gamesConsole, CancellationToken cancellationToken);
    Task AddGamesConsole(GamesConsole gamesConsole, CancellationToken cancellationToken);
    Task DeleteGamesConsole(GamesConsole gamesConsole, CancellationToken cancellationToken);
}

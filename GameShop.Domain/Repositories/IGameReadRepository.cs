using GameShop.Domain.Entities;

namespace GameShop.Domain.Repositories;

public interface IGameReadRepository
{
    Task<IReadOnlyCollection<Game>> GetAllGames(CancellationToken cancellationToken);
    Task<IReadOnlyCollection<Game>> GetGamesForConsole(int gameConsoleId, CancellationToken cancellationToken);
    Task<Game?> GetGame(int gameId, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<Game>> GetGamesByName(string gameName, CancellationToken cancellationToken);
}

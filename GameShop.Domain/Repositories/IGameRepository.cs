using GameShop.Domain.Entities;

namespace GameShop.Domain.Repositories;

public interface IGameRepository
{
    Task<IReadOnlyCollection<Game>> GetAllGames(CancellationToken cancellationToken);
    Task<IReadOnlyCollection<Game>> GetGamesForConsole(int gameConsoleId, CancellationToken cancellationToken);
    Task<Game?> GetGame(int gameId, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<Game>> GetGamesByName(string gameName, CancellationToken cancellationToken);
    Task AddGame(Game game, CancellationToken cancellationToken);
    Task DeleteGame(Game game, CancellationToken cancellationToken);
    Task UpdateGame(Game game, CancellationToken cancellationToken);
}

using GameShop.Domain.Entities;

namespace GameShop.Domain.Repositories;

public interface IGameConsoleRepository
{
    Task<IReadOnlyCollection<GameConsole>> GetAllGameConsoles(CancellationToken cancellationToken);
    Task<GameConsole?> GetGameConsole(int gameConsoleId, CancellationToken cancellationToken);
    Task UpdateGameConsole(GameConsole gameConsole, CancellationToken cancellationToken);
    Task AddGameConsole(GameConsole gameConsole, CancellationToken cancellationToken);
    Task DeleteGameConsole(GameConsole gameConsole, CancellationToken cancellationToken);
}

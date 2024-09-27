using GameShop.Domain.Entities;

namespace GameShop.Domain.Repositories;

public interface IGameConsoleWriteRepository
{
    Task<GameConsole?> GetGameConsole(int id, CancellationToken cancellationToken);
    Task UpdateGameConsole(GameConsole gameConsole, CancellationToken cancellationToken);
    Task AddGameConsole(GameConsole gameConsole, CancellationToken cancellationToken);
    Task DeleteGameConsole(GameConsole gameConsole, CancellationToken cancellationToken);
}

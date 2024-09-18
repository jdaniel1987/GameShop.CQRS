using GameShop.Domain.Entities;

namespace GameShop.Domain.Repositories;

public interface IGameConsoleReadRepository
{
    Task<IReadOnlyCollection<GameConsole>> GetAllGameConsoles(CancellationToken cancellationToken);
    Task<GameConsole?> GetGameConsole(int gameConsoleId, CancellationToken cancellationToken);
}

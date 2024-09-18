using GameShop.Domain.Entities;

namespace GameShop.Domain.Repositories;

public interface IGameWriteRepository
{
    Task<Game?> GetGame(int id, CancellationToken cancellationToken);
    Task AddGame(Game game, CancellationToken cancellationToken);
    Task DeleteGame(Game game, CancellationToken cancellationToken);
    Task UpdateGame(Game game, CancellationToken cancellationToken);
}

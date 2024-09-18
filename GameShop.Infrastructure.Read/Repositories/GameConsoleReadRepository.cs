using Microsoft.EntityFrameworkCore;
using GameShop.Domain.Entities;
using GameShop.Domain.Repositories;
using GameShop.Infrastructure.Read.Data;

namespace GameShop.Infrastructure.Read.Repositories;

public class GameConsoleReadRepository(
    IDbContextFactory<ReadOnlyDatabaseContext> readOnlyDbContextFactory) : IGameConsoleReadRepository
{
    private readonly IDbContextFactory<ReadOnlyDatabaseContext> _readOnlyDbContextFactory = readOnlyDbContextFactory;

    public async Task<IReadOnlyCollection<GameConsole>> GetAllGameConsoles(CancellationToken cancellationToken)
    {
        var readOnlyDbContext = await _readOnlyDbContextFactory.CreateDbContextAsync(cancellationToken);

        return await readOnlyDbContext
            .GameConsoles
            .ToArrayAsync(cancellationToken);
    }

    public async Task<GameConsole?> GetGameConsole(int gameConsoleId, CancellationToken cancellationToken)
    {
        var readOnlyDbContext = await _readOnlyDbContextFactory.CreateDbContextAsync(cancellationToken);

        return await readOnlyDbContext
            .GameConsoles
            .FirstOrDefaultAsync(c => c.Id == gameConsoleId, cancellationToken);
    }
}

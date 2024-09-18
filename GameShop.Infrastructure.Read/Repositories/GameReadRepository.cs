using Microsoft.EntityFrameworkCore;
using GameShop.Domain.Entities;
using GameShop.Domain.Repositories;
using GameShop.Infrastructure.Read.Data;

namespace GameShop.Infrastructure.Read.Repositories;

public class GameReadRepository(
    IDbContextFactory<ReadOnlyDatabaseContext> readOnlyDatabaseContextFactory) : IGameReadRepository
{
    private readonly IDbContextFactory<ReadOnlyDatabaseContext> _readOnlyDatabaseContextFactory = readOnlyDatabaseContextFactory;

    public async Task<IReadOnlyCollection<Game>> GetAllGames(CancellationToken cancellationToken)
    {
        using var readOnlyDbContext = await _readOnlyDatabaseContextFactory.CreateDbContextAsync(cancellationToken);

        return await readOnlyDbContext
            .Games
            .Include(g => g.GameConsole)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<Game>> GetGamesForConsole(int gameConsoleId, CancellationToken cancellationToken)
    {
        var readOnlyDbContext = await _readOnlyDatabaseContextFactory.CreateDbContextAsync(cancellationToken);
        var gameConsole = await readOnlyDbContext
            .GameConsoles
            .FirstOrDefaultAsync(c => c.Id == gameConsoleId, cancellationToken);

        return await readOnlyDbContext
            .Games
            .Include(g => g.GameConsole)
            .Where(x => x.GameConsoleId == gameConsoleId)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<Game?> GetGame(int gameId, CancellationToken cancellationToken)
    {
        var readOnlyDbContext = await _readOnlyDatabaseContextFactory.CreateDbContextAsync(cancellationToken);

        return await readOnlyDbContext
            .Games
            .Include(g => g.GameConsole)
            .FirstOrDefaultAsync(x => x.Id == gameId, cancellationToken);
    }

    public async Task<IReadOnlyCollection<Game>> GetGamesByName(string gameName, CancellationToken cancellationToken)
    {
        var readOnlyDbContext = await _readOnlyDatabaseContextFactory.CreateDbContextAsync(cancellationToken);

        return await readOnlyDbContext
            .Games
            .Include(g => g.GameConsole)
            .Where(g => EF.Functions.Like(g.Name, $"%{gameName}%"))
            .ToArrayAsync(cancellationToken);
    }    
}

using Microsoft.EntityFrameworkCore;
using GamesShop.Domain.Entities;
using GamesShop.Domain.Repositories;
using GamesShop.Infrastructure.Data;

namespace GamesShop.Infrastructure.Repositories;

public class GamesConsoleRepository(
    IDbContextFactory<ReadOnlyDatabaseContext> readOnlyDbContextFactory,
    IDbContextFactory<WriteReadDatabaseContext> writeReadDbContextFactory) : IGamesConsoleRepository
{
    private readonly IDbContextFactory<ReadOnlyDatabaseContext> _readOnlyDbContextFactory = readOnlyDbContextFactory;
    private readonly IDbContextFactory<WriteReadDatabaseContext> _writeReadDbContextFactory = writeReadDbContextFactory;

    public async Task<IReadOnlyCollection<GamesConsole>> GetAllGamesConsoles(CancellationToken cancellationToken)
    {
        var readOnlyDbContext = await _readOnlyDbContextFactory.CreateDbContextAsync(cancellationToken);

        return await readOnlyDbContext
            .GamesConsoles
            .ToArrayAsync(cancellationToken);
    }

    public async Task<GamesConsole?> GetGamesConsole(int gamesConsoleId, CancellationToken cancellationToken)
    {
        var readOnlyDbContext = await _readOnlyDbContextFactory.CreateDbContextAsync(cancellationToken);

        return await readOnlyDbContext
            .GamesConsoles
            .FirstOrDefaultAsync(c => c.Id == gamesConsoleId, cancellationToken);
    }

    public async Task AddGamesConsole(GamesConsole gamesConsole, CancellationToken cancellationToken)
    {
        var writeReadDbContext = await _writeReadDbContextFactory.CreateDbContextAsync(cancellationToken);
        await writeReadDbContext.AddAsync(gamesConsole, cancellationToken);
        await writeReadDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateGamesConsole(GamesConsole gamesConsole, CancellationToken cancellationToken)
    {
        var writeReadDbContext = await _writeReadDbContextFactory.CreateDbContextAsync(cancellationToken);

        writeReadDbContext.Update(gamesConsole);
        await writeReadDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteGamesConsole(GamesConsole gamesConsole, CancellationToken cancellationToken)
    {
       var writeReadDbContext = await _writeReadDbContextFactory.CreateDbContextAsync(cancellationToken);

        writeReadDbContext.Remove(gamesConsole);
        await writeReadDbContext.SaveChangesAsync(cancellationToken);
    }
}

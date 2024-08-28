using Microsoft.EntityFrameworkCore;
using GameShop.Domain.Entities;
using GameShop.Domain.Repositories;
using GameShop.Infrastructure.Data;

namespace GameShop.Infrastructure.Repositories;

public class GameConsoleRepository(
    IDbContextFactory<ReadOnlyDatabaseContext> readOnlyDbContextFactory,
    IDbContextFactory<WriteReadDatabaseContext> writeReadDbContextFactory) : IGameConsoleRepository
{
    private readonly IDbContextFactory<ReadOnlyDatabaseContext> _readOnlyDbContextFactory = readOnlyDbContextFactory;
    private readonly IDbContextFactory<WriteReadDatabaseContext> _writeReadDbContextFactory = writeReadDbContextFactory;

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

    public async Task AddGameConsole(GameConsole gameConsole, CancellationToken cancellationToken)
    {
        var writeReadDbContext = await _writeReadDbContextFactory.CreateDbContextAsync(cancellationToken);
        await writeReadDbContext.AddAsync(gameConsole, cancellationToken);
        await writeReadDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateGameConsole(GameConsole gameConsole, CancellationToken cancellationToken)
    {
        var writeReadDbContext = await _writeReadDbContextFactory.CreateDbContextAsync(cancellationToken);

        writeReadDbContext.Update(gameConsole);
        await writeReadDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteGameConsole(GameConsole gameConsole, CancellationToken cancellationToken)
    {
       var writeReadDbContext = await _writeReadDbContextFactory.CreateDbContextAsync(cancellationToken);

        writeReadDbContext.Remove(gameConsole);
        await writeReadDbContext.SaveChangesAsync(cancellationToken);
    }
}

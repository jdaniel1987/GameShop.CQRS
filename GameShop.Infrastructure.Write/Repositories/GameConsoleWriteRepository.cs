using Microsoft.EntityFrameworkCore;
using GameShop.Domain.Entities;
using GameShop.Domain.Repositories;
using GameShop.Infrastructure.Write.Data;

namespace GameShop.Infrastructure.Write.Repositories;

public class GameConsoleWriteRepository(
    IDbContextFactory<WriteReadDatabaseContext> writeReadDbContextFactory) : IGameConsoleWriteRepository
{
    private readonly IDbContextFactory<WriteReadDatabaseContext> _writeReadDbContextFactory = writeReadDbContextFactory;

    public async Task<GameConsole?> GetGameConsole(int id, CancellationToken cancellationToken)
    {
        var writeReadDbContext = await _writeReadDbContextFactory.CreateDbContextAsync(cancellationToken);

        return await writeReadDbContext.GameConsoles.SingleOrDefaultAsync(s => s.Id == id, cancellationToken);
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

using Microsoft.EntityFrameworkCore;
using GameShop.Domain.Entities;
using GameShop.Domain.Repositories;
using GameShop.Infrastructure.Write.Data;

namespace GameShop.Infrastructure.Write.Repositories;

public class GameWriteRepository(
    IDbContextFactory<WriteReadDatabaseContext> writeReadDatabaseContextFactory) : IGameWriteRepository
{
    private readonly IDbContextFactory<WriteReadDatabaseContext> _writeReadDbContextFactory = writeReadDatabaseContextFactory;

    public async Task<Game?> GetGame(int id, CancellationToken cancellationToken)
    {
        var writeReadDbContext = await _writeReadDbContextFactory.CreateDbContextAsync(cancellationToken);

        return await writeReadDbContext.Games.SingleOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task AddGame(Game game, CancellationToken cancellationToken)
    {
        var writeReadDbContext = await _writeReadDbContextFactory.CreateDbContextAsync(cancellationToken);

        await writeReadDbContext.Games.AddAsync(game, cancellationToken);
        await writeReadDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteGame(Game game, CancellationToken cancellationToken)
    {
        var writeReadDbContext = await _writeReadDbContextFactory.CreateDbContextAsync(cancellationToken);

        writeReadDbContext.Games.Remove(game);
        await writeReadDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateGame(Game game, CancellationToken cancellationToken)
    {
        var writeReadDbContext = await _writeReadDbContextFactory.CreateDbContextAsync(cancellationToken);

        writeReadDbContext.Update(game);
        await writeReadDbContext.SaveChangesAsync(cancellationToken);
    }
}

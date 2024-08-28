using Microsoft.EntityFrameworkCore;
using GameShop.Domain.Entities;
using GameShop.Domain.Repositories;
using GameShop.Infrastructure.Data;

namespace GameShop.Infrastructure.Repositories;

public class GameRepository(
    IDbContextFactory<ReadOnlyDatabaseContext> readOnlyDatabaseContextFactory,
    IDbContextFactory<WriteReadDatabaseContext> writeReadDatabaseContextFactory) : IGameRepository
{
    private readonly IDbContextFactory<ReadOnlyDatabaseContext> _readOnlyDatabaseContextFactory = readOnlyDatabaseContextFactory;
    private readonly IDbContextFactory<WriteReadDatabaseContext> _writeReadDatabaseContextFactory = writeReadDatabaseContextFactory;

    public async Task<IReadOnlyCollection<Game>> GetAllGames(CancellationToken cancellationToken)
    {
        using var readOnlyDbContext = await _readOnlyDatabaseContextFactory.CreateDbContextAsync(cancellationToken);
        
        return await readOnlyDbContext
            .Games
            .Include(g => g.GamesConsole)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<Game>> GetGamesForConsole(int gamesConsoleId, CancellationToken cancellationToken)
    {
        var readOnlyDbContext = await _readOnlyDatabaseContextFactory.CreateDbContextAsync(cancellationToken);
        var gameConsole = await readOnlyDbContext
            .GamesConsoles
            .FirstOrDefaultAsync(c => c.Id == gamesConsoleId, cancellationToken)
            ?? throw new Exception("Games console does not exist.");

        return await readOnlyDbContext
            .Games
            .Include(g => g.GamesConsole)
            .Where(x => x.GamesConsoleId == gamesConsoleId)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<Game?> GetGame(int gameId, CancellationToken cancellationToken)
    {
        var readOnlyDbContext = await _readOnlyDatabaseContextFactory.CreateDbContextAsync(cancellationToken);

        return await readOnlyDbContext
            .Games
            .Include(g => g.GamesConsole)
            .FirstOrDefaultAsync(x => x.Id == gameId, cancellationToken);
    }

    public async Task<IReadOnlyCollection<Game>> GetGamesByName(string gameName, CancellationToken cancellationToken)
    {
        var readOnlyDbContext = await _readOnlyDatabaseContextFactory.CreateDbContextAsync(cancellationToken);
        
        return await readOnlyDbContext
            .Games
            .Include(g => g.GamesConsole)
            .Where(g => EF.Functions.Like(g.Name, $"%{gameName}%"))
            .ToArrayAsync(cancellationToken);
    }

    public async Task AddGame(Game game, CancellationToken cancellationToken)
    {
        var writeReadDbContext = await _writeReadDatabaseContextFactory.CreateDbContextAsync(cancellationToken);

        await writeReadDbContext.Games.AddAsync(game, cancellationToken);
        await writeReadDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteGame(Game game, CancellationToken cancellationToken)
    {
        var writeReadDbContext = await _writeReadDatabaseContextFactory.CreateDbContextAsync(cancellationToken);

        writeReadDbContext.Games.Remove(game);
        await writeReadDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateGame(Game game, CancellationToken cancellationToken)
    {
        var writeReadDbContext = await _writeReadDatabaseContextFactory.CreateDbContextAsync(cancellationToken);

        writeReadDbContext.Update(game);
        await writeReadDbContext.SaveChangesAsync(cancellationToken);
    }
}

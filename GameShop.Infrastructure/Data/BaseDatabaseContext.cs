using GameShop.Domain.Entities;
using GameShop.Infrastructure.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Infrastructure.Data;

public class BaseDatabaseContext(DbContextOptions options) : DbContext(options)
{
    public virtual DbSet<GamesConsole> GamesConsoles { get; set; }
    public virtual DbSet<Game> Games { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GameConfiguration).Assembly);
    }
}

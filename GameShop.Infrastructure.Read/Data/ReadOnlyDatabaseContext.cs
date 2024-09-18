using GameShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Infrastructure.Read.Data;

public class ReadOnlyDatabaseContext(DbContextOptions<ReadOnlyDatabaseContext> options) : BaseDatabaseContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // No need to track entities, as we are not modifying them in this DbContext
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        base.OnConfiguring(optionsBuilder);
    }
}

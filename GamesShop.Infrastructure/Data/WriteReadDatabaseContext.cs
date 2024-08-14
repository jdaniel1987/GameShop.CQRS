using Microsoft.EntityFrameworkCore;

namespace GamesShop.Infrastructure.Data;

public class WriteReadDatabaseContext(DbContextOptions<WriteReadDatabaseContext> options) : BaseDatabaseContext(options)
{

}

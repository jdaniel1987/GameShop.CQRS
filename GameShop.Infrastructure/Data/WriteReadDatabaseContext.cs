using Microsoft.EntityFrameworkCore;

namespace GameShop.Infrastructure.Data;

public class WriteReadDatabaseContext(DbContextOptions<WriteReadDatabaseContext> options) : BaseDatabaseContext(options)
{

}

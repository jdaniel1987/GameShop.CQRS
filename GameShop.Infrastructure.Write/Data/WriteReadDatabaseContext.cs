using GameShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Infrastructure.Write.Data;

public class WriteReadDatabaseContext(DbContextOptions<WriteReadDatabaseContext> options) : BaseDatabaseContext(options)
{

}

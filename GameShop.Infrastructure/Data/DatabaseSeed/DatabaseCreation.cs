using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace GameShop.Infrastructure.Data.DatabaseSeed.DatabaseSeed;

public static class DatabaseCreation
{
    public static void CreateDatabase(IConfiguration configuration)
    {
        var databaseName = "GameShopExample";
        var connectionString = configuration.GetConnectionString("DbCreation");
        var scriptsDir = "../GameShop.Infrastructure/Data/DatabaseSeed/Scripts";

        var serverConnection = new ServerConnection(new SqlConnection(connectionString));
        var server = new Server(serverConnection);

        if (server.Databases.Contains(databaseName))
        {
            server.KillDatabase(databaseName);
        }

        var database = new Database(server, databaseName);
        database.Create();

        connectionString = configuration.GetConnectionString("DbTablesCreation");
        foreach (var scriptFile in Directory.GetFiles(scriptsDir, "*.sql", SearchOption.AllDirectories))
        {
            var script = File.ReadAllText(scriptFile);
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            var command = new SqlCommand(script, connection);
            command.ExecuteNonQuery();
        }
    }
}

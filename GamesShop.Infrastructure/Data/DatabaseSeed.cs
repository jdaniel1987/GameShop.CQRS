using GamesShop.Domain.Entities;
using GamesShop.Domain.ValueObjects;

namespace GamesShop.Infrastructure.Data;

public static class DatabaseSeed
{
    public static void SeedData(BaseDatabaseContext context)
    {
        var gamesConsole1 = new GamesConsole
        {
            Id = 1,
            Name = "PlayStation 4",
            Manufacturer = "Sony",
            Price = Price.Create(249),
            Games = new List<Game>(),
        }; 
        var gamesConsole2 = new GamesConsole
        {
            Id = 2,
            Name = "PlayStation 5",
            Manufacturer = "Sony",
            Price = Price.Create(549),
            Games = new List<Game>(),
        };
        var gamesConsole3 = new GamesConsole
        {
            Id = 3,
            Name = "Xbox Series X",
            Manufacturer = "Microsoft",
            Price = Price.Create(599),
            Games = new List<Game>(),
        };
        var gamesConsole4 = new GamesConsole
        {
            Id = 4,
            Name = "Nintendo Switch",
            Manufacturer = "Nintendo",
            Price = Price.Create(299),
            Games = new List<Game>(),
        };

        //Seeding Consoles
        context.GamesConsoles.Add(gamesConsole1);
        context.GamesConsoles.Add(gamesConsole2);
        context.GamesConsoles.Add(gamesConsole3);
        context.GamesConsoles.Add(gamesConsole4);

        //Seeding Games
        context.Games.Add(new Game
        {
            Id = 1,
            Name = "Final Fantasy VII Remake",
            Publisher = "Square Enix",
            Price = Price.Create(69),
            GamesConsoleId = 1,
            GamesConsole = gamesConsole1,
        });
        context.Games.Add(new Game
        {
            Id = 2,
            Name = "Final Fantasy VII Remake",
            Publisher = "Square Enix",
            Price = Price.Create(79),
            GamesConsoleId = 1,
            GamesConsole = gamesConsole2,
        });
        context.Games.Add(new Game
        {
            Id = 3,
            Name = "Horizon Forbidden West",
            Publisher = "Sony Interactive Entertainment",
            Price = Price.Create(79),
            GamesConsoleId = 2,
            GamesConsole = gamesConsole2,
        });
        context.Games.Add(new Game
        {
            Id = 4,
            Name = "The Legend of Zelda: Tears of the Kingdom",
            Publisher = "Nintendo",
            Price = Price.Create(59),
            GamesConsoleId = 4,
            GamesConsole = gamesConsole4,
        });
        context.Games.Add(new Game
        {
            Id = 5,
            Name = "Xenoblade Chronicles 3",
            Publisher = "Monolith Soft",
            Price = Price.Create(59),
            GamesConsoleId = 4,
            GamesConsole = gamesConsole4,
        });
        context.Games.Add(new Game
        {
            Id = 6,
            Name = "Halo Infinite",
            Publisher = "Xbox Game Studios",
            Price = Price.Create(69),
            GamesConsoleId = 3,
            GamesConsole = gamesConsole3,
        });
        context.Games.Add(new Game
        {
            Id = 7,
            Name = "Crisis Core Final Fantasy VII Reunion",
            Publisher = "Square Enix",
            Price = Price.Create(69),
            GamesConsoleId = 4,
            GamesConsole = gamesConsole4
        });

        context.SaveChanges();
    }
}

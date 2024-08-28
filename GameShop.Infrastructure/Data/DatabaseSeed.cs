using GameShop.Domain.Entities;
using GameShop.Domain.ValueObjects;

namespace GameShop.Infrastructure.Data;

public static class DatabaseSeed
{
    public static void SeedData(BaseDatabaseContext context)
    {
        var gameConsole1 = new GameConsole
        {
            Id = 1,
            Name = "PlayStation 4",
            Manufacturer = "Sony",
            Price = Price.Create(249),
            Games = new List<Game>(),
        }; 
        var gameConsole2 = new GameConsole
        {
            Id = 2,
            Name = "PlayStation 5",
            Manufacturer = "Sony",
            Price = Price.Create(549),
            Games = new List<Game>(),
        };
        var gameConsole3 = new GameConsole
        {
            Id = 3,
            Name = "Xbox Series X",
            Manufacturer = "Microsoft",
            Price = Price.Create(599),
            Games = new List<Game>(),
        };
        var gameConsole4 = new GameConsole
        {
            Id = 4,
            Name = "Nintendo Switch",
            Manufacturer = "Nintendo",
            Price = Price.Create(299),
            Games = new List<Game>(),
        };

        //Seeding Consoles
        context.GameConsoles.Add(gameConsole1);
        context.GameConsoles.Add(gameConsole2);
        context.GameConsoles.Add(gameConsole3);
        context.GameConsoles.Add(gameConsole4);

        //Seeding Games
        context.Games.Add(new Game
        {
            Id = 1,
            Name = "Final Fantasy VII Remake",
            Publisher = "Square Enix",
            Price = Price.Create(69),
            GameConsoleId = 1,
            GameConsole = gameConsole1,
        });
        context.Games.Add(new Game
        {
            Id = 2,
            Name = "Final Fantasy VII Remake",
            Publisher = "Square Enix",
            Price = Price.Create(79),
            GameConsoleId = 1,
            GameConsole = gameConsole2,
        });
        context.Games.Add(new Game
        {
            Id = 3,
            Name = "Horizon Forbidden West",
            Publisher = "Sony Interactive Entertainment",
            Price = Price.Create(79),
            GameConsoleId = 2,
            GameConsole = gameConsole2,
        });
        context.Games.Add(new Game
        {
            Id = 4,
            Name = "The Legend of Zelda: Tears of the Kingdom",
            Publisher = "Nintendo",
            Price = Price.Create(59),
            GameConsoleId = 4,
            GameConsole = gameConsole4,
        });
        context.Games.Add(new Game
        {
            Id = 5,
            Name = "Xenoblade Chronicles 3",
            Publisher = "Monolith Soft",
            Price = Price.Create(59),
            GameConsoleId = 4,
            GameConsole = gameConsole4,
        });
        context.Games.Add(new Game
        {
            Id = 6,
            Name = "Halo Infinite",
            Publisher = "Xbox Game Studios",
            Price = Price.Create(69),
            GameConsoleId = 3,
            GameConsole = gameConsole3,
        });
        context.Games.Add(new Game
        {
            Id = 7,
            Name = "Crisis Core Final Fantasy VII Reunion",
            Publisher = "Square Enix",
            Price = Price.Create(69),
            GameConsoleId = 4,
            GameConsole = gameConsole4
        });

        context.SaveChanges();
    }
}

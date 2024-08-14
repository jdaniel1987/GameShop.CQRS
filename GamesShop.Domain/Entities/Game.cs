using GamesShop.Domain.ValueObjects;

namespace GamesShop.Domain.Entities;

public class Game
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required string Publisher { get; set; }

    public required Price Price { get; set; }

    public required int GamesConsoleId { get; set; }

    public virtual GamesConsole? GamesConsole { get; set; }
}
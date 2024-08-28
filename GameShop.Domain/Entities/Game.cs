using GameShop.Domain.ValueObjects;

namespace GameShop.Domain.Entities;

public class Game
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required string Publisher { get; set; }

    public required Price Price { get; set; }

    public required int GameConsoleId { get; set; }

    public virtual GameConsole? GameConsole { get; set; }
}
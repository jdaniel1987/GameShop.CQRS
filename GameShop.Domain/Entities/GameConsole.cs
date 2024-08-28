using GameShop.Domain.ValueObjects;

namespace GameShop.Domain.Entities;

public class GameConsole
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required string Manufacturer { get; set; }

    public required Price Price { get; set; }

    public virtual IReadOnlyCollection<Game> Games { get; set; } = new List<Game>();
}

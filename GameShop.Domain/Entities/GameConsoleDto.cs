using GameShop.Domain.ValueObjects;

namespace GameShop.Domain.Entities;

public class GameConsoleDto
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required string Manufacturer { get; set; }

    public required Price Price { get; set; }

    public virtual IReadOnlyCollection<GameDto> Games { get; set; } = new List<GameDto>();
}

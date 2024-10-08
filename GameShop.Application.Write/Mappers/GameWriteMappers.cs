using GameShop.Application.Write.Commands.AddGame;
using GameShop.Application.Write.Commands.UpdateGame;
using GameShop.Application.Write.Events.GameCreated;
using GameShop.Domain.Entities;
using GameShop.Domain.ValueObjects;

namespace GameShop.Application.Write.Mappers;

public static class GameWriteMappers
{
    public static Game ToDomain(this AddGameCommand command) =>
        new()
        {
            Name = command.Name,
            Publisher = command.Publisher,
            Price = Price.Create(command.Price),
            GameConsoleId = command.GameConsoleId
        };

    public static Game ToDomain(this UpdateGameCommand command) =>
        new()
        {
            Id = command.Id,
            Name = command.Name,
            Publisher = command.Publisher,
            Price = Price.Create(command.Price),
            GameConsoleId = command.GameConsoleId
        };

    public static AddGameCommandResponse ToAddGameCommandResponse(this Game game) =>
        new(
            game.Id,
            game.Name);

    public static UpdateGameCommandResponse ToUpdateGameCommandResponse(this Game game) =>
        new(
            game.Id,
            game.Name);

    public static GameCreatedEvent ToEvent(this Game game) =>
        new(
            GameName: game.Name,
            Publisher: game.Publisher,
            PriceUSD: game.Price.Value,
            PriceEUR: ((PriceEuros)game.Price).Value, // Conversion is automatic due to ValueObject operator
            CreationDate: DateTime.UtcNow);
}

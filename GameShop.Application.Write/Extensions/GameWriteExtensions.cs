using GameShop.API.Write.Contracts.Requests;
using GameShop.API.Write.Contracts.Responses;
using GameShop.Application.Write.Commands.AddGame;
using GameShop.Application.Write.Commands.UpdateGame;
using GameShop.Application.Write.Events.GameCreated;
using GameShop.Domain.Entities;
using GameShop.Domain.ValueObjects;

namespace GameShop.Application.Write.Extensions;

public static class GameWriteExtensions
{
    public static AddGameCommand ToCommand(this AddGameRequest game) =>
        new(
            game.Name,
            game.Publisher,
            game.GameConsoleId,
            game.Price);

    public static UpdateGameCommand ToCommand(this UpdateGameRequest game) =>
        new(
            game.Id,
            game.Name,
            game.Publisher,
            game.GameConsoleId,
            game.Price);

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

    public static GameCreatedEvent ToEvent(this Game game) =>
        new(
            GameName: game.Name,
            Publisher: game.Publisher,
            PriceUSD: game.Price.Value,
            PriceEUR: ((PriceEuros)game.Price).Value, // Conversion is automatic due to ValueObject operator
            CreationDate: DateTime.UtcNow);

    public static AddGameResponse ToAddGameCommandResponse(this Game game) =>
        new(
            game.Id,
            game.Name);

    public static UpdateGameResponse ToUpdateGameCommandResponse(this Game game) =>
        new(
            game.Id,
            game.Name);
}

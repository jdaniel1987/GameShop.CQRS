using GameShop.API.Write.Contracts.Requests;
using GameShop.Application.Write.Commands.AddGame;
using GameShop.Application.Write.Commands.UpdateGame;

namespace GameShop.API.Write.Mappers;

public static class GameWriteMappers
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
}

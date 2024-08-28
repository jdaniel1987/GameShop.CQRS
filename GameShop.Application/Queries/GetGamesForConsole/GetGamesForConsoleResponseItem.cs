namespace GameShop.Application.Queries.GetGamesForConsole;

public record GetGamesForConsoleResponseItem(
    int Id,
    string Name,
    string Publisher,
    double Price,
    int GameConsoleId,
    string GameConsoleName);

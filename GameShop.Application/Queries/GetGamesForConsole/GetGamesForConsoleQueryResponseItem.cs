namespace GameShop.Application.Queries.GetGamesForConsole;

public record GetGamesForConsoleQueryResponseItem(
    int Id,
    string Name,
    string Publisher,
    double Price,
    int GameConsoleId,
    string GameConsoleName);

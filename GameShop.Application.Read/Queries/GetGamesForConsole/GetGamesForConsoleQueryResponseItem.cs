namespace GameShop.Application.Read.Queries.GetGamesForConsole;

public record GetGamesForConsoleQueryResponseItem(
    int Id,
    string Name,
    string Publisher,
    double Price,
    int GameConsoleId,
    string GameConsoleName);

namespace GamesShop.Application.Queries.GetGamesForConsole;

public record GetGamesForConsoleResponseItem(
    int Id,
    string Name,
    string Publisher,
    double Price,
    int GamesConsoleId,
    string GamesConsoleName);

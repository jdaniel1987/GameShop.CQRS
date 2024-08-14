namespace GamesShop.Application.Queries.GetAllGames;

public record GetGamesForConsoleResponseItem(
    int Id,
    string Name,
    string Publisher,
    double Price,
    int GamesConsoleId,
    string GamesConsoleName);

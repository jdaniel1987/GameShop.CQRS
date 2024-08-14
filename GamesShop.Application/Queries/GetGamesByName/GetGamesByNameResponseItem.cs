namespace GamesShop.Application.Queries.GetAllGames;

public record GetGamesByNameResponseItem(
    int Id,
    string Name,
    string Publisher,
    double Price,
    int GamesConsoleId,
    string GamesConsoleName);

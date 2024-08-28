namespace GameShop.Application.Queries.GetAllGames;

public record GetAllGamesResponseItem(
    int Id,
    string Name,
    string Publisher,
    double Price,
    int GameConsoleId,
    string GameConsoleName);

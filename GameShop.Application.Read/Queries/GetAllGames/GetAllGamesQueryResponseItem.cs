namespace GameShop.Application.Read.Queries.GetAllGames;

public record GetAllGamesQueryResponseItem(
    int Id,
    string Name,
    string Publisher,
    double Price,
    int GameConsoleId,
    string GameConsoleName);

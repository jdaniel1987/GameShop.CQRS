namespace GameShop.Application.Queries.GetGamesByName;

public record GetGamesByNameResponseItem(
    int Id,
    string Name,
    string Publisher,
    double Price,
    int GameConsoleId,
    string GameConsoleName);

namespace GameShop.Application.Queries.GetGamesByName;

public record GetGamesByNameQueryResponseItem(
    int Id,
    string Name,
    string Publisher,
    double Price,
    int GameConsoleId,
    string GameConsoleName);

namespace GameShop.API.Read.Contracts.Responses;

public record GetAllGamesResponseItem(
    int Id,
    string Name,
    string Publisher,
    double Price,
    int GameConsoleId,
    string GameConsoleName);

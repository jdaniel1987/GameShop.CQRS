namespace GameShop.API.Write.Contracts.Requests;

public record AddGameRequest(
    string Name,
    string Publisher,
    int GameConsoleId,
    double Price);

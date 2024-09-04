namespace GameShop.Contracts.Requests;

public record AddGameRequest(
    string Name,
    string Publisher,
    int GameConsoleId,
    double Price);

namespace GameShop.API.Write.Contracts.Requests;

public record UpdateGameRequest(
    int Id,
    string Name,
    string Publisher,
    int GameConsoleId,
    double Price);

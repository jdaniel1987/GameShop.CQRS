namespace GameShop.API.Write.Contracts.Requests;

public record AddGameConsoleRequest(
    string Name,
    string Manufacturer,
    double Price);

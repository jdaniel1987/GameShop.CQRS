namespace GameShop.Contracts.Requests;

public record UpdateGameConsoleRequest(
    int Id,
    string Name,
    string Manufacturer,
    double Price);

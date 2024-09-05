namespace GameShop.API.Read.Contracts.Responses;

public record GetAllGameConsolesResponseItem(
    int Id,
    string Name,
    string Manufacturer,
    double Price,
    int NumberOfGames);

namespace GameShop.Application.Queries.GetAllGameConsoles;

public record GetAllGameConsolesResponseItem(
    int Id,
    string Name,
    string Manufacturer,
    double Price,
    int NumberOfGames);

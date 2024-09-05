namespace GameShop.Application.Queries.GetAllGameConsoles;

public record GetAllGameConsolesQueryResponseItem(
    int Id,
    string Name,
    string Manufacturer,
    double Price,
    int NumberOfGames);

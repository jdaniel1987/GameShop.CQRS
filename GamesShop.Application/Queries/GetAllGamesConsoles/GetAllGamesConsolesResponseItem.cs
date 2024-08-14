namespace GamesShop.Application.Queries.GetAllGamesConsoles;

public record GetAllGamesConsolesResponseItem(
    int Id,
    string Name,
    string Manufacturer,
    double Price,
    int NumberOfGames);

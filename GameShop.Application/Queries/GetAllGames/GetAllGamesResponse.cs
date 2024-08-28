namespace GameShop.Application.Queries.GetAllGames;

public record GetAllGamesResponse(IReadOnlyCollection<GetAllGamesResponseItem> Games);

namespace GameShop.Application.Queries.GetAllGames;

public record GetAllGamesQueryResponse(IReadOnlyCollection<GetAllGamesQueryResponseItem> Games);

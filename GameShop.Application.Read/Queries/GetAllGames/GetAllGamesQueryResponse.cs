namespace GameShop.Application.Read.Queries.GetAllGames;

public record GetAllGamesQueryResponse(IReadOnlyCollection<GetAllGamesQueryResponseItem> Games);

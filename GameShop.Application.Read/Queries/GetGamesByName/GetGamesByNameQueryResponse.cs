namespace GameShop.Application.Read.Queries.GetGamesByName;

public record GetGamesByNameQueryResponse(IReadOnlyCollection<GetGamesByNameQueryResponseItem> Games);

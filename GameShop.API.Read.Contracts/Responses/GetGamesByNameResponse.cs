namespace GameShop.API.Read.Contracts.Responses;

public record GetGamesByNameResponse(IReadOnlyCollection<GetGamesByNameResponseItem> Games);

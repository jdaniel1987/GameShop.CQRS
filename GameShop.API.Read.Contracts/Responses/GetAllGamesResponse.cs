namespace GameShop.API.Read.Contracts.Responses;

public record GetAllGamesResponse(IReadOnlyCollection<GetAllGamesResponseItem> Games);

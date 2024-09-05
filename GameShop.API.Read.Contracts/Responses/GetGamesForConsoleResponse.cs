namespace GameShop.API.Read.Contracts.Responses;

public record GetGamesForConsoleResponse(IReadOnlyCollection<GetGamesForConsoleResponseItem> Games);

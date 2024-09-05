namespace GameShop.API.Read.Contracts.Responses;

public record GetAllGameConsolesResponse(IReadOnlyCollection<GetAllGameConsolesResponseItem> GameConsoles);

namespace GamesShop.Application.Queries.GetGamesByName;

public record GetGamesByNameResponse(IReadOnlyCollection<GetGamesByNameResponseItem> Games);

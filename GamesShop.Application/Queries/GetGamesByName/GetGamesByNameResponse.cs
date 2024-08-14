using GamesShop.Application.Queries.GetAllGames;

namespace GamesShop.Application.Queries.GetGamesByName;

public record GetGamesByNameResponse(IReadOnlyCollection<GetGamesByNameResponseItem> Games);

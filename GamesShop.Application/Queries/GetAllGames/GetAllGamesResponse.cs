namespace GamesShop.Application.Queries.GetAllGames;

public record GetAllGamesResponse(IReadOnlyCollection<GetGamesByNameResponseItem> Games);

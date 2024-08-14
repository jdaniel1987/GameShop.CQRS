using GamesShop.Application.Queries.GetAllGames;

namespace GamesShop.Application.Queries.GetGamesForConsole;

public record GetGamesForConsoleResponse(IReadOnlyCollection<GetGamesForConsoleResponseItem> Games);

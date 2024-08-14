using MediatR;

namespace GamesShop.Application.Queries.GetAllGames;

public record GetAllGamesQuery() : IRequest<GetAllGamesResponse>;

using CSharpFunctionalExtensions;
using MediatR;

namespace GamesShop.Application.Queries.GetAllGames;

public record GetAllGamesQuery() : IRequest<IResult<GetAllGamesResponse>>;

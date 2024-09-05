using CSharpFunctionalExtensions;
using MediatR;

namespace GameShop.Application.Queries.GetAllGames;

public record GetAllGamesQuery() : IRequest<IResult<GetAllGamesQueryResponse>>;

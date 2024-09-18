using CSharpFunctionalExtensions;
using MediatR;

namespace GameShop.Application.Read.Queries.GetAllGames;

public record GetAllGamesQuery() : IRequest<IResult<GetAllGamesQueryResponse>>;

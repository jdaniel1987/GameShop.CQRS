using CSharpFunctionalExtensions;
using MediatR;

namespace GamesShop.Application.Queries.GetGamesByName;

public record GetGamesByNameQuery(string GameName) : IRequest<IResult<GetGamesByNameResponse>>;

using CSharpFunctionalExtensions;
using MediatR;

namespace GameShop.Application.Queries.GetGamesByName;

public record GetGamesByNameQuery(string GameName) : IRequest<IResult<GetGamesByNameQueryResponse>>;

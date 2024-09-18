using CSharpFunctionalExtensions;
using MediatR;

namespace GameShop.Application.Read.Queries.GetGamesByName;

public record GetGamesByNameQuery(string GameName) : IRequest<IResult<GetGamesByNameQueryResponse>>;

using CSharpFunctionalExtensions;
using MediatR;

namespace GameShop.Application.Queries.GetAllGamesConsoles;

public record GetAllGamesConsolesQuery() : IRequest<IResult<GetAllGamesConsolesResponse>>;

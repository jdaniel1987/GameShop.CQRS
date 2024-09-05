using CSharpFunctionalExtensions;
using MediatR;

namespace GameShop.Application.Queries.GetAllGameConsoles;

public record GetAllGameConsolesQuery() : IRequest<IResult<GetAllGameConsolesQueryResponse>>;

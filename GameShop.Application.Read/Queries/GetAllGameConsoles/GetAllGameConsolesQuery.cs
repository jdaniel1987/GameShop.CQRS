using CSharpFunctionalExtensions;
using MediatR;

namespace GameShop.Application.Read.Queries.GetAllGameConsoles;

public record GetAllGameConsolesQuery() : IRequest<IResult<GetAllGameConsolesQueryResponse>>;

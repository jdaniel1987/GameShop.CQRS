using CSharpFunctionalExtensions;
using GameShop.Application.Read.Mappers;
using GameShop.Domain.Repositories;
using MediatR;

namespace GameShop.Application.Read.Queries.GetAllGameConsoles;

public class GetAllGameConsolesHandler(
    IGameConsoleReadRepository gameConsoleReadRepository) : IRequestHandler<GetAllGameConsolesQuery, IResult<GetAllGameConsolesQueryResponse>>
{
    private readonly IGameConsoleReadRepository _gameConsoleReadRepository = gameConsoleReadRepository;

    public async Task<IResult<GetAllGameConsolesQueryResponse>> Handle(GetAllGameConsolesQuery query, CancellationToken cancellationToken)
    {
        var gameConsoles = await _gameConsoleReadRepository.GetAllGameConsoles(cancellationToken);

        return Result.Success(gameConsoles.ToGetAllGameConsolesQueryResponse());
    }
}

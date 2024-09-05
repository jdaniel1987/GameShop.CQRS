using CSharpFunctionalExtensions;
using GameShop.Application.Extensions;
using GameShop.Domain.Repositories;
using MediatR;

namespace GameShop.Application.Queries.GetAllGameConsoles;

public class GetAllGameConsolesHandler(
    IGameConsoleRepository gameConsoleRepository) : IRequestHandler<GetAllGameConsolesQuery, IResult<GetAllGameConsolesQueryResponse>>
{
    private readonly IGameConsoleRepository _gameConsoleRepository = gameConsoleRepository;

    public async Task<IResult<GetAllGameConsolesQueryResponse>> Handle(GetAllGameConsolesQuery request, CancellationToken cancellationToken)
    {
        var gameConsoles = await _gameConsoleRepository.GetAllGameConsoles(cancellationToken);

        return Result.Success(gameConsoles.ToGetAllGameConsolesQueryResponse());
    }
}

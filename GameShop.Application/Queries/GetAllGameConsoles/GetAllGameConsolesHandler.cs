using CSharpFunctionalExtensions;
using GameShop.Application.Extensions;
using GameShop.Domain.Repositories;
using MediatR;

namespace GameShop.Application.Queries.GetAllGameConsoles;

public class GetAllGameConsolesHandler(
    IGameConsoleRepository gameConsoleRepository) : IRequestHandler<GetAllGameConsolesQuery, IResult<GetAllGameConsolesResponse>>
{
    private readonly IGameConsoleRepository _gameConsoleRepository = gameConsoleRepository;

    public async Task<IResult<GetAllGameConsolesResponse>> Handle(GetAllGameConsolesQuery request, CancellationToken cancellationToken)
    {
        var gameConsoles = await _gameConsoleRepository.GetAllGameConsoles(cancellationToken);

        return Result.Success(gameConsoles.ToGetAllGameConsolesResponse());
    }
}

using CSharpFunctionalExtensions;
using GameShop.Application.Extensions;
using GameShop.Domain.Repositories;
using MediatR;

namespace GameShop.Application.Queries.GetGamesForConsole;

public class GetGamesForConsoleHandler(
    IGameRepository gameRepository,
    IGameConsoleRepository consoleRepository) : IRequestHandler<GetGamesForConsoleQuery, IResult<GetGamesForConsoleQueryResponse>>
{
    private readonly IGameRepository _gameRepository = gameRepository;
    private readonly IGameConsoleRepository _gameConsoleRepository = consoleRepository;

    public async Task<IResult<GetGamesForConsoleQueryResponse>> Handle(GetGamesForConsoleQuery request, CancellationToken cancellationToken)
    {
        var console = await _gameConsoleRepository.GetGameConsole(request.GameConsoleId, cancellationToken);

        if (console is null)
        {
            return Result.Failure<GetGamesForConsoleQueryResponse>($"Console with ID {request.GameConsoleId} does not exist");
        }

        var games = await _gameRepository.GetGamesForConsole(request.GameConsoleId, cancellationToken);

        return Result.Success(games.ToGetGamesForConsoleQueryResponse());
    }
}

using CSharpFunctionalExtensions;
using GamesShop.Application.Extensions;
using GamesShop.Domain.Repositories;
using MediatR;

namespace GamesShop.Application.Queries.GetGamesForConsole;

public class GetGamesForConsoleHandler(
    IGameRepository gameRepository,
    IGamesConsoleRepository consoleRepository) : IRequestHandler<GetGamesForConsoleQuery, IResult<GetGamesForConsoleResponse>>
{
    private readonly IGameRepository _gameRepository = gameRepository;
    private readonly IGamesConsoleRepository _gamesConsoleRepository = consoleRepository;

    public async Task<IResult<GetGamesForConsoleResponse>> Handle(GetGamesForConsoleQuery request, CancellationToken cancellationToken)
    {
        var console = await _gamesConsoleRepository.GetGamesConsole(request.GamesConsoleId, cancellationToken);

        if (console is null)
        {
            return Result.Failure<GetGamesForConsoleResponse>($"Console with ID {request.GamesConsoleId} does not exist");
        }

        var games = await _gameRepository.GetGamesForConsole(request.GamesConsoleId, cancellationToken);

        return Result.Success(games.ToGetGamesForConsoleResponse());
    }
}

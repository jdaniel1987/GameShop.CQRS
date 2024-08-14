using GamesShop.Application.Extensions;
using GamesShop.Domain.Repositories;
using MediatR;

namespace GamesShop.Application.Queries.GetGamesForConsole;

public class GetGamesForConsoleHandler(
    IGameRepository gameRepository,
    IGamesConsoleRepository consoleRepository) : IRequestHandler<GetGamesForConsoleQuery, GetGamesForConsoleResponse>
{
    private readonly IGameRepository _gameRepository = gameRepository;
    private readonly IGamesConsoleRepository _consoleRepository = consoleRepository;

    public async Task<GetGamesForConsoleResponse> Handle(GetGamesForConsoleQuery request, CancellationToken cancellationToken)
    {
        var console = await _consoleRepository.GetGamesConsole(request.GamesConsoleId, cancellationToken);

        if (console is null)
        {
            throw new ArgumentException($"Console with ID {request.GamesConsoleId} does not exist");
        }

        var games = await _gameRepository.GetAllGamesForConsole(request.GamesConsoleId, cancellationToken);

        return games.ToGetGamesForConsoleResponse();
    }
}

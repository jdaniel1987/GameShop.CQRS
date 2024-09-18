using CSharpFunctionalExtensions;
using GameShop.Application.Read.Extensions;
using GameShop.Domain.Repositories;
using MediatR;

namespace GameShop.Application.Read.Queries.GetGamesForConsole;

public class GetGamesForConsoleHandler(
    IGameReadRepository gameReadRepository,
    IGameConsoleReadRepository gameConsoleReadRepository) : IRequestHandler<GetGamesForConsoleQuery, IResult<GetGamesForConsoleQueryResponse>>
{
    private readonly IGameReadRepository _gameReadRepository = gameReadRepository;
    private readonly IGameConsoleReadRepository _gameConsoleReadRepository = gameConsoleReadRepository;

    public async Task<IResult<GetGamesForConsoleQueryResponse>> Handle(GetGamesForConsoleQuery request, CancellationToken cancellationToken)
    {
        var console = await _gameConsoleReadRepository.GetGameConsole(request.GameConsoleId, cancellationToken);

        if(console is null)
        {
            return Result.Failure<GetGamesForConsoleQueryResponse>($"Console with ID {request.GameConsoleId} does not exist");
        }

        var games = await _gameReadRepository.GetGamesForConsole(request.GameConsoleId, cancellationToken);

        return Result.Success(games.ToGetGamesForConsoleQueryResponse());
    }
}

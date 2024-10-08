using CSharpFunctionalExtensions;
using GameShop.Application.Read.Mappers;
using GameShop.Domain.Repositories;
using MediatR;

namespace GameShop.Application.Read.Queries.GetGamesForConsole;

public class GetGamesForConsoleHandler(
    IGameReadRepository gameReadRepository,
    IGameConsoleReadRepository gameConsoleReadRepository) : IRequestHandler<GetGamesForConsoleQuery, IResult<GetGamesForConsoleQueryResponse>>
{
    private readonly IGameReadRepository _gameReadRepository = gameReadRepository;
    private readonly IGameConsoleReadRepository _gameConsoleReadRepository = gameConsoleReadRepository;

    public async Task<IResult<GetGamesForConsoleQueryResponse>> Handle(GetGamesForConsoleQuery query, CancellationToken cancellationToken)
    {
        var console = await _gameConsoleReadRepository.GetGameConsole(query.GameConsoleId, cancellationToken);

        if(console is null)
        {
            return Result.Failure<GetGamesForConsoleQueryResponse>($"Console with ID {query.GameConsoleId} does not exist");
        }

        var games = await _gameReadRepository.GetGamesForConsole(query.GameConsoleId, cancellationToken);

        return Result.Success(games.ToGetGamesForConsoleQueryResponse());
    }
}

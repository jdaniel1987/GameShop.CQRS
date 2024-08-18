using CSharpFunctionalExtensions;
using GamesShop.Application.Extensions;
using GamesShop.Domain.Repositories;
using MediatR;

namespace GamesShop.Application.Queries.GetAllGames;

public class GetAllGamesHandler(
    IGameRepository gameRepository) : IRequestHandler<GetAllGamesQuery, IResult<GetAllGamesResponse>>
{
    private readonly IGameRepository _gameRepository = gameRepository;

    public async Task<IResult<GetAllGamesResponse>> Handle(GetAllGamesQuery request, CancellationToken cancellationToken)
    {
        var games = await _gameRepository.GetAllGames(cancellationToken);

        return Result.Success(games.ToGetAllGamesResponse());
    }
}

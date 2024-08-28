using CSharpFunctionalExtensions;
using GameShop.Application.Extensions;
using GameShop.Domain.Repositories;
using MediatR;

namespace GameShop.Application.Queries.GetAllGames;

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

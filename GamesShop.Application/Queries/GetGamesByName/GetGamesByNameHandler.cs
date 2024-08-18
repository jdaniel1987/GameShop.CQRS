using CSharpFunctionalExtensions;
using GamesShop.Application.Extensions;
using GamesShop.Domain.Repositories;
using MediatR;

namespace GamesShop.Application.Queries.GetGamesByName;

public class GetGamesByNameHandler(
    IGameRepository gameRepository) : IRequestHandler<GetGamesByNameQuery, IResult<GetGamesByNameResponse>>
{
    private readonly IGameRepository _gameRepository = gameRepository;

    public async Task<IResult<GetGamesByNameResponse>> Handle(GetGamesByNameQuery request, CancellationToken cancellationToken)
    {
        var games = await _gameRepository.GetGamesByName(request.GameName, cancellationToken);

        return Result.Success(games.ToGetGamesByNameResponse());
    }
}

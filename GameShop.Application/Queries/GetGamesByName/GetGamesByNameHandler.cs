using CSharpFunctionalExtensions;
using GameShop.Application.Extensions;
using GameShop.Domain.Repositories;
using MediatR;

namespace GameShop.Application.Queries.GetGamesByName;

public class GetGamesByNameHandler(
    IGameRepository gameRepository) : IRequestHandler<GetGamesByNameQuery, IResult<GetGamesByNameQueryResponse>>
{
    private readonly IGameRepository _gameRepository = gameRepository;

    public async Task<IResult<GetGamesByNameQueryResponse>> Handle(GetGamesByNameQuery request, CancellationToken cancellationToken)
    {
        var games = await _gameRepository.GetGamesByName(request.GameName, cancellationToken);

        return Result.Success(games.ToGetGamesByNameQueryResponse());
    }
}

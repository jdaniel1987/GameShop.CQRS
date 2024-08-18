using CSharpFunctionalExtensions;
using GamesShop.Application.Extensions;
using GamesShop.Domain.Repositories;
using MediatR;

namespace GamesShop.Application.Queries.GetAllGamesConsoles;

public class GetAllGamesConsolesHandler(
    IGamesConsoleRepository gamesConsoleRepository) : IRequestHandler<GetAllGamesConsolesQuery, IResult<GetAllGamesConsolesResponse>>
{
    private readonly IGamesConsoleRepository _gamesConsoleRepository = gamesConsoleRepository;

    public async Task<IResult<GetAllGamesConsolesResponse>> Handle(GetAllGamesConsolesQuery request, CancellationToken cancellationToken)
    {
        var gamesConsoles = await _gamesConsoleRepository.GetAllGamesConsoles(cancellationToken);

        return Result.Success(gamesConsoles.ToGetAllGamesConsolesResponse());
    }
}

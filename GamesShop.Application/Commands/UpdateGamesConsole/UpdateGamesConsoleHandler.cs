using CSharpFunctionalExtensions;
using GamesShop.Application.Extensions;
using GamesShop.Domain.Repositories;
using MediatR;

namespace GamesShop.Application.Commands.UpdateGamesConsole;

public class UpdateGamesConsoleHandler(
    IGamesConsoleRepository gamesConsoleRepository) : IRequestHandler<UpdateGamesConsoleCommand, IResult<UpdateGamesConsoleResponse>>
{
    private readonly IGamesConsoleRepository _gamesConsoleRepository = gamesConsoleRepository;

    public async Task<IResult<UpdateGamesConsoleResponse>> Handle(UpdateGamesConsoleCommand request, CancellationToken cancellationToken)
    {
        var gamesConsole = request.ToDomain();
        await _gamesConsoleRepository.UpdateGamesConsole(gamesConsole, cancellationToken);

        return Result.Success(gamesConsole.ToUpdateGamesConsoleResponse());
    }
}

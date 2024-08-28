using CSharpFunctionalExtensions;
using GameShop.Application.Extensions;
using GameShop.Domain.Repositories;
using MediatR;

namespace GameShop.Application.Commands.UpdateGamesConsole;

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

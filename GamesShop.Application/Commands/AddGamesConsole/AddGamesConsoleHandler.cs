using CSharpFunctionalExtensions;
using GamesShop.Application.Extensions;
using GamesShop.Domain.Repositories;
using MediatR;

namespace GamesShop.Application.Commands.AddGamesConsole;

public class AddGamesConsoleHandler(
    IGamesConsoleRepository gamesConsoleRepository) : IRequestHandler<AddGamesConsoleCommand, IResult<AddGamesConsoleResponse>>
{
    private readonly IGamesConsoleRepository _gamesConsoleRepository = gamesConsoleRepository;

    public async Task<IResult<AddGamesConsoleResponse>> Handle(AddGamesConsoleCommand request, CancellationToken cancellationToken)
    {
        var gamesConsole = request.ToDomain();
        await _gamesConsoleRepository.AddGamesConsole(gamesConsole, cancellationToken);

        return Result.Success(gamesConsole.ToAddGamesConsoleResponse());
    }
}

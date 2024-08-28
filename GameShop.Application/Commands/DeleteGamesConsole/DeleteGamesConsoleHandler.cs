using CSharpFunctionalExtensions;
using GameShop.Application.Extensions;
using GameShop.Domain.Repositories;
using MediatR;

namespace GameShop.Application.Commands.DeleteGamesConsole;

public class DeleteGamesConsoleHandler(
    IGamesConsoleRepository gamesConsoleRepository) 
    : IRequestHandler<DeleteGamesConsoleCommand, IResult<DeleteGamesConsoleResponse>>
{
    private readonly IGamesConsoleRepository _gamesConsoleRepository = gamesConsoleRepository;

    public async Task<IResult<DeleteGamesConsoleResponse>> Handle(DeleteGamesConsoleCommand request, CancellationToken cancellationToken)
    {
        var gamesConsole = await _gamesConsoleRepository.GetGamesConsole(request.GamesConsoleId, cancellationToken);
        if (gamesConsole is null)
        {
            return Result.Failure<DeleteGamesConsoleResponse>($"Games Console with ID: {request.GamesConsoleId} not found.");
        }
        await _gamesConsoleRepository.DeleteGamesConsole(gamesConsole, cancellationToken);

        return Result.Success(gamesConsole.ToDeleteGamesConsoleResponse());
    }
}

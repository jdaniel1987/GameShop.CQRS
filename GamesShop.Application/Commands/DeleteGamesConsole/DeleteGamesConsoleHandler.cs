using GamesShop.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace GamesShop.Application.Commands.DeleteGamesConsole;

public class DeleteGamesConsoleHandler(IGamesConsoleRepository gamesConsoleRepository) : IRequestHandler<DeleteGamesConsoleCommand, IResult>
{
    private readonly IGamesConsoleRepository _gamesConsoleRepository = gamesConsoleRepository;

    public async Task<IResult> Handle(DeleteGamesConsoleCommand request, CancellationToken cancellationToken)
    {
        var gamesConsole = await _gamesConsoleRepository.GetGamesConsole(request.GamesConsoleId, cancellationToken);
        if (gamesConsole is null)
        {
            return Results.NotFound($"Games Console with ID: {request.GamesConsoleId} not found.");
        }
        await _gamesConsoleRepository.DeleteGamesConsole(gamesConsole, cancellationToken);

        return Results.Ok();
    }
}

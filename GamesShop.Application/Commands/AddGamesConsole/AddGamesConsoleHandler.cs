using GamesShop.Application.Extensions;
using GamesShop.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace GamesShop.Application.Commands.AddGamesConsole;

public class AddGamesConsoleHandler(
    IGamesConsoleRepository gamesConsoleRepository) : IRequestHandler<AddGamesConsoleCommand, IResult>
{
    private readonly IGamesConsoleRepository _gamesConsoleRepository = gamesConsoleRepository;

    public async Task<IResult> Handle(AddGamesConsoleCommand request, CancellationToken cancellationToken)
    {
        var gamesConsole = request.ToDomain();
        await _gamesConsoleRepository.AddGamesConsole(gamesConsole, cancellationToken);

        return Results.Created($"api/GamesConsole/{gamesConsole.Id}", new { gamesConsole.Id, gamesConsole.Name });
    }
}

using GamesShop.Application.Extensions;
using GamesShop.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace GamesShop.Application.Commands.UpdateGamesConsole;

public class UpdateGamesConsoleHandler(
    IGamesConsoleRepository gamesConsoleRepository) : IRequestHandler<UpdateGamesConsoleCommand, IResult>
{
    private readonly IGamesConsoleRepository _gamesConsoleRepository = gamesConsoleRepository;

    public async Task<IResult> Handle(UpdateGamesConsoleCommand request, CancellationToken cancellationToken)
    {
        var gamesConsole = request.ToDomain();
        await _gamesConsoleRepository.UpdateGamesConsole(gamesConsole, cancellationToken);

        return Results.Ok();
    }
}

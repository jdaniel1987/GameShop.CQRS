using CSharpFunctionalExtensions;
using GameShop.API.Write.Contracts.Responses;
using GameShop.Domain.Repositories;
using MediatR;

namespace GameShop.Application.Commands.DeleteGameConsole;

public class DeleteGameConsoleHandler(
    IGameConsoleRepository gameConsoleRepository) 
    : IRequestHandler<DeleteGameConsoleCommand, IResult<DeleteGameConsoleResponse>>
{
    private readonly IGameConsoleRepository _gameConsoleRepository = gameConsoleRepository;

    public async Task<IResult<DeleteGameConsoleResponse>> Handle(DeleteGameConsoleCommand request, CancellationToken cancellationToken)
    {
        var gameConsole = await _gameConsoleRepository.GetGameConsole(request.GameConsoleId, cancellationToken);
        if (gameConsole is null)
        {
            return Result.Failure<DeleteGameConsoleResponse>($"Games Console with ID: {request.GameConsoleId} not found.");
        }
        await _gameConsoleRepository.DeleteGameConsole(gameConsole, cancellationToken);

        return Result.Success(new DeleteGameConsoleResponse());
    }
}

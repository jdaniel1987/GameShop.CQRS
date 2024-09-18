using CSharpFunctionalExtensions;
using GameShop.API.Write.Contracts.Responses;
using GameShop.Domain.Repositories;
using MediatR;

namespace GameShop.Application.Write.Commands.DeleteGameConsole;

public class DeleteGameConsoleHandler(
    IGameConsoleWriteRepository gameConsoleWriteRepository)
    : IRequestHandler<DeleteGameConsoleCommand, IResult<DeleteGameConsoleResponse>>
{
    private readonly IGameConsoleWriteRepository _gameConsoleWriteRepository = gameConsoleWriteRepository;

    public async Task<IResult<DeleteGameConsoleResponse>> Handle(DeleteGameConsoleCommand request, CancellationToken cancellationToken)
    {
        var gameConsole = await _gameConsoleWriteRepository.GetGameConsole(request.GameConsoleId, cancellationToken);
        if(gameConsole is null)
        {
            return Result.Failure<DeleteGameConsoleResponse>($"Games Console with ID: {request.GameConsoleId} not found.");
        }
        await _gameConsoleWriteRepository.DeleteGameConsole(gameConsole, cancellationToken);

        return Result.Success(new DeleteGameConsoleResponse());
    }
}

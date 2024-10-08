using CSharpFunctionalExtensions;
using MediatR;

namespace GameShop.Application.Write.Commands.DeleteGame;

public record DeleteGameCommand(int GameId) : IRequest<IResult<DeleteGameCommandResponse>>;

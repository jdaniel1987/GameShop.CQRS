using CSharpFunctionalExtensions;
using MediatR;

namespace GameShop.Application.Commands.DeleteGame;

public record DeleteGameCommand(int GameId) : IRequest<IResult<DeleteGameResponse>>;

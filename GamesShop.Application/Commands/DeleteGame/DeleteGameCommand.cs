using CSharpFunctionalExtensions;
using MediatR;

namespace GamesShop.Application.Commands.DeleteGame;

public record DeleteGameCommand(int GameId) : IRequest<IResult<DeleteGameResponse>>;

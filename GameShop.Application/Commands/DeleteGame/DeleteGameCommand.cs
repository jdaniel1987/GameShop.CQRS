using CSharpFunctionalExtensions;
using GameShop.API.Write.Contracts.Responses;
using MediatR;

namespace GameShop.Application.Commands.DeleteGame;

public record DeleteGameCommand(int GameId) : IRequest<IResult<DeleteGameResponse>>;

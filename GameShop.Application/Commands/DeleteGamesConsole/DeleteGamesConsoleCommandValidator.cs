using FluentValidation;

namespace GameShop.Application.Commands.DeleteGamesConsole;

public class DeleteGamesConsoleCommandValidator : AbstractValidator<DeleteGamesConsoleCommand>
{
    public DeleteGamesConsoleCommandValidator()
    {
        RuleFor(x => x.GamesConsoleId)
            .GreaterThan(0).WithMessage("GamesConsoleId must be a positive integer.");
    }
}

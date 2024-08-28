using FluentValidation;

namespace GameShop.Application.Commands.DeleteGameConsole;

public class DeleteGameConsoleCommandValidator : AbstractValidator<DeleteGameConsoleCommand>
{
    public DeleteGameConsoleCommandValidator()
    {
        RuleFor(x => x.GameConsoleId)
            .GreaterThan(0).WithMessage("GameConsoleId must be a positive integer.");
    }
}

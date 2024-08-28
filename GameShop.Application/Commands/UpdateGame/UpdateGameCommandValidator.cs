using FluentValidation;

namespace GameShop.Application.Commands.UpdateGame;

public class UpdateGameCommandValidator : AbstractValidator<UpdateGameCommand>
{
    public UpdateGameCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be a positive integer.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(x => x.Publisher)
            .NotEmpty().WithMessage("Publisher is required.")
            .MaximumLength(100).WithMessage("Publisher must not exceed 100 characters.");

        RuleFor(x => x.GameConsoleId)
            .GreaterThan(0).WithMessage("GameConsoleId must be a positive integer.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0.");
    }
}

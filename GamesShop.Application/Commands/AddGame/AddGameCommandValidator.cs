namespace GamesShop.Application.Commands.AddGame;

using FluentValidation;

public class AddGameCommandValidator : AbstractValidator<AddGameCommand>
{
    public AddGameCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(x => x.Publisher)
            .NotEmpty().WithMessage("Publisher is required.")
            .MaximumLength(100).WithMessage("Publisher must not exceed 100 characters.");

        RuleFor(x => x.GamesConsoleId)
            .GreaterThan(0).WithMessage("GamesConsoleId must be a positive integer.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0.");
    }
}

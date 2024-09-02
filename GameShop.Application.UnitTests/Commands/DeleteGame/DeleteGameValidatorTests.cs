using FluentValidation.TestHelper;
using GameShop.Application.Commands.DeleteGame;

namespace GameShop.Application.UnitTests.Commands.DeleteGame;

public sealed class DeleteGameCommandValidatorTests
{
    private readonly DeleteGameCommandValidator _validator;

    public DeleteGameCommandValidatorTests()
    {
        _validator = new DeleteGameCommandValidator();
    }

    [Fact]
    public void Validator_Should_Have_Error_When_GameId_Is_Zero()
    {
        // Arrange
        var command = new DeleteGameCommand(GameId: 0);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.GameId)
            .WithErrorMessage("GameId must be a positive integer.");
    }

    [Fact]
    public void Validator_Should_Have_Error_When_GameId_Is_Negative()
    {
        // Arrange
        var command = new DeleteGameCommand(GameId: -1);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.GameId)
            .WithErrorMessage("GameId must be a positive integer.");
    }

    [Fact]
    public void Validator_Should_Not_Have_Error_For_Valid_GameId()
    {
        // Arrange
        var command = new DeleteGameCommand(GameId: 1);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}

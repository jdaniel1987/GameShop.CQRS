using FluentValidation.TestHelper;
using GameShop.Application.Commands.DeleteGameConsole;

namespace GameShop.Application.UnitTests.Commands.DeleteGameConsole;

public sealed class DeleteGameConsoleCommandValidatorTests
{
    private readonly DeleteGameConsoleCommandValidator _validator;

    public DeleteGameConsoleCommandValidatorTests()
    {
        _validator = new DeleteGameConsoleCommandValidator();
    }

    [Fact]
    public void Validator_Should_Have_Error_When_GameConsoleId_Is_Zero()
    {
        // Arrange
        var command = new DeleteGameConsoleCommand(GameConsoleId: 0);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.GameConsoleId)
            .WithErrorMessage("GameConsoleId must be a positive integer.");
    }

    [Fact]
    public void Validator_Should_Have_Error_When_GameConsoleId_Is_Negative()
    {
        // Arrange
        var command = new DeleteGameConsoleCommand(GameConsoleId: -1);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.GameConsoleId)
            .WithErrorMessage("GameConsoleId must be a positive integer.");
    }

    [Fact]
    public void Validator_Should_Not_Have_Error_For_Valid_GameConsoleId()
    {
        // Arrange
        var command = new DeleteGameConsoleCommand(GameConsoleId: 1);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}

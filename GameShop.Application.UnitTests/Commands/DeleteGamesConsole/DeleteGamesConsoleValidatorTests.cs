using FluentValidation.TestHelper;
using GameShop.Application.Commands.DeleteGamesConsole;

namespace GameShop.Application.UnitTests.Commands.DeleteGamesConsole;

public class DeleteGamesConsoleCommandValidatorTests
{
    private readonly DeleteGamesConsoleCommandValidator _validator;

    public DeleteGamesConsoleCommandValidatorTests()
    {
        _validator = new DeleteGamesConsoleCommandValidator();
    }

    [Fact]
    public void Validator_Should_Have_Error_When_GamesConsoleId_Is_Zero()
    {
        // Arrange
        var command = new DeleteGamesConsoleCommand(GamesConsoleId: 0);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.GamesConsoleId)
            .WithErrorMessage("GamesConsoleId must be a positive integer.");
    }

    [Fact]
    public void Validator_Should_Have_Error_When_GamesConsoleId_Is_Negative()
    {
        // Arrange
        var command = new DeleteGamesConsoleCommand(GamesConsoleId: -1);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.GamesConsoleId)
            .WithErrorMessage("GamesConsoleId must be a positive integer.");
    }

    [Fact]
    public void Validator_Should_Not_Have_Error_For_Valid_GamesConsoleId()
    {
        // Arrange
        var command = new DeleteGamesConsoleCommand(GamesConsoleId: 1);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}

using FluentValidation.TestHelper;
using GameShop.Application.Commands.AddGame;

namespace GameShop.Application.UnitTests.Commands.AddGame;

public sealed class AddGameCommandValidatorTests
{
    private readonly AddGameCommandValidator _validator;

    public AddGameCommandValidatorTests()
    {
        _validator = new AddGameCommandValidator();
    }

    [Fact]
    public void Validator_Should_Have_Error_When_Name_Is_Empty()
    {
        // Arrange
        var command = new AddGameCommand(
            Name: "",
            Publisher: "Valid Publisher",
            GameConsoleId: 1,
            Price: 10.0);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Name)
            .WithErrorMessage("Name is required.");
    }

    [Fact]
    public void Validator_Should_Have_Error_When_Name_Exceeds_MaxLength()
    {
        // Arrange
        var command = new AddGameCommand(
            Name: new string('A', 101), // 101 characters
            Publisher: "Valid Publisher",
            GameConsoleId: 1,
            Price: 10.0);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Name)
            .WithErrorMessage("Name must not exceed 100 characters.");
    }

    [Fact]
    public void Validator_Should_Have_Error_When_Publisher_Is_Empty()
    {
        // Arrange
        var command = new AddGameCommand(
            Name: "Valid Name",
            Publisher: "",
            GameConsoleId: 1,
            Price: 10.0);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Publisher)
            .WithErrorMessage("Publisher is required.");
    }

    [Fact]
    public void Validator_Should_Have_Error_When_Publisher_Exceeds_MaxLength()
    {
        // Arrange
        var command = new AddGameCommand(
            Name: "Valid Name",
            Publisher: new string('B', 101), // 101 characters
            GameConsoleId: 1,
            Price: 10.0);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Publisher)
            .WithErrorMessage("Publisher must not exceed 100 characters.");
    }

    [Fact]
    public void Validator_Should_Have_Error_When_GameConsoleId_Is_Not_Positive()
    {
        // Arrange
        var command = new AddGameCommand(
            Name: "Valid Name",
            Publisher: "Valid Publisher",
            GameConsoleId: 0,
            Price: 10.0);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.GameConsoleId)
            .WithErrorMessage("GameConsoleId must be a positive integer.");
    }

    [Fact]
    public void Validator_Should_Have_Error_When_Price_Is_Negative()
    {
        // Arrange
        var command = new AddGameCommand(
            Name: "Valid Name",
            Publisher: "Valid Publisher",
            GameConsoleId: 1,
            Price: -1.0);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Price)
            .WithErrorMessage("Price must be greater than or equal to 0.");
    }

    [Fact]
    public void Validator_Should_Not_Have_Error_For_Valid_Command()
    {
        // Arrange
        var command = new AddGameCommand(
            Name: "Valid Name",
            Publisher: "Valid Publisher",
            GameConsoleId: 1,
            Price: 10.0);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}

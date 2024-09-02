using FluentValidation.TestHelper;
using GameShop.Application.Commands.UpdateGame;

namespace GameShop.Application.UnitTests.Commands.UpdateGame;

public sealed class UpdateGameCommandValidatorTests
{
    private readonly UpdateGameCommandValidator _validator;

    public UpdateGameCommandValidatorTests()
    {
        _validator = new UpdateGameCommandValidator();
    }

    [Fact]
    public void Validator_Should_Have_Error_When_Id_Is_Zero()
    {
        // Arrange
        var command = new UpdateGameCommand(Id: 0, Name: "Valid Name", Publisher: "Valid Publisher", GameConsoleId: 1, Price: 10.0);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Id)
            .WithErrorMessage("Id must be a positive integer.");
    }

    [Fact]
    public void Validator_Should_Have_Error_When_Id_Is_Negative()
    {
        // Arrange
        var command = new UpdateGameCommand(Id: -1, Name: "Valid Name", Publisher: "Valid Publisher", GameConsoleId: 1, Price: 10.0);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Id)
            .WithErrorMessage("Id must be a positive integer.");
    }

    [Fact]
    public void Validator_Should_Have_Error_When_Name_Is_Empty()
    {
        // Arrange
        var command = new UpdateGameCommand(Id: 1, Name: "", Publisher: "Valid Publisher", GameConsoleId: 1, Price: 10.0);

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
        var command = new UpdateGameCommand(Id: 1, Name: new string('A', 101), Publisher: "Valid Publisher", GameConsoleId: 1, Price: 10.0);

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
        var command = new UpdateGameCommand(Id: 1, Name: "Valid Name", Publisher: "", GameConsoleId: 1, Price: 10.0);

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
        var command = new UpdateGameCommand(Id: 1, Name: "Valid Name", Publisher: new string('B', 101), GameConsoleId: 1, Price: 10.0);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Publisher)
            .WithErrorMessage("Publisher must not exceed 100 characters.");
    }

    [Fact]
    public void Validator_Should_Have_Error_When_GameConsoleId_Is_Zero()
    {
        // Arrange
        var command = new UpdateGameCommand(Id: 1, Name: "Valid Name", Publisher: "Valid Publisher", GameConsoleId: 0, Price: 10.0);

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
        var command = new UpdateGameCommand(Id: 1, Name: "Valid Name", Publisher: "Valid Publisher", GameConsoleId: -1, Price: 10.0);

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
        var command = new UpdateGameCommand(Id: 1, Name: "Valid Name", Publisher: "Valid Publisher", GameConsoleId: 1, Price: -1.0);

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
        var command = new UpdateGameCommand(Id: 1, Name: "Valid Name", Publisher: "Valid Publisher", GameConsoleId: 1, Price: 10.0);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}

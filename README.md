# GamesShop

This project simulates a simple video game shop, with the main entities being Games and GamesConsoles. 

# The tech stack used to build source code is:  
- MediatR: Helps abstract the presentation layer from other layers.  
- Carter: Used to separate minimal API endpoints from the Program.cs file.  
- CSharpFunctionalExtensions: Adds the IResult object to abstract application layer handler responses from presentation layer responses.  
- FluentValidation: Provides easy-to-understand validators for command and query classes.  
- MediatR.Extensions.FluentValidation.AspNetCore: Enables automatic validation when commands and queries are sent to MediatR handlers.  
- Microsoft.EntityFrameworkCore: ORM to facilitate database access.  

# The tech stack used to build unit and integration tests is:  
- FluentAssertions: Allows creating fluent and easy-to-understand assertions.  
- AutoFixture: Helps create objects automatically for use in tests.
- AutoFixture.AutomMoq: Creates mocks for interfaces automatically when used as test parameters.
- ILogger.Moq: Helps create and assert on logger mocks.
- Microsoft.EntityFrameworkCore.InMemory: Replaces SQL Server database with an in-memory one for testing purposes.

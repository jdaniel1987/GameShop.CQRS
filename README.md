# GameShop

This project simulates a simple video game shop, with the main entities being Games and GamesConsoles. It is built following the principles of **CQRS** (Command and Query Responsibility Segregation), **Clean Architecture**, **DDD** (Domain-Driven Design), and **Minimal APIs**.

# Architecture and Design
- **CQRS** (Command and Query Responsibility Segregation): The project implements CQRS by using two different DbContexts pointing to the same SQL Server database. One of these DbContexts is configured for read-only purposes. This separation allows for optimized handling of read and write operations.  
- **Clean Architecture**: The project adheres to Clean Architecture principles, which helps in organizing the codebase into well-defined layers and ensuring that the core business logic is isolated from external concerns.  
- **DDD** (Domain-Driven Design): The project uses DDD concepts to model the business domain effectively, focusing on the main entities (Games and GamesConsoles) and their interactions.  
- **Minimal APIs**: The project utilizes Minimal APIs to streamline the API development process, making it easier to manage and maintain API endpoints.
- **Entity Configurations**: Rather than using an abstraction layer to separate Domain models from Database models, I opted to use entity configurations. This approach involves directly mapping Domain models to Database models through configurations, simplifying the translation between them.  

# Scalability and Separation
- API Separation: The API is divided into commands and queries, allowing for independent scaling of read operations. This design facilitates scaling the read API separately if needed, improving performance and resource utilization.  

# The tech stack used to build source code is:  
- **MediatR**: Helps abstract the presentation layer from other layers.  
- **Carter**: Used to separate minimal API endpoints from the Program.cs file.  
- **CSharpFunctionalExtensions**: Adds the IResult object to abstract application layer handler responses from presentation layer responses.  
- **FluentValidation**: Provides easy-to-understand validators for command and query classes.  
- **MediatR.Extensions.FluentValidation.AspNetCore**: Enables automatic validation when commands and queries are sent to MediatR handlers.  
- **Microsoft.EntityFrameworkCore**: ORM to facilitate database access.  

# The tech stack used to build unit and integration tests is:  
- **FluentAssertions**: Allows creating fluent and easy-to-understand assertions.  
- **AutoFixture**: Helps create objects automatically for use in tests.
- **AutoFixture.AutomMoq**: Creates mocks for interfaces automatically when used as test parameters.
- **ILogger.Moq**: Helps create and assert on logger mocks.
- **Microsoft.EntityFrameworkCore.InMemory**: Replaces SQL Server database with an in-memory one for testing purposes.

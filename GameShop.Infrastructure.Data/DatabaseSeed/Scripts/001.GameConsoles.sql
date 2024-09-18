CREATE TABLE [dbo].[GameConsoles] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [Name]         VARCHAR (MAX)   NOT NULL,
    [Manufacturer] VARCHAR (MAX)   NOT NULL,
    [Price]        NUMERIC (18, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

INSERT INTO [dbo].[GameConsoles] ([Name], [Manufacturer], [Price])
VALUES
('PlayStation 5', 'Sony', 499.99),
('Xbox Series X', 'Microsoft', 599.99),
('Nintendo Switch', 'Nintendo', 299.99),
('PlayStation 4', 'Sony', 149.99),
('Xbox One', 'Microsoft', 199.99);

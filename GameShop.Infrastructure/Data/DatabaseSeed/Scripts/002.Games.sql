CREATE TABLE [dbo].[Games] (
    [Id]             INT             IDENTITY (1, 1) NOT NULL,
    [Name]           VARCHAR (MAX)   NOT NULL,
    [Publisher]      VARCHAR (MAX)   NOT NULL,
    [Price]          DECIMAL (18, 2) NOT NULL,
    [GameConsoleId] INT             NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Games_ToGameConsole] FOREIGN KEY ([GameConsoleId]) REFERENCES [dbo].[GameConsoles] ([Id])
);

INSERT INTO [dbo].[Games] ([Name], [Publisher], [Price], [GameConsoleId])
VALUES
('Spider-Man: Miles Morales', 'Sony Interactive Entertainment', 49.99, 1),
('Halo Infinite', 'Xbox Game Studios', 59.99, 2),
('The Legend of Zelda: Breath of the Wild', 'Nintendo', 59.99, 3),
('The Legend of Zelda: Tears of the kingdom', 'Nintendo', 59.99, 3),
('God of War', 'Sony Interactive Entertainment', 19.99, 4),
('Forza Horizon 4', 'Xbox Game Studios', 39.99, 5);

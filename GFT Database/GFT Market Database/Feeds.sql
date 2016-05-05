CREATE TABLE [dbo].[Feeds]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ItemName] VARCHAR(50) NOT NULL, 
    [Quantity] INT NOT NULL, 
    [OperationType] VARCHAR(50) NOT NULL, 
    [Price] INT NOT NULL DEFAULT 0
)

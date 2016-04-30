CREATE TABLE [dbo].[Feeds]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [ItemName] VARCHAR(50) NOT NULL, 
    [Quantity] INT NOT NULL, 
    [OperationType] VARCHAR(50) NOT NULL
)

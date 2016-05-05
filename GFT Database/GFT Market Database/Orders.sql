CREATE TABLE [dbo].[Orders]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [OrderID] INT NOT NULL, 
    [OrderType] VARCHAR(50) NOT NULL, 
    [Quantity] INT NOT NULL, 
    [Price] INT NOT NULL, 
    [ItemID] INT NOT NULL, 
    CONSTRAINT [ItemID_FK] FOREIGN KEY (ItemID) REFERENCES Items(Id)
)

GO

CREATE TABLE [dbo].[Orders]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [OrderID] NCHAR(10) NOT NULL, 
    [OrderType] VARCHAR(50) NOT NULL, 
    [Quantity] INT NOT NULL, 
    [Price] INT NOT NULL, 
    [ItemID] INT NOT NULL, 
    CONSTRAINT [ItemID_FK] FOREIGN KEY (ItemID) REFERENCES Items(ID)
)

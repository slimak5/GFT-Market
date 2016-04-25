CREATE TABLE [dbo].[Items]
(
	[id] INT NOT NULL PRIMARY KEY, 
    [name] TEXT NOT NULL DEFAULT 'item.name', 
    [quantity] INT NOT NULL DEFAULT 1
)

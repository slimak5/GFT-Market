CREATE TABLE [dbo].[Items] (
    [itemId]             INT            IDENTITY (1, 1) NOT NULL,
    [itemName]           NVARCHAR (MAX) NOT NULL,
    [supportedServiceId] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_dbo.Items] PRIMARY KEY CLUSTERED ([itemId] ASC)
);


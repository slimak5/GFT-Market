CREATE TABLE [dbo].[Transactions] (
    [transactionId]      INT            IDENTITY (1, 1) NOT NULL,
    [clientId]           NVARCHAR (MAX) NOT NULL,
    [transactionDate]    DATETIME       NOT NULL,
    [sellOrderId]        NVARCHAR (MAX) NOT NULL,
    [buyOrderId]         NVARCHAR (MAX) NOT NULL,
    [orderedItem_itemId] INT            NOT NULL,
    CONSTRAINT [PK_dbo.Transactions] PRIMARY KEY CLUSTERED ([transactionId] ASC),
    CONSTRAINT [FK_dbo.Transactions_dbo.Items_orderedItem_itemId] FOREIGN KEY ([orderedItem_itemId]) REFERENCES [dbo].[Items] ([itemId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_orderedItem_itemId]
    ON [dbo].[Transactions]([orderedItem_itemId] ASC);


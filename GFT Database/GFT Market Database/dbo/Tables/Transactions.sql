CREATE TABLE [dbo].[Transactions] (
    [transactionId]   INT      IDENTITY (1, 1) NOT NULL,
    [clientId]        INT      NOT NULL,
    [transactionDate] DATETIME NOT NULL,
    [sellOrderId]     INT      NOT NULL,
    [buyOrderId]      INT      NOT NULL,
    [itemId]          INT      NOT NULL,
    [quantity]        INT      DEFAULT ((0)) NOT NULL,
    [price]           INT      DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_dbo.Transactions] PRIMARY KEY CLUSTERED ([transactionId] ASC),
    CONSTRAINT [FK_dbo.Transactions_dbo.Items_orderedItem_itemId] FOREIGN KEY ([itemId]) REFERENCES [dbo].[Items] ([itemId]) ON DELETE CASCADE
);






GO
CREATE NONCLUSTERED INDEX [IX_itemId]
    ON [dbo].[Transactions]([itemId] ASC);


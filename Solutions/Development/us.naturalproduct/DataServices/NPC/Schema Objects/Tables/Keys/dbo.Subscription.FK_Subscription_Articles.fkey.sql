ALTER TABLE [dbo].[Subscription] WITH NOCHECK ADD
CONSTRAINT [FK_Subscription_Articles] FOREIGN KEY ([ArticleId]) REFERENCES [dbo].[Articles] ([ArticleId]) ON DELETE CASCADE NOT FOR REPLICATION



ALTER TABLE [dbo].[ArticleDocuments] WITH NOCHECK ADD
CONSTRAINT [FK_ArticleDocuments_Articles] FOREIGN KEY ([ArticleId]) REFERENCES [dbo].[Articles] ([ArticleId]) NOT FOR REPLICATION



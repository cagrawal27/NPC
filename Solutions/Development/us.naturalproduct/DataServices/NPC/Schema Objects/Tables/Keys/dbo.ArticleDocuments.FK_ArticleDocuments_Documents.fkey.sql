ALTER TABLE [dbo].[ArticleDocuments] WITH NOCHECK ADD
CONSTRAINT [FK_ArticleDocuments_Documents] FOREIGN KEY ([DocId]) REFERENCES [dbo].[Documents] ([DocId]) NOT FOR REPLICATION



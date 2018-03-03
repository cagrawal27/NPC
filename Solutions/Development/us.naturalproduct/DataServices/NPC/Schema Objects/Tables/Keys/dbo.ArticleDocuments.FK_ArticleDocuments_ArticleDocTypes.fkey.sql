ALTER TABLE [dbo].[ArticleDocuments] WITH NOCHECK ADD
CONSTRAINT [FK_ArticleDocuments_ArticleDocTypes] FOREIGN KEY ([ArtDocTypeId]) REFERENCES [dbo].[ArticleDocTypes] ([ArtDocTypeId]) NOT FOR REPLICATION



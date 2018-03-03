ALTER TABLE [dbo].[IssueArticles] WITH NOCHECK ADD
CONSTRAINT [FK_IssueArticles_Articles] FOREIGN KEY ([ArticleId]) REFERENCES [dbo].[Articles] ([ArticleId]) NOT FOR REPLICATION



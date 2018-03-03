ALTER TABLE [dbo].[IssueDocuments] WITH NOCHECK ADD
CONSTRAINT [FK_IssueDocuments_IssueDocTypes] FOREIGN KEY ([IssueDocTypeId]) REFERENCES [dbo].[IssueDocTypes] ([IssueDocTypeId]) NOT FOR REPLICATION



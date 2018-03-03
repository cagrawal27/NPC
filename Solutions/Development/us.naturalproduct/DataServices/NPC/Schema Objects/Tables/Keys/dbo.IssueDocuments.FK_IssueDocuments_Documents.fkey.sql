ALTER TABLE [dbo].[IssueDocuments] WITH NOCHECK ADD
CONSTRAINT [FK_IssueDocuments_Documents] FOREIGN KEY ([DocId]) REFERENCES [dbo].[Documents] ([DocId]) NOT FOR REPLICATION



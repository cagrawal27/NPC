ALTER TABLE [dbo].[IssueDocuments] WITH NOCHECK ADD
CONSTRAINT [FK_IssueDocuments_VolumeIssues] FOREIGN KEY ([VolumeIssueId]) REFERENCES [dbo].[VolumeIssues] ([VolumeIssueId]) NOT FOR REPLICATION



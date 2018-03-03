ALTER TABLE [dbo].[IssueArticles] WITH NOCHECK ADD
CONSTRAINT [FK_IssueArticles_VolumeIssues] FOREIGN KEY ([VolumeIssueId]) REFERENCES [dbo].[VolumeIssues] ([VolumeIssueId]) NOT FOR REPLICATION



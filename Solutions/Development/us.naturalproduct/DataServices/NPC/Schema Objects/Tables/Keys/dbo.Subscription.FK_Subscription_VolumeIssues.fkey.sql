ALTER TABLE [dbo].[Subscription] WITH NOCHECK ADD
CONSTRAINT [FK_Subscription_VolumeIssues] FOREIGN KEY ([VolumeIssueId]) REFERENCES [dbo].[VolumeIssues] ([VolumeIssueId]) ON DELETE CASCADE NOT FOR REPLICATION



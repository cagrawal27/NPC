ALTER TABLE [dbo].[Users] WITH NOCHECK ADD
CONSTRAINT [FK_Users_AccountStatus] FOREIGN KEY ([AccountStatus]) REFERENCES [dbo].[AccountStatus] ([AccountStatusID]) NOT FOR REPLICATION



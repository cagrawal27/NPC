ALTER TABLE [dbo].[Users] WITH NOCHECK ADD
CONSTRAINT [FK_Users_AccountTypes] FOREIGN KEY ([AccountTypeId]) REFERENCES [dbo].[AccountTypes] ([AccountTypeId]) NOT FOR REPLICATION



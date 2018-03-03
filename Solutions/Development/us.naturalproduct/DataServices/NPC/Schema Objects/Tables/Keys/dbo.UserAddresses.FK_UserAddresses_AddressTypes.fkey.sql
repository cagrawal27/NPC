ALTER TABLE [dbo].[UserAddresses] WITH NOCHECK ADD
CONSTRAINT [FK_UserAddresses_AddressTypes] FOREIGN KEY ([AddressTypeId]) REFERENCES [dbo].[AddressTypes] ([AddressTypeId]) NOT FOR REPLICATION



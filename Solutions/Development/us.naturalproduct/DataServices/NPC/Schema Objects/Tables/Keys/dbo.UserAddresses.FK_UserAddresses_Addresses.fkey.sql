ALTER TABLE [dbo].[UserAddresses] WITH NOCHECK ADD
CONSTRAINT [FK_UserAddresses_Addresses] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Addresses] ([AddressId]) ON DELETE CASCADE NOT FOR REPLICATION



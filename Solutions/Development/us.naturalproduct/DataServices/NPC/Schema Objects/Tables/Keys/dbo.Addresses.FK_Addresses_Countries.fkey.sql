ALTER TABLE [dbo].[Addresses] WITH NOCHECK ADD
CONSTRAINT [FK_Addresses_Countries] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Countries] ([CountryId]) NOT FOR REPLICATION



CREATE TABLE [dbo].[Addresses]
(
[AddressId] [int] NOT NULL IDENTITY(1, 1),
[Line1] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Line2] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[City] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[StateProvince] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CountryId] [int] NOT NULL,
[Phone] [varchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Fax] [varchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Active] [bit] NOT NULL,
[CreationUserId] [int] NOT NULL,
[CreationDateTime] [datetime] NOT NULL,
[UpdateUserId] [int] NOT NULL,
[UpdateDateTime] [datetime] NOT NULL
) ON [PRIMARY]



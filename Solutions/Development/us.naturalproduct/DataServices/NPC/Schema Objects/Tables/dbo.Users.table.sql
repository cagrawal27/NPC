CREATE TABLE [dbo].[Users]
(
[UserId] [int] NOT NULL IDENTITY(1, 1),
[Email] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[PasswordHash] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[PasswordSalt] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[FirstName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[LastName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[MiddleInitial] [varchar] (5) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[SecretQuestion1Id] [int] NOT NULL,
[SecretQuestion2Id] [int] NOT NULL,
[SecretAnswer1Hash] [varchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[SecretAnswer2Hash] [varchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[AccountTypeId] [int] NOT NULL,
[AccountStatus] [int] NOT NULL,
[Active] [bit] NOT NULL,
[CreationUserId] [int] NOT NULL,
[CreationDateTime] [datetime] NOT NULL,
[UpdateUserId] [int] NOT NULL,
[UpdateDateTime] [datetime] NOT NULL
) ON [PRIMARY]



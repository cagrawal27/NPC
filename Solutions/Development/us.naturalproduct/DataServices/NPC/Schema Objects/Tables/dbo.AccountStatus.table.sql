CREATE TABLE [dbo].[AccountStatus]
(
[AccountStatusID] [int] NOT NULL,
[AccountStatusDescription] [varchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Active] [bit] NOT NULL,
[CreateUserId] [int] NOT NULL,
[CreateDateTime] [datetime] NOT NULL,
[UpdateUserId] [int] NOT NULL,
[UpdateDateTime] [datetime] NOT NULL
) ON [PRIMARY]



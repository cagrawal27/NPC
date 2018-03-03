CREATE TABLE [dbo].[LoginLog]
(
[DateTime] [datetime] NOT NULL,
[IPAddress] [varchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Email] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[SuccessFlag] [bit] NOT NULL
) ON [PRIMARY]



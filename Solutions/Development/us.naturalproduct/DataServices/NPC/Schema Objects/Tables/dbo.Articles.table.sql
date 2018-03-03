CREATE TABLE [dbo].[Articles]
(
[ArticleId] [int] NOT NULL IDENTITY(1, 1),
[Title] [varchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Authors] [varchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Keywords] [varchar] (2000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Active] [bit] NOT NULL,
[CreationUserId] [int] NOT NULL,
[CreationDateTime] [datetime] NOT NULL,
[UpdateUserId] [int] NOT NULL,
[UpdateDateTime] [datetime] NOT NULL,
[PageNumber] [int] NULL
) ON [PRIMARY]



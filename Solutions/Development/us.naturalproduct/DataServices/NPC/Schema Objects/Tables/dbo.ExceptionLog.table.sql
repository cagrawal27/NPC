CREATE TABLE [dbo].[ExceptionLog]
(
[EventId] [int] NOT NULL IDENTITY(1, 1),
[LogDateTime] [datetime] NOT NULL,
[Source] [char] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Message] [varchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Form] [varchar] (4000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[QueryString] [varchar] (2000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[TargetSite] [varchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[StackTrace] [varchar] (4000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Referrer] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]



CREATE TABLE [dbo].[dtproperties]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[objectid] [int] NULL,
[property] [varchar] (64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[value] [varchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[uvalue] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[lvalue] [image] NULL,
[version] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]



CREATE TABLE [dbo].[Documents]
(
[DocId] [int] NOT NULL IDENTITY(1, 1),
[Data] [image] NOT NULL,
[FileName] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[FileSizeKB] [int] NOT NULL,
[Comments] [varchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Active] [bit] NOT NULL,
[CreationUserId] [int] NOT NULL,
[CreationDateTime] [datetime] NOT NULL,
[UpdateUserId] [int] NOT NULL,
[UpdateDateTime] [datetime] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]



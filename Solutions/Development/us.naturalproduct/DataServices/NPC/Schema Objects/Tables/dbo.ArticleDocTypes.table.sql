CREATE TABLE [dbo].[ArticleDocTypes]
(
[ArtDocTypeId] [int] NOT NULL IDENTITY(1, 1),
[ArtDocTypeDescription] [varchar] (80) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Active] [bit] NOT NULL,
[CreationUserId] [int] NOT NULL,
[CreationDateTime] [datetime] NOT NULL,
[UpdateUserId] [int] NOT NULL,
[UpdateDateTime] [datetime] NOT NULL,
[PubliclyAvailable] [bit] NOT NULL
) ON [PRIMARY]



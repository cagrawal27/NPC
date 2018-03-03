CREATE TABLE [dbo].[Volumes]
(
[VolumeId] [int] NOT NULL IDENTITY(1, 1),
[VolumeName] [varchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[VolumeYear] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Active] [bit] NOT NULL,
[CreationUserId] [int] NOT NULL,
[CreationDateTime] [datetime] NOT NULL,
[UpdateUserId] [int] NOT NULL,
[UpdateDateTime] [datetime] NOT NULL
) ON [PRIMARY]



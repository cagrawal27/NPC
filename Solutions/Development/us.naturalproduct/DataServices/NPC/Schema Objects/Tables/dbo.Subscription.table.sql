CREATE TABLE [dbo].[Subscription]
(
[SubscriptionId] [int] NOT NULL IDENTITY(1, 1),
[VolumeIssueId] [int] NOT NULL,
[ArticleId] [int] NOT NULL,
[UserId] [int] NOT NULL,
[EffectiveDate] [datetime] NOT NULL,
[ExpirationDate] [datetime] NOT NULL,
[Active] [bit] NOT NULL,
[CreationUserId] [int] NOT NULL,
[CreationDateTime] [datetime] NOT NULL,
[UpdateUserId] [int] NOT NULL,
[UpdateDateTime] [datetime] NOT NULL
) ON [PRIMARY]



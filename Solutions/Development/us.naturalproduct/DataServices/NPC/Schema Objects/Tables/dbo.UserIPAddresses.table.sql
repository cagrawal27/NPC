CREATE TABLE [dbo].[UserIPAddresses]
(
[UserIPId] [int] NOT NULL IDENTITY(1, 1),
[UserId] [int] NOT NULL,
[IPOctet1Begin] [tinyint] NOT NULL,
[IPOctet2Begin] [tinyint] NOT NULL,
[IPOctet3Begin] [tinyint] NOT NULL,
[IPOctet4Begin] [tinyint] NOT NULL,
[IPOctet4End] [tinyint] NOT NULL,
[IPOctet3End] [tinyint] NULL
) ON [PRIMARY]



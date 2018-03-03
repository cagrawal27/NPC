SET  ARITHABORT, CONCAT_NULL_YIELDS_NULL, ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, QUOTED_IDENTIFIER ON 
SET  NUMERIC_ROUNDABORT OFF
GO
:setvar databasename "NPC"

USE [master]

GO

:on error exit

IF  (   DB_ID(N'$(databasename)') IS NOT NULL
    AND DATABASEPROPERTYEX(N'$(databasename)','Status') <> N'ONLINE')
BEGIN
    RAISERROR(N'The state of the target database, %s, is not set to ONLINE. To deploy to this database, its state must be set to ONLINE.', 16, 127,N'$(databasename)') WITH NOWAIT
    RETURN
END
GO

:on error resume
     
IF (DB_ID(N'$(databasename)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(databasename)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(databasename)];
END
GO
CREATE DATABASE [$(databasename)] COLLATE SQL_Latin1_General_CP1_CS_AS;

GO

EXEC sp_dbcmptlevel N'$(databasename)', 90

GO

IF EXISTS (SELECT 1 FROM [sys].[databases] WHERE [name] = N'$(databasename)') 
    ALTER DATABASE [$(databasename)] SET  
	ALLOW_SNAPSHOT_ISOLATION OFF
GO

IF EXISTS (SELECT 1 FROM [sys].[databases] WHERE [name] = N'$(databasename)') 
    ALTER DATABASE [$(databasename)] SET  
	READ_COMMITTED_SNAPSHOT OFF
GO

IF EXISTS (SELECT 1 FROM [sys].[databases] WHERE [name] = N'$(databasename)') 
    ALTER DATABASE [$(databasename)] SET  
	MULTI_USER,
	CURSOR_CLOSE_ON_COMMIT OFF,
	CURSOR_DEFAULT LOCAL,
	AUTO_CLOSE OFF,
	AUTO_CREATE_STATISTICS ON,
	AUTO_SHRINK OFF,
	AUTO_UPDATE_STATISTICS ON,
	AUTO_UPDATE_STATISTICS_ASYNC ON,
	ANSI_NULL_DEFAULT ON,
	ANSI_NULLS ON,
	ANSI_PADDING ON,
	ANSI_WARNINGS ON,
	ARITHABORT ON,
	CONCAT_NULL_YIELDS_NULL ON,
	NUMERIC_ROUNDABORT OFF,
	QUOTED_IDENTIFIER ON,
	RECURSIVE_TRIGGERS OFF,
	RECOVERY FULL,
	PAGE_VERIFY NONE,
	DISABLE_BROKER,
	PARAMETERIZATION SIMPLE
	WITH ROLLBACK IMMEDIATE
GO

IF IS_SRVROLEMEMBER ('sysadmin') = 1
BEGIN

IF EXISTS (SELECT 1 FROM [sys].[databases] WHERE [name] = N'$(databasename)') 
    EXEC sp_executesql N'
    ALTER DATABASE [$(databasename)] SET  
	DB_CHAINING OFF,
	TRUSTWORTHY OFF'

END
ELSE
BEGIN
    RAISERROR(N'Unable to modify the database settings for DB_CHAINING or TRUSTWORTHY. You must be a SysAdmin in order to apply these settings.',0,1)
END

GO

USE [$(databasename)]

GO
-- Pre-Deployment Script Template							
----------------------------------------------------------------------------------------
-- This file contains SQL statements that will be executed before the build script	
-- Use SQLCMD syntax to include a file into the pre-deployment script			
-- Example:      :r .\filename.sql								
-- Use SQLCMD syntax to reference a variable in the pre-deployment script		
-- Example:      :setvar $TableName MyTable							
--               SELECT * FROM [$(TableName)]					
----------------------------------------------------------------------------------------

/*
IF NOT EXISTS (SELECT * FROM master.dbo.syslogins WHERE loginname = N'NPC_WEB_USR')
CREATE LOGIN [NPC_WEB_USR] WITH PASSWORD = 'Aíaë|YNW1|ã¿pù%­msFT7_&#$!~<WCó­îÖbLa¢'
*/

GO

/*
IF NOT EXISTS (SELECT * FROM master.dbo.syslogins WHERE loginname = N'NPC_WEB_USR')
CREATE LOGIN [NPC_WEB_USR] WITH PASSWORD = 'à~p©lçS6èfÔa¾¯;¹ÚmsFT7_&#$!~<û cæ´!ayäC'
*/

GO

GO








GO
PRINT N'Creating role db_npcwebrole'
GO
CREATE ROLE [db_npcwebrole]
AUTHORIZATION [dbo]
GO
PRINT N'Creating [dbo].[UserIPAddresses]'
GO
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
GO
PRINT N'Creating primary key [PK_UserIPAddresses] on [dbo].[UserIPAddresses]'
GO
ALTER TABLE [dbo].[UserIPAddresses] ADD CONSTRAINT [PK_UserIPAddresses] PRIMARY KEY CLUSTERED  ([UserIPId], [IPOctet1Begin]) ON [PRIMARY]
GO
PRINT N'Creating index [IX_UserIPAddresses] on [dbo].[UserIPAddresses]'
GO
CREATE NONCLUSTERED INDEX [IX_UserIPAddresses] ON [dbo].[UserIPAddresses] ([IPOctet1Begin]) ON [PRIMARY]
GO
PRINT N'Creating [dbo].[spDeleteUserIPAddress]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/19/2006
-- Description:	Deletes records from the documents
--		and issuedocuments tables
-- =============================================
CREATE PROCEDURE [dbo].[spDeleteUserIPAddress] 
(
	@UserIPId	int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int
		
	DELETE FROM [dbo].[UserIPAddresses]
	WHERE 
		UserIPId = @UserIPId

	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
		GOTO FAIL
	ELSE
		RETURN 0

FAIL:
 BEGIN
	RAISERROR('An error occured deleting user ip record.', 16, 1)	
	RETURN -1
 END


END
GO
PRINT N'Creating [dbo].[Subscription]'
GO
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
GO
PRINT N'Creating primary key [PK_Subscription] on [dbo].[Subscription]'
GO
ALTER TABLE [dbo].[Subscription] ADD CONSTRAINT [PK_Subscription] PRIMARY KEY CLUSTERED  ([SubscriptionId]) ON [PRIMARY]
GO
PRINT N'Creating [dbo].[spGetUserIPAddressByIP]'
GO
/****** Object:  Stored Procedure dbo.spGetUserIPAddressByIP   ****/

-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 04/10/2007
-- Description:	Checks if an IP address already
--		exists.
-- =============================================
CREATE   PROCEDURE [dbo].[spGetUserIPAddressByIP] 
(
	@IPOctet1	int,
	@IPOctet2	int,
	@IPOctet3	int,
	@IPOctet4	int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int,
		@UserId	int

	SELECT
		UserId 
	FROM
		UserIPAddresses
	WHERE
		IPOctet1Begin = @IPOctet1
		AND
		IPOctet2Begin = @IPOctet2
		AND
		@IPOctet3 BETWEEN IPOctet3Begin AND IPOctet3End
		AND
		@IPOctet4	BETWEEN IPOctet4Begin AND IPOctet4End	
		
	SELECT @ERROR = @@ERROR
	
	IF (@ERROR <> 0)
		RAISERROR('Failed to determine UserId from IP address', 10, 1)

END
GO
PRINT N'Creating [dbo].[AccountTypes]'
GO
CREATE TABLE [dbo].[AccountTypes]
(
[AccountTypeId] [int] NOT NULL,
[Description] [varchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Active] [bit] NOT NULL,
[CreationUserId] [int] NOT NULL,
[CreationDateTime] [datetime] NOT NULL,
[UpdateUserId] [int] NOT NULL,
[UpdateDateTime] [datetime] NOT NULL
) ON [PRIMARY]
GO
PRINT N'Creating primary key [PK__AccountTypes__15502E78] on [dbo].[AccountTypes]'
GO
ALTER TABLE [dbo].[AccountTypes] ADD CONSTRAINT [PK__AccountTypes__15502E78] PRIMARY KEY CLUSTERED  ([AccountTypeId]) ON [PRIMARY]
GO
PRINT N'Creating [dbo].[Issues]'
GO
CREATE TABLE [dbo].[Issues]
(
[IssueId] [int] NOT NULL IDENTITY(1, 1),
[IssueName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Active] [bit] NOT NULL,
[CreationUserId] [int] NOT NULL,
[CreationDateTime] [datetime] NOT NULL,
[UpdateUserId] [int] NOT NULL,
[UpdateDateTime] [datetime] NOT NULL
) ON [PRIMARY]
GO
PRINT N'Creating primary key [PK__Issues__07020F21] on [dbo].[Issues]'
GO
ALTER TABLE [dbo].[Issues] ADD CONSTRAINT [PK__Issues__07020F21] PRIMARY KEY CLUSTERED  ([IssueId]) ON [PRIMARY]
GO
PRINT N'Creating [dbo].[Numbers]'
GO
CREATE TABLE [dbo].[Numbers]
(
[Number] [int] NOT NULL IDENTITY(1, 1),
[NullCol] [int] NULL
) ON [PRIMARY]
GO
PRINT N'Creating primary key [PK_Numbers] on [dbo].[Numbers]'
GO
ALTER TABLE [dbo].[Numbers] ADD CONSTRAINT [PK_Numbers] PRIMARY KEY CLUSTERED  ([Number]) ON [PRIMARY]
GO
PRINT N'Creating [dbo].[TabulateCharSV]'
GO
/********************************************************************
**
**	NAME: 	TabulateCharSV
**
**	AUTHOR: Monish Nagisetty
**
**	DATE: 	01/04/2005
**
**	DESC: 	Converts a list of character seperated values into a table.  
**		Can return up to 50 items each with a max length of 100 characters.
**
**	INPUT:	@CSV - Comma seperated value 
**		@Char - The delimiting character
**		Ex:  ab,cdef,gh,ijklmn,op
**
**	OUTPUT:	Result set containing a list of successful/unsuccesful
**		cases of the bulk 
**
**	UPDATES:
**	DATE	UPDATED BY		COMMENTS
**	
**
**
********************************************************************/
--Multi-statement Table-Valued User-Defined Function
CREATE     FUNCTION dbo.TabulateCharSV
(
	@CSV varchar(8000),
	@Char char(1)
)
RETURNS @TabularData Table
(
	[ID]		int,
	[Value]		varchar(1000)
)
AS  
BEGIN 
	INSERT INTO @TabularData
	(
		[ID],
		[Value]
	)
	(
		SELECT 
			1 - LEN(REPLACE(LEFT(@CSV, Number), @Char, SPACE(0))) + Number AS [ID],
			SUBSTRING(@CSV, Number, CHARINDEX(@Char, @CSV + @Char, Number) - Number) AS [Value]
		FROM 
			Numbers
		WHERE 
			SUBSTRING(@Char + @CSV, Number, 1) = @Char
			AND 
			Number < LEN(@CSV) + 1
	)
	
	RETURN
END
GO
PRINT N'Creating [dbo].[Documents]'
GO
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
GO
PRINT N'Creating primary key [PK_Documents] on [dbo].[Documents]'
GO
ALTER TABLE [dbo].[Documents] ADD CONSTRAINT [PK_Documents] PRIMARY KEY CLUSTERED  ([DocId]) ON [PRIMARY]
GO
PRINT N'Creating [dbo].[IssueArticles]'
GO
CREATE TABLE [dbo].[IssueArticles]
(
[VolumeIssueId] [int] NOT NULL,
[ArticleId] [int] NOT NULL
) ON [PRIMARY]
GO
PRINT N'Creating primary key [PK_IssueArticles] on [dbo].[IssueArticles]'
GO
ALTER TABLE [dbo].[IssueArticles] ADD CONSTRAINT [PK_IssueArticles] PRIMARY KEY CLUSTERED  ([VolumeIssueId], [ArticleId]) ON [PRIMARY]
GO
PRINT N'Creating [dbo].[ArticleDocuments]'
GO
CREATE TABLE [dbo].[ArticleDocuments]
(
[ArticleId] [int] NOT NULL,
[ArtDocTypeId] [int] NOT NULL,
[DocId] [int] NOT NULL
) ON [PRIMARY]
GO
PRINT N'Creating primary key [PK_ArticleDocuments] on [dbo].[ArticleDocuments]'
GO
ALTER TABLE [dbo].[ArticleDocuments] ADD CONSTRAINT [PK_ArticleDocuments] PRIMARY KEY CLUSTERED  ([ArticleId], [ArtDocTypeId], [DocId]) ON [PRIMARY]
GO
PRINT N'Creating [dbo].[spInsertArticleDoc]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/18/2006
-- Description:	Inserts records into the documents
--		and articledocuments tables for
--		creating an article document.
-- =============================================
CREATE    PROCEDURE [dbo].[spInsertArticleDoc] 
(
	 @ArticleId		int
	,@ArtDocTypeId		int
	,@Data			image
	,@FileName		varchar(100)
	,@FileSizeKB		int
	,@Comments		varchar(500)
	,@Active		bit
	,@CreationUserId	int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int,
		@RC		int,
		@DocId		int

	INSERT INTO [dbo].[Documents]
	(
		[Data], 
		[FileName], 
		[FileSizeKB], 
		[Comments], 
		[Active], 
		[CreationUserId], 
		[CreationDateTime], 
		[UpdateUserId], 
		[UpdateDateTime]
	)
	VALUES
	(
		 @Data
		,@FileName
		,@FileSizeKB
		,@Comments
		,@Active
		,@CreationUserId
		,GETDATE()
		,@CreationUserId
		,GETDATE()
	)

	SELECT @ERROR = @@ERROR, @DocId = @@IDENTITY

	IF (@ERROR <> 0)
		GOTO FAIL

	--Add this article to the issue
	INSERT INTO [dbo].[ArticleDocuments]
	(
		[ArticleId], 
		[ArtDocTypeId], 
		[DocId]
	)
	VALUES
	(
		 @ArticleId
		,@ArtDocTypeId
		,@DocId
	)
		
	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
		GOTO FAIL
	ELSE
		RETURN 0

FAIL:
 BEGIN
	RAISERROR('An error occured adding a article document.', 16, 1)	
	RETURN -1
 END


END
GO
PRINT N'Creating [dbo].[VolumeIssues]'
GO
CREATE TABLE [dbo].[VolumeIssues]
(
[VolumeIssueId] [int] NOT NULL IDENTITY(1, 1),
[VolumeId] [int] NOT NULL,
[IssueId] [int] NOT NULL
) ON [PRIMARY]
GO
PRINT N'Creating primary key [PK_VolumeIssues] on [dbo].[VolumeIssues]'
GO
ALTER TABLE [dbo].[VolumeIssues] ADD CONSTRAINT [PK_VolumeIssues] PRIMARY KEY CLUSTERED  ([VolumeIssueId]) ON [PRIMARY]
GO
PRINT N'Creating [dbo].[spUpdateIssue]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/09/2006
-- Description:	
-- =============================================
CREATE  PROCEDURE [dbo].[spUpdateIssue]
(
	 @VolumeIssueId		int
	,@IssueName		varchar(50)
	,@Active		bit
	,@UpdateUserId		int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int

	UPDATE [dbo].[Issues] 
	SET 
		 [IssueName] 		= @IssueName
		,[Active]		= @Active
		,[UpdateUserId]		= @UpdateUserId
		,[UpdateDateTime]	= GETDATE()
	FROM
		VolumeIssues vi
	WHERE 
		vi.IssueId 	= Issues.IssueId
		AND
		VolumeIssueId 	= @VolumeIssueId

	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
		RETURN 0
	ELSE
	 BEGIN
		RAISERROR('An error occured while updating issue details.', 16, 1)
		RETURN -1
	 END
END
GO
PRINT N'Creating [dbo].[Users]'
GO
CREATE TABLE [dbo].[Users]
(
[UserId] [int] NOT NULL IDENTITY(1, 1),
[Email] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[PasswordHash] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[PasswordSalt] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[FirstName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[LastName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[MiddleInitial] [varchar] (5) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[SecretQuestion1Id] [int] NOT NULL,
[SecretQuestion2Id] [int] NOT NULL,
[SecretAnswer1Hash] [varchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[SecretAnswer2Hash] [varchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[AccountTypeId] [int] NOT NULL,
[AccountStatus] [int] NOT NULL,
[Active] [bit] NOT NULL,
[CreationUserId] [int] NOT NULL,
[CreationDateTime] [datetime] NOT NULL,
[UpdateUserId] [int] NOT NULL,
[UpdateDateTime] [datetime] NOT NULL
) ON [PRIMARY]
GO
PRINT N'Creating primary key [PK__Users__108B795B] on [dbo].[Users]'
GO
ALTER TABLE [dbo].[Users] ADD CONSTRAINT [PK__Users__108B795B] PRIMARY KEY CLUSTERED  ([UserId]) ON [PRIMARY]
GO
PRINT N'Creating [dbo].[spExistsUser]'
GO
-- =============================================
-- Author:		Monish Nagisetty
-- ALTER  date: 04/17/2007
-- Description:	Checks if a user exists with 
--				with the gaven email address.
-- =============================================
CREATE PROCEDURE [dbo].[spExistsUser] 
(
	@Email	varchar(50)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int,
		@RC		int,
		@EXISTS		int

	SELECT 
			@RC = COUNT(*)
	FROM
			dbo.Users
	WHERE
			Email = @Email

	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
		GOTO FAIL

	-- EMAIL DOES NOT EXIST
	IF (@RC = 0)	
	 BEGIN
		SET @EXISTS = 0			
	 END
	ELSE -- EMAIL ALREADY EXISTS
	 BEGIN
		SET @EXISTS = 1
	 END

	SELECT @EXISTS AS [Exists]

	FAIL:
	 BEGIN
		--AN ERROR OCCURED
		RAISERROR('Failed to determine if user exists.', 10, 1)
	 END

END
GO
PRINT N'Creating [dbo].[spGetDocument]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/18/2006
-- Description:	Gets all the active articles
--		and document links within an 
--		issue.
-- =============================================
CREATE    PROCEDURE [dbo].[spGetDocument] 
(
	@DocId		int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR			int
	
	--Returns a list of all documents for the current
	--article
	SELECT
		 [FileName]
		,[Data]
	FROM
		dbo.Documents
	WHERE
		DocId = @DocId
		AND
		Active = 1


	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
		GOTO FAIL
	ELSE
		RETURN 0


FAIL:
 BEGIN
	RAISERROR('An error occured while retrieving the document.', 16, 1)	
	RETURN -1
 END


END
GO
PRINT N'Creating [dbo].[spAdminInsertSubscription]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/21/2006

--  Author: Parag Jagdale
-- ALTER date: 01/03/2007
-- Description:	Inserts a subscription into the database
-- =============================================
CREATE  PROCEDURE [dbo].[spAdminInsertSubscription]
(
	 @UserId			int
	,@VolumeIssueId		int
	,@ArticleId			int 
	,@EffectiveDate		datetime
	,@ExpirationDate	datetime
	,@Active			bit
	,@CreationUserId	int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int

	INSERT INTO [dbo].[Subscription]
	(
		[UserId], 
		[VolumeIssueId],
		[ArticleId],
		[EffectiveDate], 
		[ExpirationDate], 
		[Active], 
		[CreationUserId], 
		[CreationDateTime], 
		[UpdateUserId], 
		[UpdateDateTime]


	)
	VALUES
	(
		 @UserId
		,@VolumeIssueId
		,@ArticleId			
		,@EffectiveDate
		,@ExpirationDate
		,@Active
		,@CreationUserId
		,GETDATE()
		,@CreationUserId
		,GETDATE()
	)

	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
		GOTO FAIL

	RETURN 0

FAIL:
 BEGIN
	RAISERROR('An error occured while adding a subscription.', 16, 1)	
	RETURN -1
 END


END
GO
PRINT N'Creating [dbo].[AccountStatus]'
GO
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
GO
PRINT N'Creating primary key [PK__AccountStatus__703EA55A] on [dbo].[AccountStatus]'
GO
ALTER TABLE [dbo].[AccountStatus] ADD CONSTRAINT [PK__AccountStatus__703EA55A] PRIMARY KEY CLUSTERED  ([AccountStatusID]) ON [PRIMARY]
GO
PRINT N'Creating [dbo].[IssueDocuments]'
GO
CREATE TABLE [dbo].[IssueDocuments]
(
[VolumeIssueId] [int] NOT NULL,
[IssueDocTypeId] [int] NOT NULL,
[DocId] [int] NOT NULL
) ON [PRIMARY]
GO
PRINT N'Creating primary key [PK_IssueDocuments] on [dbo].[IssueDocuments]'
GO
ALTER TABLE [dbo].[IssueDocuments] ADD CONSTRAINT [PK_IssueDocuments] PRIMARY KEY CLUSTERED  ([VolumeIssueId], [IssueDocTypeId], [DocId]) ON [PRIMARY]
GO
PRINT N'Creating [dbo].[spUpdateIssueDetails]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/09/2006
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateIssueDetails]
(
	 @IssueId	int
	,@IssueName	varchar(50)
	,@Active	bit
	,@UpdateUserId	int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int

	BEGIN TRAN

	UPDATE [dbo].[Issues]
		SET 
		 [IssueName] 		= @IssueName
		,[Active]		= @Active
		,[UpdateUserId]		= @UpdateUserId
		,[UpdateDateTime]	= GETDATE()
	WHERE 
		IssueId = @IssueId

	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
	 BEGIN
		COMMIT TRAN
		RETURN 0
	 END
	ELSE
	 BEGIN
		ROLLBACK TRAN
		RAISERROR('An error occured while updating issue details.', 16, 1)
		RETURN -1
	 END
END
GO
PRINT N'Creating [dbo].[spAdminGetIssues]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/09/2006
-- Description:	Gets a list of all issues for
--		a volume
-- =============================================
CREATE    PROCEDURE [dbo].[spAdminGetIssues]
(
	@VolumeId	int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int

	SELECT 
		 vi.[VolumeIssueId]
		  ,[IssueName]
	FROM 
		[dbo].[Issues] i
	INNER JOIN VolumeIssues vi ON
		i.IssueId = vi.IssueId
	WHERE
		vi.VolumeId = @VolumeId

	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
	 BEGIN
		RETURN 0
	 END
	ELSE
	 BEGIN
		RAISERROR('An error occured while getting all issues.', 16, 1)
		RETURN -1
	 END


END
GO
PRINT N'Creating [dbo].[spGetAllIssuesDetailed]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/09/2006
-- Description:	
-- =============================================
CREATE     PROCEDURE [dbo].[spGetAllIssuesDetailed]
(
	@VolumeId	int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int

	SELECT 
		vi.VolumeIssueId,
		i.[IssueId], 
		[IssueName], 
		[Active], 
		(
			SELECT
				COUNT(*)
			FROM
				IssueDocuments idoc
			WHERE
				idoc.VolumeIssueId = vi.VolumeIssueId
		) AS Documents,
		(
			SELECT
				COUNT(*)
			FROM
				IssueArticles ia
			WHERE
				ia.VolumeIssueId = vi.VolumeIssueId
		) AS Articles			
	FROM 
		[dbo].[Issues] i
	INNER JOIN VolumeIssues vi ON
		i.IssueId = vi.IssueId
	WHERE
		vi.VolumeId = @VolumeId

	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
	 BEGIN
		RETURN 0
	 END
	ELSE
	 BEGIN
		RAISERROR('An error occured while getting all issues.', 16, 1)
		RETURN -1
	 END


END
GO
PRINT N'Creating [dbo].[spUpdateUserPasswordHash]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/20/2006
-- Description:	
-- =============================================
CREATE  PROCEDURE [dbo].[spUpdateUserPasswordHash]
(
	@Email		varchar(50),
	@PasswordHash	varchar(100),
	@AccountStatus	int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int,
		@RC	int

	UPDATE [dbo].[Users]
	SET 
		[PasswordHash] = @PasswordHash,
		[AccountStatus] = @AccountStatus
	WHERE 
		Email 	= @Email

	SELECT @ERROR = @@ERROR, @RC = @@ROWCOUNT

	IF (@ERROR = 0 AND @RC = 1)
	 BEGIN
		RETURN 0
	 END
	ELSE
	 BEGIN
		RETURN -1
	 END
END
GO
PRINT N'Creating [dbo].[spGetUserByEmail]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 04/17/2007
-- Description:	This procedure returns the password
--				and salt for a given user.
-- =============================================
CREATE   PROCEDURE [dbo].[spGetUserByEmail] 
(
	@Email	varchar(50)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int,
		@RC		int

	SELECT 
		[UserId], 
		[Email], 
		[PasswordHash], 
		[PasswordSalt], 
		[FirstName], 
		[LastName], 
		[MiddleInitial], 
		[SecretQuestion1Id], 
		[SecretQuestion2Id], 
		[SecretAnswer1Hash], 
		[SecretAnswer2Hash], 
		[AccountTypeId], 
		[AccountStatus], 
		[Active], 
		[CreationUserId], 
		[CreationDateTime], 
		[UpdateUserId], 
		[UpdateDateTime] 
	FROM 
		[dbo].[Users]
	WHERE 
		Email = @Email

	SELECT @ERROR = @@ERROR, @RC = @@ROWCOUNT

	IF (@ERROR <> 0 OR @RC <> 1)
		GOTO FAIL
	ELSE
		RETURN 0	

	FAIL:
	 BEGIN
		--AN ERROR OCCURED
		RAISERROR('An error occured or returned more than one record when retrieving a User record.', 10, 1)
		RETURN -1
	 END
END
GO
PRINT N'Creating [dbo].[spInsertUser]'
GO
-- =============================================
-- Author:		Monish Nagisetty
-- ALTER  date: 03/03/2006
-- Description:	Inserts a record into the Users table.
-- =============================================
CREATE PROCEDURE [dbo].[spInsertUser] 
(
	@Email			varchar(50),
	@PasswordHash		varchar(100),
	@PasswordSalt		varchar(50),
	@FirstName		varchar(50),
	@LastName		varchar(50),
	@MiddleInitial		varchar(5),
	@SecretQuestion1Id	int,
	@SecretQuestion2Id	int,
	@SecretAnswer1Hash	varchar(200),
	@SecretAnswer2Hash	varchar(200),
	@AccountTypeId		int,
	@AccountStatus		int,
	@Active			bit,
	@CreationUserId		int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int,
			@RC			int,
			@IDENTITY	int

	INSERT INTO [dbo].[Users]
	(
		 [Email]
		,[PasswordHash]
		,[PasswordSalt]
		,[FirstName]
		,[LastName]
		,[MiddleInitial]
		,[SecretQuestion1Id]
		,[SecretQuestion2Id]
		,[SecretAnswer1Hash]
		,[SecretAnswer2Hash]
		,[AccountTypeId]
		,[AccountStatus]
		,[Active]
		,[CreationUserId]
		,[CreationDateTime]
		,[UpdateUserId]
		,[UpdateDateTime]
	)
	VALUES
	(
		@Email,
		@PasswordHash,
		@PasswordSalt,
		@FirstName,
		@LastName,
		@MiddleInitial,
		@SecretQuestion1Id,
		@SecretQuestion2Id,
		@SecretAnswer1Hash, 
		@SecretAnswer2Hash,
		@AccountTypeId,
		@AccountStatus,
		@Active,
		@CreationUserId,
		GETDATE(),
		@CreationUserId,
		GETDATE()
	)

	SELECT @ERROR = @@ERROR, @IDENTITY = @@IDENTITY

	IF (@ERROR = 0)
	 BEGIN
		RETURN @IDENTITY
	 END
	ELSE
	 BEGIN
		RAISERROR(@ERROR, 16, 1)
		RETURN -1
	 END

END
GO
PRINT N'Creating [dbo].[spUpdateUser]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/20/2006
-- Description:	
-- =============================================
CREATE  PROCEDURE [dbo].[spUpdateUser]
(
	 @UserId		int
	,@FirstName		varchar(50)
	,@LastName		varchar(50)
	,@MiddleInitial		varchar(5)
	,@AccountTypeId		int
	,@AccountStatus		int
	,@Active		bit
	,@UpdateUserId		int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int,
		@RC	int

	BEGIN TRAN
	
	UPDATE [dbo].[Users]
	SET 
		 [FirstName]		= @FirstName
		,[LastName]		= @LastName
		,[MiddleInitial]	= @MiddleInitial
		,[AccountTypeId]	= @AccountTypeId
		,[AccountStatus]	= @AccountStatus
		,[Active]		= @Active
		,[UpdateUserId]		= @UpdateUserId
		,[UpdateDateTime] 	= GETDATE()
	WHERE 
		UserId 	= @UserId

	SELECT @ERROR = @@ERROR, @RC = @@ROWCOUNT

	IF (@ERROR = 0 AND @RC = 1)
	 BEGIN
		COMMIT TRAN
		RETURN 0
	 END
	ELSE
	 BEGIN
		ROLLBACK TRAN
		RETURN -1
	 END
END
GO
PRINT N'Creating [dbo].[ArticleDocTypes]'
GO
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
GO
PRINT N'Creating primary key [PK_DocumentTypes] on [dbo].[ArticleDocTypes]'
GO
ALTER TABLE [dbo].[ArticleDocTypes] ADD CONSTRAINT [PK_DocumentTypes] PRIMARY KEY CLUSTERED  ([ArtDocTypeId]) ON [PRIMARY]
GO
PRINT N'Creating [dbo].[IssueDocTypes]'
GO
CREATE TABLE [dbo].[IssueDocTypes]
(
[IssueDocTypeId] [int] NOT NULL IDENTITY(1, 1),
[IssueDocTypeDescription] [varchar] (80) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[PubliclyAvailable] [bit] NULL,
[Active] [bit] NOT NULL,
[CreationUserId] [int] NOT NULL,
[CreationDateTime] [datetime] NOT NULL,
[UpdateUserId] [int] NOT NULL,
[UpdateDateTime] [datetime] NOT NULL
) ON [PRIMARY]
GO
PRINT N'Creating primary key [PK_IssueDocTypes] on [dbo].[IssueDocTypes]'
GO
ALTER TABLE [dbo].[IssueDocTypes] ADD CONSTRAINT [PK_IssueDocTypes] PRIMARY KEY CLUSTERED  ([IssueDocTypeId]) ON [PRIMARY]
GO
PRINT N'Creating [dbo].[UserRoles]'
GO
CREATE TABLE [dbo].[UserRoles]
(
[RoleId] [int] NOT NULL,
[UserId] [int] NOT NULL
) ON [PRIMARY]
GO
PRINT N'Creating primary key [PK_UserRoles] on [dbo].[UserRoles]'
GO
ALTER TABLE [dbo].[UserRoles] ADD CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED  ([RoleId], [UserId]) ON [PRIMARY]
GO
PRINT N'Creating [dbo].[spGetUserRolesByUserId]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 04/17/2007
-- Description:	This procedure returns the password
--				and salt for a given user.
-- =============================================
CREATE  PROCEDURE [dbo].[spGetUserRolesByUserId] 
(
	@UserId		int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int,
		@RC		int

	SELECT
		RoleId
	FROM
		UserRoles ur
	INNER JOIN Users u ON
		u.UserId = ur.UserId
	WHERE
		u.UserId = @UserId

	SELECT @ERROR = @@ERROR, @RC = @@ROWCOUNT

	IF (@ERROR <> 0 OR @RC <> 1)
		GOTO FAIL
	ELSE
		RETURN 0	

	FAIL:
	 BEGIN
		--AN ERROR OCCURED
		RAISERROR('An error occured while retrieving a UserRoles record.', 10, 1)
		RETURN -1
	 END
END
GO
PRINT N'Creating [dbo].[spAdminExistsUserIPAddress]'
GO
/****** Object:  Stored Procedure dbo.spAdminExistsUserIPAddress    Script Date: 4/10/2007 11:18:08 PM ******/

-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 04/10/2007
-- Description:	Checks if an IP address already
--		exists.
-- =============================================
CREATE    PROCEDURE [dbo].[spAdminExistsUserIPAddress] 
(
	@IPOctet1Begin	int,
	@IPOctet2Begin	int,
	@IPOctet3Begin	int,
	@IPOctet3End	int,
	@IPOctet4Begin	int,
	@IPOctet4End	int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int,
		@RC	int,
		@Exists	int
				
	SET @Exists = 0

	SELECT
		@RC = COUNT(*) 
	FROM
		UserIPAddresses
	WHERE
		IPOctet1Begin = @IPOctet1Begin
		AND
		IPOctet2Begin = @IPOctet2Begin
		AND
		(
			@IPOctet3Begin	BETWEEN IPOctet3Begin AND IPOctet3End	
			OR
			@IPOctet3End	BETWEEN IPOctet3Begin AND IPOctet3End				
			OR
			(
				@IPOctet3Begin < IPOctet3Begin
				AND
				@IPOctet3End > IPOctet3End
			)
		)
		AND
		(
			@IPOctet4Begin	BETWEEN IPOctet4Begin AND IPOctet4End	
			OR
			@IPOctet4End	BETWEEN IPOctet4Begin AND IPOctet4End				
			OR
			(
				@IPOctet4Begin < IPOctet4Begin
				AND
				@IPOctet4End > IPOctet4End
			)
		)
		
	SELECT @ERROR = @@ERROR
	
	IF (@ERROR = 0)
	BEGIN
		IF (@RC > 0)
			SET @Exists = 1		
	
		SELECT @EXISTS AS [Exists]

	END
	ELSE
		RAISERROR('Failed to determine if IP address exists.', 10, 1)

END
GO
PRINT N'Creating [dbo].[Volumes]'
GO
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
GO
PRINT N'Creating primary key [PK__Volumes__1367E606] on [dbo].[Volumes]'
GO
ALTER TABLE [dbo].[Volumes] ADD CONSTRAINT [PK__Volumes__1367E606] PRIMARY KEY CLUSTERED  ([VolumeId]) ON [PRIMARY]
GO
PRINT N'Creating [dbo].[spGetVolume]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/09/2006
-- Description:	Gets the volume and issue name.
-- =============================================
CREATE PROCEDURE [dbo].[spGetVolume]
(
	@VolumeId	int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int

	--Return the Volume Name, Year and issue name
	SELECT
		 VolumeName
		,VolumeYear
		,Active
		,CreationUserId
		,CreationDateTime
		,UpdateUserId
		,UpdateDateTime
	FROM
		Volumes
	WHERE
		VolumeId = @VolumeId

	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
	 BEGIN
		RETURN 0
	 END
	ELSE
	 BEGIN
		RAISERROR('An error occurred while retrieving a volume.', 16, 1)
		RETURN -1
	 END

END
GO
PRINT N'Creating [dbo].[spDeleteArticleDoc]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/19/2006
-- Description:	Deletes records from the documents
--		and issuedocuments tables
-- =============================================
CREATE  PROCEDURE [dbo].[spDeleteArticleDoc] 
(
	 @ArticleId		int
	,@ArtDocTypeId		int
	,@DocId			int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int
		
	DELETE FROM [ArticleDocuments]
	WHERE 
		ArticleId 	= @ArticleId
		AND
		DocId 		= @DocId
		AND
		ArtDocTypeId 	= @ArtDocTypeId

	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
		GOTO FAIL
	
	DELETE FROM [Documents]
	WHERE 
		DocId  = @DocId

	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
		GOTO FAIL
	ELSE
		RETURN 0

FAIL:
 BEGIN
	RAISERROR('An error occured deleting an article document.', 16, 1)	
	RETURN -1
 END


END
GO
PRINT N'Creating [dbo].[spInsertUserRole]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/17/2006
-- Description:	Inserts a user's address into 
--				the user's table
-- =============================================
CREATE PROCEDURE [dbo].[spInsertUserRole] 
(
	 @UserId	int
	,@RoleId	int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int

	INSERT INTO [dbo].[UserRoles]
	(
		[RoleId], 
		[UserId]
	)
	VALUES
	(
		 @RoleId
		,@UserId
	)

	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
		GOTO FAIL
	ELSE
		GOTO SUCCESS

	SUCCESS:
		RETURN 0

	FAIL:
	 BEGIN
		RAISERROR('Failed to add role for new user.', 16, 1)
		RETURN -1
	 END
END
GO
PRINT N'Creating [dbo].[spGetIssueDocTypes]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/05/2006
-- Description:	Gets countries from the Countries
--				table.
-- =============================================
CREATE PROCEDURE [dbo].[spGetIssueDocTypes]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int,
		@RC	int

	SELECT 
		[IssueDocTypeId], 
		[IssueDocTypeDescription]
	FROM 
		[dbo].[IssueDocTypes]
	WHERE
		Active = 1	
	ORDER BY
		[IssueDocTypeDescription]

	SELECT @ERROR = @@ERROR, @RC = @@ROWCOUNT

	IF (@ERROR = 0)
	 BEGIN
		RETURN 0
	 END
	ELSE
	 BEGIN
		RETURN -1
	 END

END
GO
PRINT N'Creating [dbo].[spAdminUpdateSubscription]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/21/2006

--  Author: Parag Jagdale
-- ALTER date: 01/03/2007
-- Description:	updates subscription using SubscriptionId 
--		to select the correct row
-- =============================================
CREATE PROCEDURE [dbo].[spAdminUpdateSubscription]
(
	 @SubscriptionId		int
	,@VolumeIssueId		int
	,@ArticleId			int
	,@UserId			int
	,@EffectiveDate		datetime
	,@ExpirationDate	datetime
	,@Active			bit
	,@UpdateUserId		int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int

	/*
	TODO:  Modify this update statement to include the new parameters above
		VolumeIssueId
		ArticleId
	*/
	UPDATE [dbo].[Subscription]
	SET 
 		 [VolumeIssueId]	= @VolumeIssueId
		,[ArticleId]		= @ArticleId
		,[UserId]			= @UserId
		,[EffectiveDate]	= @EffectiveDate
		,[ExpirationDate]	= @ExpirationDate
		,[Active]			= @Active
		,[UpdateUserId]		= @UpdateUserId
		,[UpdateDateTime]	= GETDATE()
	WHERE
		SubscriptionId = @SubscriptionId

	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
		GOTO FAIL

	RETURN 0

FAIL:
 BEGIN
	RAISERROR('An error occured while updating a subscription.', 16, 1)	
	RETURN -1
 END


END
GO
PRINT N'Creating [dbo].[spUpdateVolume]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/19/2006
-- Description:	
-- =============================================
CREATE  PROCEDURE [dbo].[spUpdateVolume]
(
	 @VolumeId	int
	,@VolumeName	varchar(30)
	,@VolumeYear	varchar(10)
	,@Active	bit
	,@UpdateUserId	int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int

	BEGIN TRAN

	UPDATE [dbo].[Volumes]
	SET 
		 [VolumeName]		= @VolumeName
		,[VolumeYear]		= @VolumeYear
		,[Active]		= @Active
		,[UpdateUserId]		= @UpdateUserId
		,[UpdateDateTime]	= GETDATE()
	WHERE 
		VolumeId = @VolumeId

	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
	 BEGIN
		COMMIT TRAN
		RETURN 0
	 END
	ELSE
	 BEGIN
		ROLLBACK TRAN
		RAISERROR('An error occured while updating volume details.', 16, 1)
		RETURN -1
	 END
END
GO
PRINT N'Creating [dbo].[spGetAllVolumes]'
GO
-- =============================================
-- Author:		Monish Nagisetty
-- ALTER  date: 03/09/2006
-- Description:	Gets a list of all volumes
-- =============================================
CREATE  PROCEDURE [dbo].[spGetAllVolumes]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int

	SELECT 
		 [VolumeId]
		,[VolumeName]+' ('+[VolumeYear]+')' as 'VolumeName'
	FROM 
		[dbo].[Volumes]

	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
	 BEGIN
		RETURN 0
	 END
	ELSE
	 BEGIN
		RAISERROR('An error occured while getting all volumes.', 16, 1)
		RETURN -1
	 END


END
GO
PRINT N'Creating [dbo].[spAdminGetUserInfo]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/17/2006
-- Description:	This procedure returns the password
--				and salt for a given user.
-- =============================================
CREATE PROCEDURE [dbo].[spAdminGetUserInfo] 
(
	@UserId		int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int

	SELECT 
		u.[UserId], 
		u.[Email], 
		u.[FirstName], 
		u.[LastName], 
		u.[MiddleInitial], 
		--u.[AccountTypeId], 
		acTypes.[Description] as AcctTypeDescription,
		--u.[AccountStatus], 
		acct.[AccountStatusDescription] as AcctStatusDescription,
		u.[Active]
	FROM 
		[dbo].[Users] u
	INNER JOIN AccountStatus acct ON
		acct.AccountStatusID = u.AccountStatus
	INNER JOIN AccountTypes acTypes ON
		acTypes.AccountTypeId = u.AccountTypeId
	WHERE
		UserId = @UserId

	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
		GOTO FAIL
	ELSE
		RETURN 0	

	FAIL:
	 BEGIN
		--AN ERROR OCCURED
		RAISERROR('Failed to get user information.', 16, 1)
		RETURN -1
	 END
END
GO
PRINT N'Creating [dbo].[IsUserArticleSubscriber]'
GO
/*=============================================
Author:  Monish Nagisetty
Created: 03/22/2006
Description:	This procedure returns user 
information.
===============================================
Revision History
=============================================================================
Change   Initial Date        Description
    ID        AA mm/dd/yyyy  Describe reason for change.                            
=============================================================================*/
CREATE FUNCTION dbo.IsUserArticleSubscriber
(
	 @UserId			int
	,@VolumeIssueId		int
	,@ArticleId			int
	,@CurrentDate		datetime
)  
RETURNS int
AS
BEGIN 
	DECLARE @IsUserSubscriber int
	SET @IsUserSubscriber = 0

	--Returns 0 or more depending on whether
	--the user has a valid subscription
	SELECT
		@IsUserSubscriber = COUNT(*)
	FROM
		Subscription s
	INNER JOIN Users u on 
		s.UserId = u.UserId
	WHERE
		u.UserId		= @UserId
		AND
		u.AccountStatus = 1 -- Account Active
		AND	
		u.Active		= 1 --User should be active
		AND 
		s.Active		= 1	--Subscription should be active
		AND
		s.VolumeIssueId = @VolumeIssueId -- User has access to issue
		AND
		s.ArticleId		= @ArticleId -- User has access to article
		AND
		DATEDIFF(dd, s.EffectiveDate, @CurrentDate) >= 0
		AND
		DATEDIFF(DD, s.ExpirationDate, @CurrentDate) <= 0		

	RETURN (@IsUserSubscriber)
END
GO
PRINT N'Creating [dbo].[spGetActiveVolumes]'
GO
-- =============================================
-- Author:		Monish Nagisetty
-- ALTER  date: 03/09/2006
-- Description:	Gets a list of active volumes
-- =============================================
CREATE   PROCEDURE [dbo].[spGetActiveVolumes]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int

	SELECT 
		 [VolumeId]
		,[VolumeName]
		,[VolumeYear]
	FROM 
		[dbo].[Volumes]
	WHERE
		Active = 1

	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
	 BEGIN
		RETURN 0
	 END
	ELSE
	 BEGIN
		RAISERROR('An error occured while getting active volumes.', 16, 1)
		RETURN -1
	 END


END
GO
PRINT N'Creating [dbo].[UserAddresses]'
GO
CREATE TABLE [dbo].[UserAddresses]
(
[UserId] [int] NOT NULL,
[AddressId] [int] NOT NULL,
[AddressTypeId] [int] NOT NULL
) ON [PRIMARY]
GO
PRINT N'Creating primary key [PK__UserAddresses__0539C240] on [dbo].[UserAddresses]'
GO
ALTER TABLE [dbo].[UserAddresses] ADD CONSTRAINT [PK__UserAddresses__0539C240] PRIMARY KEY CLUSTERED  ([UserId], [AddressId], [AddressTypeId]) ON [PRIMARY]
GO
PRINT N'Creating [dbo].[spGetUserByUserId]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 04/17/2007
-- Description:	This procedure returns the password
--				and salt for a given user.
-- =============================================
CREATE   PROCEDURE [dbo].[spGetUserByUserId] 
(
	@UserId		int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int,
		@RC		int

	SELECT 
		[UserId], 
		[Email], 
		[PasswordHash], 
		[PasswordSalt], 
		[FirstName], 
		[LastName], 
		[MiddleInitial], 
		[SecretQuestion1Id], 
		[SecretQuestion2Id], 
		[SecretAnswer1Hash], 
		[SecretAnswer2Hash], 
		[AccountTypeId], 
		[AccountStatus], 
		[Active], 
		[CreationUserId], 
		[CreationDateTime], 
		[UpdateUserId], 
		[UpdateDateTime] 
	FROM 
		[dbo].[Users]
	WHERE 
		UserId = @UserId

	SELECT @ERROR = @@ERROR, @RC = @@ROWCOUNT

	IF (@ERROR <> 0 OR @RC <> 1)
		GOTO FAIL
	ELSE
		RETURN 0	

	FAIL:
	 BEGIN
		--AN ERROR OCCURED
		RAISERROR('An error occured or returned more than one record when retrieving a User record.', 10, 1)
		RETURN -1
	 END
END
GO
PRINT N'Creating [dbo].[Addresses]'
GO
CREATE TABLE [dbo].[Addresses]
(
[AddressId] [int] NOT NULL IDENTITY(1, 1),
[Line1] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Line2] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[City] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[StateProvince] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CountryId] [int] NOT NULL,
[Phone] [varchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Fax] [varchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Active] [bit] NOT NULL,
[CreationUserId] [int] NOT NULL,
[CreationDateTime] [datetime] NOT NULL,
[UpdateUserId] [int] NOT NULL,
[UpdateDateTime] [datetime] NOT NULL
) ON [PRIMARY]
GO
PRINT N'Creating primary key [PK__Addresses__00551192] on [dbo].[Addresses]'
GO
ALTER TABLE [dbo].[Addresses] ADD CONSTRAINT [PK__Addresses__00551192] PRIMARY KEY CLUSTERED  ([AddressId]) ON [PRIMARY]
GO
PRINT N'Creating [dbo].[Countries]'
GO
CREATE TABLE [dbo].[Countries]
(
[CountryId] [int] NOT NULL,
[CountryCode] [varchar] (5) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CountryName] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
PRINT N'Creating primary key [PK_Countries] on [dbo].[Countries]'
GO
ALTER TABLE [dbo].[Countries] ADD CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED  ([CountryId]) ON [PRIMARY]
GO
PRINT N'Creating [dbo].[spGetArticleDocTypes]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/05/2006
-- Description:	Gets countries from the Countries
--				table.
-- =============================================
CREATE  PROCEDURE [dbo].[spGetArticleDocTypes]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int,
		@RC	int

	SELECT 
		[ArtDocTypeId], 
		[ArtDocTypeDescription]
	FROM 
		[dbo].[ArticleDocTypes]
	WHERE
		Active = 1
	ORDER BY
		[ArtDocTypeDescription]
	
	SELECT @ERROR = @@ERROR, @RC = @@ROWCOUNT

	IF (@ERROR = 0)
	 BEGIN
		RETURN 0
	 END
	ELSE
	 BEGIN
		RETURN -1
	 END

END
GO
PRINT N'Creating [dbo].[spDeleteIssueDoc]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/19/2006
-- Description:	Deletes records from the documents
--		and issuedocuments tables
-- =============================================
CREATE    PROCEDURE [dbo].[spDeleteIssueDoc] 
(
	 @VolumeIssueId		int
	,@IssueDocTypeId	int
	,@DocId			int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int
		
	DELETE FROM [dbo].[IssueDocuments]
	WHERE 
		VolumeIssueId 	= @VolumeIssueId
		AND
		DocId 		= @DocId
		AND
		IssueDocTypeId 	= @IssueDocTypeId

	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
		GOTO FAIL
	
	DELETE FROM [dbo].[Documents]
	WHERE 
		DocId  = @DocId

	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
		GOTO FAIL
	ELSE
		RETURN 0

FAIL:
 BEGIN
	RAISERROR('An error occured deleting an issue document.', 16, 1)	
	RETURN -1
 END


END
GO
PRINT N'Creating [dbo].[PasswordRecoveryQuestions]'
GO
CREATE TABLE [dbo].[PasswordRecoveryQuestions]
(
[QuestionId] [int] NOT NULL IDENTITY(1, 1),
[Description] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Active] [bit] NOT NULL,
[CreationUserId] [int] NOT NULL,
[CreationDateTime] [datetime] NOT NULL,
[UpdateUserId] [int] NOT NULL,
[UpdateDateTime] [datetime] NOT NULL
) ON [PRIMARY]
GO
PRINT N'Creating primary key [PK__PasswordRecovery__7D78A4E7] on [dbo].[PasswordRecoveryQuestions]'
GO
ALTER TABLE [dbo].[PasswordRecoveryQuestions] ADD CONSTRAINT [PK__PasswordRecovery__7D78A4E7] PRIMARY KEY CLUSTERED  ([QuestionId]) ON [PRIMARY]
GO
PRINT N'Creating [dbo].[spAdminUpdateUser]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/20/2006
-- Description:	
-- =============================================
CREATE  PROCEDURE [dbo].[spAdminUpdateUser]
(
	 @UserId		int
	,@FirstName		varchar(50)
	,@LastName		varchar(50)
	,@MiddleInitial		varchar(5)
	,@AccountTypeId		int
	,@AccountStatus		int
	,@Active		bit
	,@UpdateUserId		int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int,
		@RC	int

	BEGIN TRAN
	
	UPDATE [dbo].[Users]
	SET 
		 [FirstName]		= @FirstName
		,[LastName]		= @LastName
		,[MiddleInitial]	= @MiddleInitial
		,[AccountTypeId]	= @AccountTypeId
		,[AccountStatus]	= @AccountStatus
		,[Active]		= @Active
		,[UpdateUserId]		= @UpdateUserId
		,[UpdateDateTime] 	= GETDATE()
	WHERE 
		UserId 	= @UserId

	SELECT @ERROR = @@ERROR, @RC = @@ROWCOUNT

	IF (@ERROR = 0 AND @RC = 1)
	 BEGIN
		COMMIT TRAN
		RETURN 0
	 END
	ELSE
	 BEGIN
		ROLLBACK TRAN
		RETURN -1
	 END
END
GO
PRINT N'Creating [dbo].[spDeleteVolume]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 01/03/2009
-- Description:	Deletes records from the documents
--		and issuedocuments tables
-- =============================================
CREATE PROCEDURE [dbo].[spDeleteVolume] 
(
	@VolumeId		int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int,
			@ERRMSG		varchar(50),
			@RC			int	

	SELECT
		@RC = COUNT(*)
	FROM
		VolumeIssues
	WHERE
		VolumeId = @VolumeId	

	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
	BEGIN
		SET @ERRMSG = 'An error occurred while determining the number of issues in the provided volume.'
		GOTO FAIL
	END

	IF (@RC > 0)
	BEGIN
		SET @ERRMSG = 'The selected volume cannot be deleted because it contains multiple active issues.'
		GOTO FAIL
	END
	ELSE
	BEGIN
		DELETE 
		FROM
			Volumes 
		WHERE
			VolumeId = @VolumeId
	
		SELECT @ERROR = @@ERROR
	END

	IF (@ERROR <> 0)
	BEGIN
		SET @ERRMSG = 'An error occurred when attempting to delete the selected volume.'
		GOTO FAIL
	END
	ELSE
		RETURN 0

FAIL:
 BEGIN
	RAISERROR(@ERRMSG, 16, 1)	
	RETURN -1
 END


END
GO
PRINT N'Creating [dbo].[spGetCountries]'
GO
-- =============================================
-- Author:		Monish Nagisetty
-- ALTER  date: 03/05/2006
-- Description:	Gets countries from the Countries
--				table.
-- =============================================
CREATE  PROCEDURE [dbo].[spGetCountries]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int,
			@RC		int

	SELECT 
		[CountryId]
		,[CountryName]
	FROM 
		[dbo].[Countries]
	ORDER BY
		CountryName
	
	SELECT @ERROR = @@ERROR, @RC = @@ROWCOUNT

	IF (@ERROR = 0)
	 BEGIN
		RETURN 0
	 END
	ELSE
	 BEGIN
		RETURN -1
	 END

END
GO
PRINT N'Creating [dbo].[spGetAccountTypes]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/05/2006
-- Description:	Gets countries from the Countries
--				table.
-- =============================================
CREATE PROCEDURE [dbo].[spGetAccountTypes]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int

	SELECT 
		[AccountTypeId], 
		[Description]
	FROM
		[dbo].[AccountTypes]
	WHERE
		Active = 1	

	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
	 BEGIN
		RETURN 0
	 END
	ELSE
	 BEGIN
		RETURN -1
	 END

END
GO
PRINT N'Creating [dbo].[spGetUser]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/17/2006
-- Description:	This procedure returns the password
--				and salt for a given user.
-- =============================================
CREATE PROCEDURE [dbo].[spGetUser] 
(
	@Email	varchar(50)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int,
		@RC		int

	SELECT 
		[UserId], 
		[Email], 
		[PasswordHash], 
		[PasswordSalt], 
		[FirstName], 
		[LastName], 
		[MiddleInitial], 
		[SecretQuestion1Id], 
		[SecretQuestion2Id], 
		[SecretAnswer1Hash], 
		[SecretAnswer2Hash], 
		[AccountTypeId], 
		[AccountStatus], 
		[Active]
	FROM 
		[dbo].[Users]
	WHERE 
		Email = @Email

	SELECT @ERROR = @@ERROR, @RC = @@ROWCOUNT

	IF (@ERROR <> 0 OR @RC <> 1)
		GOTO FAIL
	ELSE
		RETURN 0	

	FAIL:
	 BEGIN
		--AN ERROR OCCURED
		RAISERROR('Failed to get user information.', 16, 1)
		RETURN -1
	 END
END
GO
PRINT N'Creating [dbo].[spGetActiveIssues]'
GO
-- =============================================
-- Author:		Monish Nagisetty
-- ALTER  date: 03/09/2006
-- Description:	Gets a list of issues for the
--				a given volume.
-- =============================================
CREATE     PROCEDURE [dbo].[spGetActiveIssues]
(
	@VolumeId	int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int

	--Return the Volume Name
	SELECT
		VolumeName
		,VolumeYear
	FROM
		dbo.Volumes
	WHERE
		VolumeId = @VolumeId
		AND
		Active = 1

	--Return all active issues in the volume
	SELECT 
		 vi.[VolumeIssueId]
		,i.[IssueName]
	FROM 
		[dbo].[Issues] i
	INNER JOIN VolumeIssues vi ON
		vi.IssueId = i.IssueId
	WHERE
		i.Active = 1
		AND
		vi.VolumeId = @VolumeId

	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
	 BEGIN
		RETURN 0
	 END
	ELSE
	 BEGIN
		RAISERROR(@ERROR, 16, 1)
		RETURN -1
	 END


END
GO
PRINT N'Creating [dbo].[Roles]'
GO
CREATE TABLE [dbo].[Roles]
(
[RoleId] [int] NOT NULL IDENTITY(1, 1),
[RoleDescription] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Active] [bit] NOT NULL,
[CreationUserId] [int] NOT NULL,
[CreationDateTime] [datetime] NOT NULL,
[UpdateUserId] [int] NOT NULL,
[UpdateDateTime] [datetime] NOT NULL
) ON [PRIMARY]
GO
PRINT N'Creating primary key [PK_Roles] on [dbo].[Roles]'
GO
ALTER TABLE [dbo].[Roles] ADD CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED  ([RoleId]) ON [PRIMARY]
GO
PRINT N'Creating [dbo].[spInsertIssueDoc]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/19/2006
-- Description:	Inserts records into the documents
--		and issuedocuments tables for
--		creating an issue document.
-- =============================================
CREATE PROCEDURE [dbo].[spInsertIssueDoc] 
(
	 @VolumeIssueId		int
	,@IssueDocTypeId	int
	,@Data			image
	,@FileName		varchar(100)
	,@FileSizeKB		int
	,@Comments		varchar(500)
	,@Active		bit
	,@CreationUserId	int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int,
		@DocId		int

	INSERT INTO [dbo].[Documents]
	(
		[Data], 
		[FileName], 
		[FileSizeKB], 
		[Comments], 
		[Active], 
		[CreationUserId], 
		[CreationDateTime], 
		[UpdateUserId], 
		[UpdateDateTime]
	)
	VALUES
	(
		 @Data
		,@FileName
		,@FileSizeKB
		,@Comments
		,@Active
		,@CreationUserId
		,GETDATE()
		,@CreationUserId
		,GETDATE()
	)

	SELECT @ERROR = @@ERROR, @DocId = @@IDENTITY

	IF (@ERROR <> 0)
		GOTO FAIL

	--Add this article to the issue
	INSERT INTO [dbo].[IssueDocuments]
	(
		[VolumeIssueId], 
		[IssueDocTypeId], 
		[DocId]
	)
	VALUES
	(
		 @VolumeIssueId
		,@IssueDocTypeId
		,@DocId
	)
		
	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
		GOTO FAIL
	ELSE
		RETURN 0

FAIL:
 BEGIN
	RAISERROR('An error occured adding an issue document.', 16, 1)	
	RETURN -1
 END


END
GO
PRINT N'Creating [dbo].[spInsertUserIPAddress]'
GO
-- =============================================
-- Author:		Monish Nagisetty
-- ALTER  date: 06/04/2006
-- Description:	Inserts a record into the Users table.
-- =============================================
CREATE PROCEDURE [dbo].[spInsertUserIPAddress] 
(
	@UserId		int,
	@IPOctet1Begin	int,
	@IPOctet2Begin	int,
	@IPOctet3Begin	int,
	@IPOctet3End	int,
	@IPOctet4Begin	int,
	@IPOctet4End	int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int,
		@RC		int
		
	INSERT INTO [dbo].[UserIPAddresses]
	(
		[UserId], 
		[IPOctet1Begin], 
		[IPOctet2Begin], 
		[IPOctet3Begin], 
		[IPOctet3End],
		[IPOctet4Begin],
		[IPOctet4End]
	)
	VALUES
	( 
	   	@UserId,
		@IPOctet1Begin, 
		@IPOctet2Begin, 
		@IPOctet3Begin, 
		@IPOctet3End,
		@IPOctet4Begin,
		@IPOctet4End
	)
	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
		RETURN 0
	ELSE
		RETURN -1

END
GO
PRINT N'Creating [dbo].[spAdminDeleteSubscription]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/21/2006
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[spAdminDeleteSubscription]
(
	 @SubscriptionId	int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int

	DELETE FROM [dbo].[Subscription]
	WHERE
		SubscriptionId = @SubscriptionId


	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
		GOTO FAIL

	RETURN 0

FAIL:
 BEGIN
	RAISERROR('An error occured while deleting a subscription.', 16, 1)	
	RETURN -1
 END


END
GO
PRINT N'Creating [dbo].[spAdminGetVolumes]'
GO
-- =============================================
-- Author:		Monish Nagisetty
-- ALTER  date: 03/09/2006
-- Description:	Gets a list of all volumes
-- =============================================
CREATE   PROCEDURE [dbo].[spAdminGetVolumes]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int

	SELECT 
		 [VolumeId]
		,[VolumeName]+' ('+[VolumeYear]+')' as 'VolumeName'
	FROM 
		[dbo].[Volumes]

	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
	 BEGIN
		RETURN 0
	 END
	ELSE
	 BEGIN
		RAISERROR('An error occured while getting all volumes.', 16, 1)
		RETURN -1
	 END


END
GO
PRINT N'Creating [dbo].[spUpdateUserPasswordRecoveryInfo]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 09/30/2006
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateUserPasswordRecoveryInfo]
(
	@Email			varchar(50),
	@SecretQuestion1Id	int,
	@SecretQuestion2Id	int,
	@SecretAnswer1Hash	varchar(200),
	@SecretAnswer2Hash	varchar(200)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int,
		@RC	int

	UPDATE [dbo].[Users]
	SET 
		SecretQuestion1Id = @SecretQuestion1Id,
		SecretQuestion2Id = @SecretQuestion2Id,
		SecretAnswer1Hash = @SecretAnswer1Hash,
		SecretAnswer2Hash = @SecretAnswer2Hash
	WHERE 
		Email 	= @Email

	SELECT @ERROR = @@ERROR, @RC = @@ROWCOUNT

	IF (@ERROR = 0 AND @RC = 1)
	 BEGIN
		RETURN 0
	 END
	ELSE
	 BEGIN
		RETURN -1
	 END
END
GO
PRINT N'Creating [dbo].[spGetAccountStatuses]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/20/2006
-- Description:	
-- =============================================
CREATE  PROCEDURE [dbo].[spGetAccountStatuses]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int
	SELECT 
		[AccountStatusID], 
		[AccountStatusDescription]
	FROM 
		[dbo].[AccountStatus]
	WHERE
		Active = 1

	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
		RETURN 0
	ELSE
		RETURN -1
END
GO
PRINT N'Creating [dbo].[ExceptionLog]'
GO
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
GO
PRINT N'Creating primary key [PK_ExceptionLog] on [dbo].[ExceptionLog]'
GO
ALTER TABLE [dbo].[ExceptionLog] ADD CONSTRAINT [PK_ExceptionLog] PRIMARY KEY CLUSTERED  ([EventId]) ON [PRIMARY]
GO
PRINT N'Creating [dbo].[spInsertException]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/24/2006
-- Description:	
-- =============================================
CREATE  PROC dbo.spInsertException
(
	 @Source	varchar(100)
	,@LogDateTime	dateTime
	,@Message	varchar(1000)
	,@Form		varchar(4000)
	,@QueryString	varchar(2000)
	,@TargetSite	varchar(300)
	,@StackTrace	varchar(4000)
	,@Referrer 	varchar(250)
)
AS
	SET NOCOUNT ON;

	DECLARE @ERROR		int,
		@IDENTITY	int

	INSERT INTO ExceptionLog
	(
		Source, 
		LogDateTime,
		Message,
		Form,
		QueryString,
		TargetSite,
		StackTrace,
		Referrer
	)
	Values 
	( 
		@Source,
		@LogDateTime,
		@Message,
		@Form,
		@QueryString,
		@TargetSite,
		@StackTrace,
		@Referrer
	)


	SELECT @ERROR = @@ERROR, @IDENTITY = @@IDENTITY
	
	IF (@ERROR = 0)
	 BEGIN
		RETURN @IDENTITY
	 END
	ELSE
	 BEGIN
		RAISERROR('An error occured inserting into the exceptionlog table.', 16, 1)
		RETURN -1
	 END
GO
PRINT N'Creating [dbo].[TabulateCSV]'
GO
/********************************************************************
**
**	NAME: 	TabulateCSV
**
**	AUTHOR: Monish Nagisetty
**
**	DATE: 	12/30/2004
**
**	DESC: 	Converts a csv into a table.  Can return up to
**		50 items each with a max length of 100 characters.
**
**	INPUT:	@CSV - Comma seperated value 
**		Ex:  ab,cdef,gh,ijklmn,op
**
**	OUTPUT:	Result set containing a list of successful/unsuccesful
**		cases of the bulk 
**
**	UPDATES:
**	DATE	UPDATED BY		COMMENTS
**	
**
**
********************************************************************/
--Multi-statement Table-Valued User-Defined Function
CREATE     FUNCTION dbo.TabulateCSV
(
	@CSV varchar(8000)
)
RETURNS @TabularData Table
(
	[ID]		int,
	[Value]		varchar(1000)
)
AS  
BEGIN 
	INSERT INTO @TabularData
	(
		[ID],
		[Value]
	)
	(
		SELECT 
			1 - LEN(REPLACE(LEFT(@CSV, Number), ',', SPACE(0))) + Number AS [ID],
			SUBSTRING(@CSV, Number, CHARINDEX(',', @CSV + ',', Number) - Number) AS [Value]
		FROM 
			Numbers
		WHERE 
			SUBSTRING(',' + @CSV, Number, 1) = ','
			AND 
			Number < LEN(@CSV) + 1
	)
	
	RETURN
END
GO
PRINT N'Creating [dbo].[spGetAllVolumesDetailed]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/09/2006
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[spGetAllVolumesDetailed]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int

	SELECT 
		[VolumeId], 
		[VolumeName], 
		[VolumeYear], 
		[Active], 
		(
			SELECT
				COUNT(*)
			FROM
				VolumeIssues
			WHERE	
				VolumeId = v.VolumeId
		) AS Issues
	FROM 
		[dbo].[Volumes] v

	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
	 BEGIN
		RETURN 0
	 END
	ELSE
	 BEGIN
		RAISERROR('An error occured while getting all issues.', 16, 1)
		RETURN -1
	 END


END
GO
PRINT N'Creating [dbo].[spAdminGetUserIPAddresses]'
GO
/****** Object:  Stored Procedure dbo.spAdminGetUserIPAddresses    Script Date: 4/15/2007 8:05:25 PM ******/




-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/17/2006
-- Description:	This procedure returns the password
--				and salt for a given user.
-- =============================================
CREATE  PROCEDURE [dbo].[spAdminGetUserIPAddresses] 
(
	@UserId		int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int

	SELECT 
		[UserIPId], 
		[UserId], 
		[IPOctet1Begin], 
		[IPOctet2Begin], 
		[IPOctet3Begin], 
		[IPOctet3End], 
		[IPOctet4Begin], 
		[IPOctet4End] 
	FROM 
		[dbo].[UserIPAddresses]
	WHERE
		UserId = @UserId

	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
		GOTO FAIL
	ELSE
		RETURN 0	

	FAIL:
	 BEGIN
		--AN ERROR OCCURED
		RAISERROR('Failed to get user IP information.', 16, 1)
		RETURN -1
	 END
END
GO
PRINT N'Creating [dbo].[spInsertVolume]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 01/03/2009
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[spInsertVolume]
(
	 @VolumeName		varchar(30)
	,@VolumeYear		varchar(10)	
	,@Active			bit
	,@CreationUserId	int
	,@VolumeId			int 	output
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int

	--Insert the volume
	INSERT INTO [dbo].[Volumes]
    (
			[VolumeName]
           ,[VolumeYear]
           ,[Active]
           ,[CreationUserId]
           ,[CreationDateTime]
           ,[UpdateUserId]
           ,[UpdateDateTime])
     VALUES
           (@VolumeName
           ,@VolumeYear
           ,@Active
           ,@CreationUserId
           ,GETDATE()
           ,@CreationUserId
           ,GETDATE()
	)

	SELECT @ERROR = @@ERROR, @VolumeId = @@IDENTITY

	IF (@ERROR <> 0)
		GOTO FAIL
	ELSE
	 BEGIN
		RETURN -1
	 END

FAIL:
 BEGIN
	RAISERROR('An error occured while adding a volume.', 16, 1)
	RETURN -1	
 END
END
GO
PRINT N'Creating [dbo].[AddressTypes]'
GO
CREATE TABLE [dbo].[AddressTypes]
(
[AddressTypeId] [int] NOT NULL,
[AddressTypeDescription] [varchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Active] [bit] NOT NULL,
[CreationUserId] [int] NOT NULL,
[CreationDateTime] [datetime] NOT NULL,
[UpdateUserId] [int] NOT NULL,
[UpdateDateTime] [datetime] NOT NULL
) ON [PRIMARY]
GO
PRINT N'Creating primary key [PK__AddressTypes__76CBA758] on [dbo].[AddressTypes]'
GO
ALTER TABLE [dbo].[AddressTypes] ADD CONSTRAINT [PK__AddressTypes__76CBA758] PRIMARY KEY CLUSTERED  ([AddressTypeId]) ON [PRIMARY]
GO
PRINT N'Creating [dbo].[Articles]'
GO
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
[PageNumber] [int] NULL CONSTRAINT [DF__Articles__PageNu__70099B30] DEFAULT ((0))
) ON [PRIMARY]
GO
PRINT N'Creating primary key [PK__Articles__78B3EFCA] on [dbo].[Articles]'
GO
ALTER TABLE [dbo].[Articles] ADD CONSTRAINT [PK__Articles__78B3EFCA] PRIMARY KEY CLUSTERED  ([ArticleId]) ON [PRIMARY]
GO
PRINT N'Creating [dbo].[spAdminGetSubscriptions]'
GO
-- =============================================
--  Author: Monish Nagisetty
-- ALTER  date: 03/21/2006

--  Author: Parag Jagdale
-- ALTER date: 01/03/2007
-- Description:	Gets all subscription data
-- =============================================
CREATE   PROCEDURE [dbo].[spAdminGetSubscriptions]
(
	 @UserId	int 
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int
	
	--// TODO	Monish Please check these joins for errors. I have not tested as I only
	--//		have a few data sets for each table
	SELECT
		 s.[SubscriptionId]
		,s.VolumeIssueId
		,s.ArticleId 
		,v.[VolumeName]
		,i.[IssueName]
		,CAST(a.[PageNumber] AS VARCHAR)+' - '+LEFT(a.[Title], 15)+'...' AS ArticleName
		,s.[EffectiveDate]
		,s.[ExpirationDate]
		,s.[Active]
	FROM
		Subscription s
			INNER JOIN Articles a 		ON s.ArticleId 		= a.ArticleId
			INNER JOIN VolumeIssues vi 	ON s.VolumeIssueId 	= vi.VolumeIssueId,
	
		VolumeIssues vi2
			INNER JOIN Volumes v		ON vi2.VolumeId 	= v.VolumeId
			INNER JOIN Issues i 		ON vi2.IssueId 		= i.IssueId
		
		
	WHERE
		 s.UserId = @UserId
	AND
		 s.VolumeIssueId  = vi2.VolumeIssueId
		
	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
		GOTO FAIL
	ELSE
		RETURN 0

FAIL:
 BEGIN
	RAISERROR('An error occured while retrieving the user''s subscriptions.', 16, 1)	
	RETURN -1
 END


END
GO
PRINT N'Creating [dbo].[spGetAllUsersDetailed]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/19/2006
-- Description:	
-- =============================================
CREATE  PROCEDURE [dbo].[spGetAllUsersDetailed]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int

	SELECT 
		u.[UserId], 
		u.[Email], 
		u.[FirstName], 
		u.[LastName], 
		u.[MiddleInitial], 
		--u.[AccountTypeId], 
		acTypes.[Description] as AcctTypeDescription,
		--u.[AccountStatus], 
		acct.[AccountStatusDescription] as AcctStatusDescription,
		u.[Active]
	FROM 
		[dbo].[Users] u
	INNER JOIN AccountStatus acct ON
		acct.AccountStatusID = u.AccountStatus
	INNER JOIN AccountTypes acTypes ON
		acTypes.AccountTypeId = u.AccountTypeId

	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
	 BEGIN
		RETURN 0
	 END
	ELSE
	 BEGIN
		RAISERROR('An error occured while getting all users.', 16, 1)
		RETURN -1
	 END

END
GO
PRINT N'Creating [dbo].[spGetAllIssues]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/09/2006
-- Description:	Gets a list of all issues for
--		a volume
-- =============================================
CREATE PROCEDURE [dbo].[spGetAllIssues]
(
	@VolumeId	int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int

	SELECT 
		 [VolumeIssueId]
		,[IssueName]
	FROM 
		[dbo].[Issues] i
	INNER JOIN VolumeIssues vi ON
		i.IssueId = vi.IssueId
	WHERE
		vi.VolumeId = @VolumeId

	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
	 BEGIN
		RETURN 0
	 END
	ELSE
	 BEGIN
		RAISERROR('An error occured while getting all issues.', 16, 1)
		RETURN -1
	 END


END
GO
PRINT N'Creating [dbo].[spAdminGetArticleDocs]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/09/2006
-- Description:	Gets a list of all issues for
--		a volume
-- =============================================
CREATE  PROCEDURE [dbo].[spAdminGetArticleDocs]
(
	@ArticleId	int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int

	SELECT
		 d.DocId
		,FileName
		,ad.ArtDocTypeId
		,ArtDocTypeDescription
		,d.Active
	FROM
		Documents d
	INNER JOIN ArticleDocuments ad ON
		d.DocId = ad.DocId
	INNER JOIN ArticleDocTypes adt ON
		adt.ArtDocTypeId = ad.ArtDocTypeId
	WHERE
		ad.ArticleId = @ArticleId


	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
	 BEGIN
		RETURN 0
	 END
	ELSE
	 BEGIN
		RAISERROR('An error occured while getting article documents.', 16, 1)
		RETURN -1
	 END


END
GO
PRINT N'Creating [dbo].[spAdminGetArticles]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/09/2006
-- Description:	Gets a list of all issues for
--		a volume
-- =============================================
CREATE PROCEDURE [dbo].[spAdminGetArticles]
(
	@VolumeIssueId	int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int

	SELECT 
		a.ArticleId
		,CAST([PageNumber] AS VARCHAR)+' - '+LEFT(Title, 20)+'...' AS ArticleName
	FROM 
		[dbo].[Articles] a
	INNER JOIN IssueArticles ia ON
		a.ArticleId = ia.ArticleId
	WHERE
		ia.VolumeIssueId = @VolumeIssueId
	ORDER BY
		PageNumber

	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
	 BEGIN
		RETURN 0
	 END
	ELSE
	 BEGIN
		RAISERROR('An error occured while getting all articles.', 16, 1)
		RETURN -1
	 END


END
GO
PRINT N'Creating [dbo].[spInsertIssue]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/19/2006
-- Description:	
-- =============================================
CREATE   PROCEDURE [dbo].[spInsertIssue]
(
	 @VolumeId		int
	,@IssueName		varchar(50)
	,@Active		bit
	,@CreationUserId	int
	,@VolumeIssueId		int 	output
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int,
		@IssueId 	int

	BEGIN TRAN

	--Insert the issue
	INSERT INTO [dbo].[Issues]
	(
		[IssueName], 
		[Active], 
		[CreationUserId], 
		[CreationDateTime], 
		[UpdateUserId], 
		[UpdateDateTime]
	)
	VALUES
	(
		 @IssueName
		,@Active
		,@CreationUserId
		,GETDATE()
		,@CreationUserId
		,GETDATE()
	)

	SELECT @ERROR = @@ERROR, @IssueId = @@IDENTITY

	IF (@ERROR <> 0)
		GOTO FAIL

	--Insert Issue into volume issue table
	INSERT INTO [dbo].[VolumeIssues]
	(
		[VolumeId], 
		[IssueId]
	)
	VALUES
	(
		 @VolumeId
		,@IssueId
	)

	SELECT @ERROR = @@ERROR, @VolumeIssueId = @@IDENTITY

	IF (@ERROR <> 0)
		GOTO FAIL
	ELSE
	 BEGIN
		COMMIT TRAN
		RETURN -1
	 END

FAIL:
 BEGIN
	ROLLBACK TRAN
	RAISERROR('An error occured while adding an issue.', 16, 1)
	RETURN -1	
 END
END
GO
PRINT N'Creating [dbo].[spAdminGetAllUsers]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/19/2006
-- Description:	
-- =============================================
CREATE  PROCEDURE [dbo].[spAdminGetAllUsers]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int

	SELECT 
		u.[UserId], 
		u.[Email], 
		u.[FirstName], 
		u.[LastName], 
		u.[MiddleInitial], 
		--u.[AccountTypeId], 
		acTypes.[Description] as AcctTypeDescription,
		--u.[AccountStatus], 
		acct.[AccountStatusDescription] as AcctStatusDescription,
		u.[Active]
	FROM 
		[dbo].[Users] u
	INNER JOIN AccountStatus acct ON
		acct.AccountStatusID = u.AccountStatus
	INNER JOIN AccountTypes acTypes ON
		acTypes.AccountTypeId = u.AccountTypeId

	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
	 BEGIN
		RETURN 0
	 END
	ELSE
	 BEGIN
		RAISERROR('An error occured while getting all users.', 16, 1)
		RETURN -1
	 END

END
GO
PRINT N'Creating [dbo].[spGetPasswordQuestionsByEmail]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/17/2006
-- Description:	This procedure returns the password
--				and salt for a given user.
-- =============================================
CREATE PROCEDURE [dbo].[spGetPasswordQuestionsByEmail] 
(
	@Email	varchar(50)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int,
		@RC		int

	SELECT 
		[QuestionId],
		[Description]
	FROM 
		[PasswordRecoveryQuestions] prq
	INNER JOIN Users u ON
		prq.QuestionId = u.SecretQuestion1Id
		or
		prq.QuestionId = u.SecretQuestion2Id
	WHERE 
		u.Email = @Email
		AND
		u.Active = 1

	SELECT @ERROR = @@ERROR, @RC = @@ROWCOUNT

	IF (@ERROR <> 0 OR @RC = 0)
		GOTO FAIL
	ELSE
		RETURN 0	

	FAIL:
	 BEGIN
		--AN ERROR OCCURED
		RAISERROR('Failed to get user security information.', 16, 1)
		RETURN -1
	 END
END
GO
PRINT N'Creating [dbo].[spAdminGetIssueDocs]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/09/2006
-- Description:	Gets a list of all issues for
--		a volume
-- =============================================
CREATE     PROCEDURE [dbo].[spAdminGetIssueDocs]
(
	@VolumeIssueId		int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int

	SELECT
		 d.DocId
		,FileName
		,[id].IssueDocTypeId
		,IssueDocTypeDescription
		,d.Active
	FROM
		Documents d
	INNER JOIN IssueDocuments [id] ON
		d.DocId = [id].DocId
	INNER JOIN IssueDocTypes idt ON
		idt.IssueDocTypeId = [id].IssueDocTypeId
	WHERE
		[id].VolumeIssueId = @VolumeIssueId


	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
	 BEGIN
		RETURN 0
	 END
	ELSE
	 BEGIN
		RAISERROR('An error occured while getting issue details.', 16, 1)
		RETURN -1
	 END


END
GO
PRINT N'Creating [dbo].[spAdminGetAvailableSubscriptions]'
GO
/*=============================================
Author:  Monish Nagisetty
Created: 01/14/2007
Description:	This procedure returns the 
available subscriptions.
===============================================
Revision History
=============================================================================
Change   Initial Date        Description
    ID        AA mm/dd/yyyy  Describe reason for change.                            
=============================================================================*/
CREATE PROCEDURE [dbo].[spAdminGetAvailableSubscriptions]
(
	@UserId			int,
	@VolumeIssueId		int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int
	
	SELECT
		 a.ArticleId,
		CAST([PageNumber] AS VARCHAR)+' - '+LEFT(Title, 20)+'...' AS Title
	FROM
		Articles a
	INNER JOIN IssueArticles ia ON
		ia.ArticleId = a.ArticleId
	WHERE
		ia.VolumeIssueId	= @VolumeIssueId
		AND
		a.ArticleId NOT IN
			(
				SELECT 
					s.ArticleId
				FROM
					Subscription s
				WHERE
					s.VolumeIssueId		= @VolumeIssueId
					AND
					s.UserId			= @UserId	
			)
				
	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
		GOTO FAIL
	ELSE
		RETURN 0

FAIL:
 BEGIN
	RAISERROR('An error occured while retrieving the user''s available subscriptions.', 16, 1)	
	RETURN -1
 END


END
GO
PRINT N'Creating [dbo].[spUpdateArticle]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/09/2006
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateArticle]
(
	 @ArticleId		int
	,@Title			varchar(1000)
	,@Authors		varchar(1000)
	,@Keywords		varchar(1000)
	,@PageNumber		int
	,@Active		bit
	,@UpdateUserId		int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int

	UPDATE [Articles]
	SET 
		 [Title] 		= @Title
		,[Authors]		= @Authors
		,[Keywords]		= @Keywords
		,[PageNumber]		= @PageNumber
		,[Active]		= @Active
		,[UpdateUserId]		= @UpdateUserId
		,[UpdateDateTime]	= GETDATE()
	WHERE 
		ArticleId 		= @ArticleId

	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
		RETURN 0
	ELSE
	 BEGIN
		RAISERROR('An error occured while updating article details.', 16, 1)
		RETURN -1
	 END
END
GO
PRINT N'Creating [dbo].[spAdminGetIssue]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/09/2006
-- Description:	Gets a list of all issues for
--		a volume
-- =============================================
CREATE   PROCEDURE [dbo].[spAdminGetIssue]
(
	@VolumeIssueId		int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int

	SELECT 
		i.[IssueId], 
		[IssueName], 
		[Active]
	FROM 
		[dbo].[Issues] i
	INNER JOIN VolumeIssues vi ON
		vi.IssueId = i.IssueId	
	WHERE
		vi.VolumeIssueId = @VolumeIssueId

	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
		GOTO FAIL

	EXEC dbo.spAdminGetIssueDocs @VolumeIssueId	

	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
		RETURN 0
	ELSE
		GOTO FAIL
	

FAIL:
 BEGIN
	RAISERROR('An error occured while getting issue details.', 16, 1)
	RETURN -1
 END


END
GO
PRINT N'Creating [dbo].[spGetVolumeAndIssueName]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/09/2006
-- Description:	Gets the volume and issue name.
-- =============================================
CREATE  PROCEDURE [dbo].[spGetVolumeAndIssueName]
(
	@VolumeIssueId	int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int

	--Return the Volume Name, Year and issue name
	SELECT
		 VolumeName
		,VolumeYear
		,IssueName	
	FROM
		 VolumeIssues vi
		,Volumes v
		,Issues i
	WHERE
		vi.VolumeIssueId = @VolumeIssueId
		AND
		vi.VolumeId = v.VolumeId
		AND
		vi.IssueId = i.IssueId
		AND
		v.Active = 1
		AND
		i.Active = 1

	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
	 BEGIN
		RETURN 0
	 END
	ELSE
	 BEGIN
		RAISERROR('An error occurred while retrieving the volume and issue name.', 16, 1)
		RETURN -1
	 END


END
GO
PRINT N'Creating [dbo].[spGetArticleDocumentsBySubscription]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/18/2006
-- Description:	Gets all the active articles
--		and document links within an 
--		issue.
-- =============================================
CREATE PROCEDURE [dbo].[spGetArticleDocumentsBySubscription] 
(
	 @Userid		int
	,@VolumeIssueId	int
	,@ArticleId		int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR			int,
		@HasSubscription	int
				
	--Returns 0 or more depending on whether
	--the user has a valid subscription
	SELECT @HasSubscription = [dbo].[IsUserArticleSubscriber](@UserId, @VolumeIssueId, @ArticleId, GETDATE())

	--Returns a list of all documents for the current
	--article
	SELECT
		 adt.ArtDocTypeDescription
		,adt.ArtDocTypeId
		,d.DocId
		,d.FileSizeKB
	FROM
		Documents d
	INNER JOIN ArticleDocuments ad ON
		d.DocId = ad.DocId
	INNER JOIN ArticleDocTypes adt ON
		ad.ArtDocTypeId	= adt.ArtDocTypeId
	WHERE
		ad.ArticleId = @ArticleId
		AND
		d.Active = 1
		AND
		(   --If the user has subscription for this article
			@HasSubscription > 0 
			OR
			(--If the user does not have subscriptions then
		  	 --only return publicly available documents
				@HasSubscription = 0
				AND
				adt.PubliclyAvailable = 1					
			)
		)
	ORDER BY
		 ad.ArticleId
		,adt.ArtDocTypeDescription		

	
	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
		GOTO FAIL
	ELSE
		RETURN 0


FAIL:
 BEGIN
	RAISERROR('An error occured while retrieving article documents.', 16, 1)	
	RETURN -1
 END


END
GO
PRINT N'Creating [dbo].[IsUserIssueSubscriber]'
GO
/*=============================================
Author:  Monish Nagisetty
Created: 03/22/2006
Description:	This procedure determines if a
user is subscribed to all the articles in a 
issue.
===============================================
Revision History
=============================================================================
Change   Initial Date        Description
    ID        AA mm/dd/yyyy  Describe reason for change.                            
=============================================================================*/
CREATE FUNCTION dbo.IsUserIssueSubscriber
(
	 @UserId			int
	,@VolumeIssueId		int
	,@CurrentDate		datetime
)  
RETURNS int
AS
BEGIN 
	DECLARE @SubscriptionCount	int
	DECLARE @ArticleCount		int
	DECLARE @IsUserSubscriber	int
	SET @IsUserSubscriber = 0

	--Return total subscriptions for this issue
	SELECT DISTINCT
		@SubscriptionCount = COUNT(*)
	FROM
		Subscription s
	INNER JOIN Users u on 
		s.UserId = u.UserId
	WHERE
		u.UserId		= @UserId
		AND
		u.AccountStatus = 1 -- Account Active
		AND	
		u.Active		= 1 --User should be active
		AND 
		s.Active		= 1 --Subscription should be active
		AND
		s.VolumeIssueId = @VolumeIssueId --
		AND
		DATEDIFF(dd, s.EffectiveDate, @CurrentDate) >= 0
		AND
		DATEDIFF(DD, s.ExpirationDate, @CurrentDate) <= 0		

	--Return total articles for this issue
	SELECT
		@ArticleCount = COUNT(*)
	FROM
		Articles a
	INNER JOIN IssueArticles ia ON
		ia.ArticleId = a.ArticleId
	WHERE
		ia.VolumeIssueId = @VolumeIssueId
		AND
		a.Active = 1

	IF (@SubscriptionCount >= @ArticleCount)
		SET @IsUserSubscriber = 1
	ELSE
		SET @IsUserSubscriber = 0

	RETURN (@IsUserSubscriber)
END
GO
PRINT N'Creating [dbo].[spDeleteUser]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/19/2006
-- Description:	Deletes records from the documents
--		and issuedocuments tables
-- =============================================
CREATE  PROCEDURE [dbo].[spDeleteUser] 
(
	@UserId		int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int
		
	DELETE dbo.Addresses
	FROM
		UserAddresses ua
	WHERE
		ua.AddressId = Addresses.AddressId
		and
		ua.UserId = @UserId
		
	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
		GOTO FAIL

	DELETE FROM [dbo].[Users]
	WHERE 
		UserId = @UserId

	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
		GOTO FAIL
	ELSE
		RETURN 0

FAIL:
 BEGIN
	RAISERROR('An error occured deleting an user.', 16, 1)	
	RETURN -1
 END


END
GO
PRINT N'Creating [dbo].[spGetActiveArticles]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/18/2006
-- Description:	Gets all the active articles
--		and document links within an 
--		issue.
-- =============================================
CREATE   PROCEDURE [dbo].[spGetActiveArticles] 
(
	 @VolumeIssueId		int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int,
		@RC		int
	
	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
		GOTO FAIL

	--Returns all the articles for the current issue
	SELECT
		 a.ArticleId
		,a.Title
		,a.Authors
		,a.Keywords
		,a.CreationDateTime
	FROM
		Articles a
	INNER JOIN IssueArticles ia ON
		ia.ArticleId = a.ArticleId
	WHERE
		ia.VolumeIssueId = @VolumeIssueId
		AND
		a.Active = 1
	ORDER BY
		a.ArticleId

	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
		GOTO FAIL
	ELSE
		RETURN 0


FAIL:
 BEGIN
	RAISERROR('An error occured while retrieving article information.', 16, 1)	
	RETURN -1
 END


END
GO
PRINT N'Creating [dbo].[spInsertUserAddress]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date:  03/05/2006
-- Description:  Inserts a user's address into 
--				the user's table
-- =============================================
CREATE  PROCEDURE [dbo].[spInsertUserAddress] 
(
	 @UserId			int
	,@AddressTypeId		int
	,@Line1				varchar(50)
	,@Line2				varchar(50)
	,@City				varchar(50)
	,@StateProvince		varchar(50)
	,@CountryId			int
	,@Phone				varchar(30)
	,@Fax				varchar(30)
	,@Active			bit
	,@CreationUserId	int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int,
			@RC			int,
			@IDENTITY	int

--START TRANSACTION
	BEGIN TRAN

INSERT INTO [dbo].[Addresses]
    (
			[Line1]
           ,[Line2]
           ,[City]
           ,[StateProvince]
           ,[CountryId]
           ,[Phone]
           ,[Fax]
           ,[Active]
           ,[CreationUserId]
           ,[CreationDateTime]
           ,[UpdateUserId]
           ,[UpdateDateTime])
     VALUES
	 (
			@Line1
           ,@Line2
           ,@City
           ,@StateProvince
           ,@CountryId
           ,@Phone
           ,@Fax
           ,@Active
           ,@CreationUserId
           ,GETDATE()
           ,@CreationUserId
           ,GETDATE()
	)

	SELECT @ERROR = @@ERROR, @IDENTITY = @@IDENTITY

	IF (@ERROR <> 0)
		GOTO FAIL
	ELSE
	 BEGIN
		INSERT INTO [dbo].[UserAddresses]
		(
				[UserId]
			   ,[AddressId]
			   ,[AddressTypeId]
		)
		VALUES
		(
				@UserId
			   ,@IDENTITY
			   ,@AddressTypeId
		)		

		SELECT @ERROR = @@ERROR

		IF (@ERROR <> 0)
			GOTO FAIL
		ELSE
			GOTO SUCCESS

	 END


	SUCCESS:
	 BEGIN
		COMMIT TRAN
		RETURN 0
	 END

	FAIL:
	 BEGIN
		ROLLBACK TRAN
		RAISERROR('Failed to add address for user.', 16, 1)
		RETURN -1
	 END
END
GO
PRINT N'Creating [dbo].[spGetIssueDocumentsBySubscription]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- Created: 01/04/2006
-- Modified: 01/04/2006
-- Description:	Gets all the issue documents for
-- the current issue and based on the user's 
-- subscriptions.
-- =============================================
CREATE PROCEDURE [dbo].[spGetIssueDocumentsBySubscription] 
(
	 @UserId			int
	,@VolumeIssueId		int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR				int,
			@HasSubscription	int
				
	--Returns 0 or more depending on whether
	--the user has a valid subscription
	SELECT @HasSubscription = [dbo].[IsUserIssueSubscriber](@UserId, @VolumeIssueId, GETDATE())

	--Returns a list of all issue documents for the current
	--issue
	SELECT
		idt.IssueDocTypeDescription
		,d.DocId
		,d.FileSizeKB
	FROM
		Documents d
	INNER JOIN IssueDocuments idoc ON
		d.DocId = idoc.DocId
	INNER JOIN IssueDocTypes idt ON
		idoc.IssueDocTypeId = idt.IssueDocTypeId
	WHERE
		idoc.VolumeIssueId = @VolumeIssueId
		AND
		d.Active = 1
		AND
		(   --If the user has subscription for this article
			@HasSubscription > 0 
			OR
			(--If the user does not have subscriptions then
		  	 --only return publicly available documents
				@HasSubscription = 0
				AND
				idt.PubliclyAvailable = 1					
			)
		)		
	ORDER BY
		idt.IssueDocTypeDescription

	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
		GOTO FAIL
	ELSE
		RETURN 0


FAIL:
 BEGIN
	RAISERROR('An error occured while retrieving issue documents.', 16, 1)	
	RETURN -1
 END


END
GO
PRINT N'Creating [dbo].[spInsertArticle]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/18/2006
-- Description:	Inserts a record into the articles
--		and issuearticles tables for creating
--		an article.
-- =============================================
CREATE     PROCEDURE [dbo].[spInsertArticle] 
(
	 @VolumeIssueId		int
	,@Title			varchar(1000)
	,@Authors		varchar(1000)
	,@Keywords		varchar(2000)
	,@Active		bit
	,@CreationUserId	int
	,@ArticleId		int	output
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int,
		@RC		int

	INSERT INTO [dbo].[Articles]
	(
		 [Title]
		,[Authors]
		,[Keywords]
		,[Active]
		,[CreationUserId]
		,[CreationDateTime]
		,[UpdateUserId]
		,[UpdateDateTime]
	)
	VALUES
	(
		 @Title
		,@Authors
		,@Keywords
		,@Active
		,@CreationUserId
		,GETDATE()
		,@CreationUserId
		,GETDATE()
	)

	SELECT @ERROR = @@ERROR, @ArticleId = @@IDENTITY

	IF (@ERROR <> 0)
		GOTO FAIL

	--Add this article to the issue
	INSERT INTO [dbo].[IssueArticles]
	(
		[VolumeIssueId], 
		[ArticleId]
	)
	VALUES
	(	 
		 @VolumeIssueId
		,@ArticleId
	)
		
	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
		GOTO FAIL
	ELSE
		RETURN 0

FAIL:
 BEGIN
	RAISERROR('An error occured adding an article.', 16, 1)	
	RETURN -1
 END


END
GO
PRINT N'Creating [dbo].[spAdminGetArticle]'
GO
-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/09/2006
-- Description:	Gets a list of all issues for
--		a volume
-- =============================================
CREATE  PROCEDURE [dbo].[spAdminGetArticle]
(
	@ArticleId	int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int

	SELECT
		 ArticleId
		,Title
		,Authors
		,Keywords
		,PageNumber
		,Active
	FROM
		Articles a
	WHERE
		a.ArticleId = @ArticleId

	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
		GOTO FAIL

	EXEC dbo.spAdminGetArticleDocs @ArticleId

	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
		RETURN 0
	ELSE
		GOTO FAIL
	

FAIL:
 BEGIN
	RAISERROR('An error occured while getting article details.', 16, 1)
	RETURN -1
 END


END
GO
PRINT N'Creating [dbo].[LoginLog]'
GO
CREATE TABLE [dbo].[LoginLog]
(
[DateTime] [datetime] NOT NULL,
[IPAddress] [varchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Email] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[SuccessFlag] [bit] NOT NULL
) ON [PRIMARY]
GO
PRINT N'Creating primary key [PK__LoginLog__7B905C75] on [dbo].[LoginLog]'
GO
ALTER TABLE [dbo].[LoginLog] ADD CONSTRAINT [PK__LoginLog__7B905C75] PRIMARY KEY CLUSTERED  ([DateTime], [IPAddress], [Email]) ON [PRIMARY]
GO
PRINT N'Creating [dbo].[GranExecRights]'
GO
/* ------------------------------------------------------------
PROCEDURE: prc_gen_CreateGrants

DESCRIPTION: Grants Execute permissions on all procs in database
for Login MyLogin

AUTHOR: Brian Lockwood 3/15/00 5:38:48 PM
------------------------------------------------------------ */
CREATE    PROCEDURE dbo.GranExecRights
AS

DECLARE @LoginName	varchar(50)
DECLARE @ExecSQL 	varchar(100)

SET @LoginName = 'db_npcwebrole'

DECLARE curGrants CURSOR FOR
SELECT 
	'GRANT EXECUTE ON ' + NAME + ' TO '+@LoginName
FROM 
	dbo.sysobjects
WHERE 
	TYPE = 'P'
	AND 
	LEFT(NAME,2) = 'sp' 

OPEN curGrants
FETCH NEXT FROM curGrants
INTO @ExecSQL


WHILE @@FETCH_STATUS = 0
 BEGIN -- this will loop thru all your own procs and grant Execute privileges on each one

	Exec(@ExecSQL)

	IF @@ERROR <> 0
	 BEGIN
 		RETURN 1 -- return 1 if there is an error
	 END
	
	Print @ExecSQL
	
	FETCH NEXT FROM curGrants INTO @ExecSQL

 END

CLOSE curGrants
DEALLOCATE curGrants
GO
PRINT N'Adding foreign keys to [dbo].[UserAddresses]'
GO
ALTER TABLE [dbo].[UserAddresses] WITH NOCHECK ADD
CONSTRAINT [FK_UserAddresses_Addresses] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Addresses] ([AddressId]) ON DELETE CASCADE NOT FOR REPLICATION,
CONSTRAINT [FK_UserAddresses_AddressTypes] FOREIGN KEY ([AddressTypeId]) REFERENCES [dbo].[AddressTypes] ([AddressTypeId]) NOT FOR REPLICATION,
CONSTRAINT [FK_UserAddresses_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId]) ON DELETE CASCADE NOT FOR REPLICATION
GO
PRINT N'Adding foreign keys to [dbo].[Subscription]'
GO
ALTER TABLE [dbo].[Subscription] WITH NOCHECK ADD
CONSTRAINT [FK_Subscription_Articles] FOREIGN KEY ([ArticleId]) REFERENCES [dbo].[Articles] ([ArticleId]) ON DELETE CASCADE NOT FOR REPLICATION,
CONSTRAINT [FK_Subscription_VolumeIssues] FOREIGN KEY ([VolumeIssueId]) REFERENCES [dbo].[VolumeIssues] ([VolumeIssueId]) ON DELETE CASCADE NOT FOR REPLICATION,
CONSTRAINT [FK_Subscription_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId]) ON DELETE CASCADE NOT FOR REPLICATION
GO
PRINT N'Adding foreign keys to [dbo].[IssueArticles]'
GO
ALTER TABLE [dbo].[IssueArticles] WITH NOCHECK ADD
CONSTRAINT [FK_IssueArticles_Articles] FOREIGN KEY ([ArticleId]) REFERENCES [dbo].[Articles] ([ArticleId]) NOT FOR REPLICATION,
CONSTRAINT [FK_IssueArticles_VolumeIssues] FOREIGN KEY ([VolumeIssueId]) REFERENCES [dbo].[VolumeIssues] ([VolumeIssueId]) NOT FOR REPLICATION
GO
PRINT N'Adding foreign keys to [dbo].[Users]'
GO
ALTER TABLE [dbo].[Users] WITH NOCHECK ADD
CONSTRAINT [FK_Users_AccountStatus] FOREIGN KEY ([AccountStatus]) REFERENCES [dbo].[AccountStatus] ([AccountStatusID]) NOT FOR REPLICATION,
CONSTRAINT [FK_Users_AccountTypes] FOREIGN KEY ([AccountTypeId]) REFERENCES [dbo].[AccountTypes] ([AccountTypeId]) NOT FOR REPLICATION
GO
PRINT N'Adding foreign keys to [dbo].[VolumeIssues]'
GO
ALTER TABLE [dbo].[VolumeIssues] WITH NOCHECK ADD
CONSTRAINT [FK_VolumeIssues_Issues] FOREIGN KEY ([IssueId]) REFERENCES [dbo].[Issues] ([IssueId]) NOT FOR REPLICATION,
CONSTRAINT [FK_VolumeIssues_Volumes] FOREIGN KEY ([VolumeId]) REFERENCES [dbo].[Volumes] ([VolumeId]) NOT FOR REPLICATION
GO
PRINT N'Adding foreign keys to [dbo].[ArticleDocuments]'
GO
ALTER TABLE [dbo].[ArticleDocuments] WITH NOCHECK ADD
CONSTRAINT [FK_ArticleDocuments_ArticleDocTypes] FOREIGN KEY ([ArtDocTypeId]) REFERENCES [dbo].[ArticleDocTypes] ([ArtDocTypeId]) NOT FOR REPLICATION,
CONSTRAINT [FK_ArticleDocuments_Articles] FOREIGN KEY ([ArticleId]) REFERENCES [dbo].[Articles] ([ArticleId]) NOT FOR REPLICATION,
CONSTRAINT [FK_ArticleDocuments_Documents] FOREIGN KEY ([DocId]) REFERENCES [dbo].[Documents] ([DocId]) NOT FOR REPLICATION
GO
PRINT N'Adding foreign keys to [dbo].[UserRoles]'
GO
ALTER TABLE [dbo].[UserRoles] WITH NOCHECK ADD
CONSTRAINT [FK_UserRoles_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([RoleId]) NOT FOR REPLICATION,
CONSTRAINT [FK_UserRoles_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId]) ON DELETE CASCADE NOT FOR REPLICATION
GO
PRINT N'Adding foreign keys to [dbo].[Addresses]'
GO
ALTER TABLE [dbo].[Addresses] WITH NOCHECK ADD
CONSTRAINT [FK_Addresses_Countries] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Countries] ([CountryId]) NOT FOR REPLICATION
GO
PRINT N'Adding foreign keys to [dbo].[UserIPAddresses]'
GO
ALTER TABLE [dbo].[UserIPAddresses] WITH NOCHECK ADD
CONSTRAINT [FK_UserIPAddresses_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId]) ON DELETE CASCADE NOT FOR REPLICATION
GO
PRINT N'Adding foreign keys to [dbo].[IssueDocuments]'
GO
ALTER TABLE [dbo].[IssueDocuments] WITH NOCHECK ADD
CONSTRAINT [FK_IssueDocuments_Documents] FOREIGN KEY ([DocId]) REFERENCES [dbo].[Documents] ([DocId]) NOT FOR REPLICATION,
CONSTRAINT [FK_IssueDocuments_IssueDocTypes] FOREIGN KEY ([IssueDocTypeId]) REFERENCES [dbo].[IssueDocTypes] ([IssueDocTypeId]) NOT FOR REPLICATION,
CONSTRAINT [FK_IssueDocuments_VolumeIssues] FOREIGN KEY ([VolumeIssueId]) REFERENCES [dbo].[VolumeIssues] ([VolumeIssueId]) NOT FOR REPLICATION
GO

GO
-- Post-Deployment Script Template							
----------------------------------------------------------------------------------------
-- This file contains SQL statements that will be appended to the build script		
-- Use SQLCMD syntax to include a file into the post-deployment script			
-- Example:      :r .\filename.sql								
-- Use SQLCMD syntax to reference a variable in the post-deployment script		
-- Example:      :setvar $TableName MyTable							
--               SELECT * FROM [$(TableName)]					
----------------------------------------------------------------------------------------


EXEC dbo.GranExecRights
GO










-- =============================================
-- Script Template
-- =============================================
--ADDS REFERENCE DATA

--ADDS Numbers
DECLARE @NumberCount int
SET @NumberCount = 1

WHILE @NumberCount < 8002
 BEGIN -- this will loop thru all your own procs and grant Execute privileges on each one

	INSERT INTO [dbo].[Numbers]([NullCol])
    VALUES(null)

	SET @NumberCount = @NumberCount + 1
 END

--ACCOUNT STATUSES
INSERT INTO [dbo].[AccountStatus]
(
	[AccountStatusID], 
	[AccountStatusDescription], 
	[Active], 
	[CreateUserId], 
	[CreateDateTime], 
	[UpdateUserId], 
	[UpdateDateTime]
)
VALUES(
	1 
	,'Account Active'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
)

INSERT INTO [dbo].[AccountStatus]
(
	[AccountStatusID], 
	[AccountStatusDescription], 
	[Active], 
	[CreateUserId], 
	[CreateDateTime], 
	[UpdateUserId], 
	[UpdateDateTime]
)
VALUES(
	2 
	,'Account Locked'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
)


INSERT INTO [dbo].[AccountStatus]
(
	[AccountStatusID], 
	[AccountStatusDescription], 
	[Active], 
	[CreateUserId], 
	[CreateDateTime], 
	[UpdateUserId], 
	[UpdateDateTime]
)
VALUES(
	3 
	,'Account Stale'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
)

--ADD ACCOUNT TYPES
INSERT INTO [dbo].[AccountTypes]
(
	[AccountTypeId], 
	[Description], 
	[Active], 
	[CreationUserId], 
	[CreationDateTime], 
	[UpdateUserId], 
	[UpdateDateTime]
)
VALUES(
	1 
	,'Personal'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
)
INSERT INTO [dbo].[AccountTypes]
(
	[AccountTypeId], 
	[Description], 
	[Active], 
	[CreationUserId], 
	[CreationDateTime], 
	[UpdateUserId], 
	[UpdateDateTime]
)
VALUES(
	2 
	,'Institutional'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
)

--ADD ADDRESSTYPES

INSERT INTO [dbo].[AddressTypes]
(
	[AddressTypeId], 
	[AddressTypeDescription], 
	[Active], 
	[CreationUserId], 
	[CreationDateTime], 
	[UpdateUserId], 
	[UpdateDateTime]
)
VALUES
(
	1 
	,'Default'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
)

INSERT INTO [dbo].[AddressTypes]
(
	[AddressTypeId], 
	[AddressTypeDescription], 
	[Active], 
	[CreationUserId], 
	[CreationDateTime], 
	[UpdateUserId], 
	[UpdateDateTime]
)
VALUES
(
	2 
	,'Billing'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
)

--ADD COUNTRIES
DECLARE @CSV varchar(8000)
DECLARE @CSV2 varchar(8000)

SET @CSV = 'AP,EU,AD,AE,AF,AG,AI,AL,AM,AN,AO,AQ,AR,AS,AT,AU,AW,AZ,BA,BB,BD,BE,BF,BG,BH,BI,BJ,BM,BN,BO,BR,BS,BT,BV,BW,BY,BZ,CA,CC,CD,CF,CG,CH,CI,CK,CL,CM,CN,CO,CR,CU,CV,CX,CY,CZ,DE,DJ,DK,DM,DO,DZ,EC,EE,EG,EH,ER,ES,ET,FI,FJ,FK,FM,FO,FR,FX,GA,GB,GD,GE,GF,GH,GI,GL,GM,GN,GP,GQ,GR,GS,GT,GU,GW,GY,HK,HM,HN,HR,HT,HU,ID,IE,IL,IN,IO,IQ,IR,IS,IT,JM,JO,JP,KE,KG,KH,KI,KM,KN,KP,KR,KW,KY,KZ,LA,LB,LC,LI,LK,LR,LS,LT,LU,LV,LY,MA,MC,MD,MG,MH,MK,ML,MM,MN,MO,MP,MQ,MR,MS,MT,MU,MV,MW,MX,MY,MZ,NA,NC,NE,NF,NG,NI,NL,NO,NP,NR,NU,NZ,OM,PA,PE,PF,PG,PH,PK,PL,PM,PN,PR,PS,PT,PW,PY,QA,RE,RO,RU,RW,SA,SB,SC,SD,SE,SG,SH,SI,SJ,SK,SL,SM,SN,SO,SR,ST,SV,SY,SZ,TC,TD,TF,TG,TH,TJ,TK,TM,TN,TO,TP,TR,TT,TV,TW,TZ,UA,UG,UM,US,UY,UZ,VA,VC,VE,VG,VI,VN,VU,WF,WS,YE,YT,CS,ZA,ZM,ZR,ZW,A1,A2'
SET @CSV2 = 'Asia/Pacific Region;Europe;Andorra;United Arab Emirates;Afghanistan;Antigua and Barbuda;Anguilla;Albania;Armenia;Netherlands Antilles;Angola;Antarctica;Argentina;American Samoa;Austria;Australia;Aruba;Azerbaijan;Bosnia and Herzegovina;Barbados;Bangladesh;Belgium;Burkina Faso;Bulgaria;Bahrain;Burundi;Benin;Bermuda;Brunei Darussalam;Bolivia;Brazil;Bahamas;Bhutan;Bouvet Island;Botswana;Belarus;Belize;Canada;Cocos (Keeling) Islands;Congo, The Democratic Republic of the;Central African Republic;Congo;Switzerland;Cote D''Ivoire;Cook Islands;Chile;Cameroon;China;Colombia;Costa Rica;Cuba;Cape Verde;Christmas Island;Cyprus;Czech Republic;Germany;Djibouti;Denmark;Dominica;Dominican Republic;Algeria;Ecuador;Estonia;Egypt;Western Sahara;Eritrea;Spain;Ethiopia;Finland;Fiji;Falkland Islands (Malvinas);Micronesia, Federated States of;Faroe Islands;France;France, Metropolitan;Gabon;United Kingdom;Grenada;Georgia;French Guiana;Ghana;Gibraltar;Greenland;Gambia;Guinea;Guadeloupe;Equatorial Guinea;Greece;South Georgia and the South Sandwich Islands;Guatemala;Guam;Guinea-Bissau;Guyana;Hong Kong;Heard Island and McDonald Islands;Honduras;Croatia;Haiti;Hungary;Indonesia;Ireland;Israel;India;British Indian Ocean Territory;Iraq;Iran, Islamic Republic of;Iceland;Italy;Jamaica;Jordan;Japan;Kenya;Kyrgyzstan;Cambodia;Kiribati;Comoros;Saint Kitts and Nevis;Korea, Democratic People''s Republic of;Korea, Republic of;Kuwait;Cayman Islands;Kazakstan;Lao People''s Democratic Republic;Lebanon;Saint Lucia;Liechtenstein;Sri Lanka;Liberia;Lesotho;Lithuania;Luxembourg;Latvia;Libyan Arab Jamahiriya;Morocco;Monaco;Moldova, Republic of;Madagascar;Marshall Islands;Macedonia;Mali;Myanmar;Mongolia;Macau;Northern Mariana Islands;Martinique;Mauritania;Montserrat;Malta;Mauritius;Maldives;Malawi;Mexico;Malaysia;Mozambique;Namibia;New Caledonia;Niger;Norfolk Island;Nigeria;Nicaragua;Netherlands;Norway;Nepal;Nauru;Niue;New Zealand;Oman;Panama;Peru;French Polynesia;Papua New Guinea;Philippines;Pakistan;Poland;Saint Pierre and Miquelon;Pitcairn Islands;Puerto Rico;Palestinian Territory;Portugal;Palau;Paraguay;Qatar;Reunion;Romania;Russian Federation;Rwanda;Saudi Arabia;Solomon Islands;Seychelles;Sudan;Sweden;Singapore;Saint Helena;Slovenia;Svalbard and Jan Mayen;Slovakia;Sierra Leone;San Marino;Senegal;Somalia;Suriname;Sao Tome and Principe;El Salvador;Syrian Arab Republic;Swaziland;Turks and Caicos Islands;Chad;French Southern Territories;Togo;Thailand;Tajikistan;Tokelau;Turkmenistan;Tunisia;Tonga;East Timor;Turkey;Trinidad and Tobago;Tuvalu;Taiwan;Tanzania, United Republic of;Ukraine;Uganda;United States Minor Outlying Islands;United States;Uruguay;Uzbekistan;Holy See (Vatican City State);Saint Vincent and the Grenadines;Venezuela;Virgin Islands, British;Virgin Islands, U.S.;Vietnam;Vanuatu;Wallis and Futuna;Samoa;Yemen;Mayotte;Serbia and Montenegro;South Africa;Zambia;Zaire;Zimbabwe;Anonymous Proxy;Satellite Provider'

INSERT INTO [dbo].[Countries]
(
	[CountryId]
	,[CountryCode]
	,[CountryName]
)
SELECT 
		a.ID AS CountryId,	
		LTRIM(RTRIM(a.Value)) as CountryCode,
		LTRIM(RTRIM(b.Value)) as CountryName
FROM 
	[TabulateCSV]  (@CSV) a
INNER JOIN [TabulateCharSV]  (@CSV2, ';') b ON
	a.ID = b.ID

--ADD ARTICLE DOCUMENT TYPES

INSERT INTO [dbo].[ArticleDocTypes]
(
	[ArtDocTypeDescription], 
	[Active], 
	[CreationUserId], 
	[CreationDateTime], 
	[UpdateUserId], 
	[UpdateDateTime],
	[PubliclyAvailable]
)
VALUES
(	
	'Abstract'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
	,1
)

INSERT INTO [dbo].[ArticleDocTypes]
(
	[ArtDocTypeDescription], 
	[Active], 
	[CreationUserId], 
	[CreationDateTime], 
	[UpdateUserId], 
	[UpdateDateTime],
	[PubliclyAvailable]
)
VALUES
(	
	'Full-Text'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
	,0
)

INSERT INTO [dbo].[ArticleDocTypes]
(
	[ArtDocTypeDescription], 
	[Active], 
	[CreationUserId], 
	[CreationDateTime], 
	[UpdateUserId], 
	[UpdateDateTime],
	[PubliclyAvailable]
)
VALUES
(	
	'Supplementary Data'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
	,0
)


--ADD Issue Doc Types

INSERT INTO [dbo].[IssueDocTypes]
(
	[IssueDocTypeDescription], 
	[Active], 
	[CreationUserId], 
	[CreationDateTime], 
	[UpdateUserId], 
	[UpdateDateTime],
	[PubliclyAvailable]
)
VALUES
(
	'Table of Contents'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
	,1
)

INSERT INTO [dbo].[IssueDocTypes]
(
	[IssueDocTypeDescription], 
	[Active], 
	[CreationUserId], 
	[CreationDateTime], 
	[UpdateUserId], 
	[UpdateDateTime],
	[PubliclyAvailable]
)
VALUES
(
	'Manuscripts in Press'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
	,1
)

INSERT INTO [dbo].[IssueDocTypes]
(
	[IssueDocTypeDescription], 
	[Active], 
	[CreationUserId], 
	[CreationDateTime], 
	[UpdateUserId], 
	[UpdateDateTime],
	[PubliclyAvailable]
)
VALUES
(
	'Authors'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
	,1
)

INSERT INTO [dbo].[IssueDocTypes]
(
	[IssueDocTypeDescription], 
	[Active], 
	[CreationUserId], 
	[CreationDateTime], 
	[UpdateUserId], 
	[UpdateDateTime],
	[PubliclyAvailable]
)
VALUES
(
	'Full Issue'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
	,0
)


--ADD PASSWORD RECOVERY QUESTIONS

INSERT INTO [dbo].[PasswordRecoveryQuestions]
(	
	[Description], 
	[Active], 
	[CreationUserId], 
	[CreationDateTime], 
	[UpdateUserId], 
	[UpdateDateTime]
)
VALUES
(
	'Mother''s Maiden Name'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
)

INSERT INTO [dbo].[PasswordRecoveryQuestions]
(	
	[Description], 
	[Active], 
	[CreationUserId], 
	[CreationDateTime], 
	[UpdateUserId], 
	[UpdateDateTime]
)
VALUES
(
	'City of Birth'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
)

INSERT INTO [dbo].[PasswordRecoveryQuestions]
(	
	[Description], 
	[Active], 
	[CreationUserId], 
	[CreationDateTime], 
	[UpdateUserId], 
	[UpdateDateTime]
)
VALUES
(
	'Name of Favorite Teacher/Professor'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
)

INSERT INTO [dbo].[PasswordRecoveryQuestions]
(	
	[Description], 
	[Active], 
	[CreationUserId], 
	[CreationDateTime], 
	[UpdateUserId], 
	[UpdateDateTime]
)
VALUES
(
	'Name of First School'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
)

--ADD ROLES

INSERT INTO [dbo].[Roles]
(
	[RoleDescription], 
	[Active], 
	[CreationUserId], 
	[CreationDateTime], 
	[UpdateUserId], 
	[UpdateDateTime]
)
VALUES
(	
	'Member'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
)

INSERT INTO [dbo].[Roles]
(
	[RoleDescription], 
	[Active], 
	[CreationUserId], 
	[CreationDateTime], 
	[UpdateUserId], 
	[UpdateDateTime]
)
VALUES
(	
	'Admin'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
)
GO


-- =============================================
-- Script Template
-- =============================================
DECLARE @USRIDENTITY INT
DECLARE @ADDRIDENTITY INT

--INSER ADMIN USER
INSERT INTO [dbo].[Users]
(
	    [Email]
           ,[PasswordHash]
	   ,[PasswordSalt]
           ,[FirstName]
           ,[LastName]
           ,[MiddleInitial]
           ,[SecretQuestion1Id]
           ,[SecretQuestion2Id]
           ,[SecretAnswer1Hash]
           ,[SecretAnswer2Hash]
           ,[AccountTypeId]
           ,[AccountStatus]
           ,[Active]
           ,[CreationUserId]
           ,[CreationDateTime]
           ,[UpdateUserId]
           ,[UpdateDateTime])
     VALUES
           (
	    'mnagisetty@yahoo.com'
           ,'A0D78BA0445642CBD00CC4811F0D234A'
	   ,'tiv816EPXJi6iw=='           
           ,'Monish'
           ,'Nagisetty'
	   ,''
           ,1
           ,2
           ,'5265CEB37D775551F3B1F0F9F7D960CD'
           ,'3B3D42C6E256AA0D547814313137C65F'
           ,1
           ,1
           ,1
           ,1
           ,GETDATE()
           ,1
           ,GETDATE()
)

SELECT @USRIDENTITY = @@IDENTITY

INSERT INTO [dbo].[Addresses]
(
	[Line1], 
	[Line2], 
	[City], 
	[StateProvince], 
	[CountryId], 
	[Phone], 
	[Fax], 
	[Active], 
	[CreationUserId], 
	[CreationDateTime], 
	[UpdateUserId], 
	[UpdateDateTime]
)
VALUES
(	
	'123 Some st.' 
	,''
	,'Some Town'
	,'OH'
	,225
	,613-333-3333
	,''
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
)

SELECT @ADDRIDENTITY = @@IDENTITY


INSERT INTO [dbo].[UserAddresses]
(
	[UserId], 
	[AddressId], 
	[AddressTypeId]
)
VALUES
(
	 @USRIDENTITY 
	,@ADDRIDENTITY 
	,1
)


INSERT INTO [dbo].[UserRoles]
(
	[RoleId], 
	[UserId]
)
VALUES
(	
	1
	,@USRIDENTITY
)

INSERT INTO [dbo].[UserRoles]
(
	[RoleId], 
	[UserId]
)
VALUES
(	
	2
	,@USRIDENTITY
)


--INSER REGULAR USER
INSERT INTO [dbo].[Users]
(
	    [Email]
           ,[PasswordHash]
	   ,[PasswordSalt]
           ,[FirstName]
           ,[LastName]
           ,[MiddleInitial]
           ,[SecretQuestion1Id]
           ,[SecretQuestion2Id]
           ,[SecretAnswer1Hash]
           ,[SecretAnswer2Hash]
           ,[AccountTypeId]
           ,[AccountStatus]
           ,[Active]
           ,[CreationUserId]
           ,[CreationDateTime]
           ,[UpdateUserId]
           ,[UpdateDateTime])
     VALUES
           (
	    'm_nagisetty@yahoo.com'
           ,'A0D78BA0445642CBD00CC4811F0D234A'
	   ,'tiv816EPXJi6iw=='           
           ,'Monish'
           ,'Nagisetty'
	   ,''
           ,1
           ,2
           ,'5265CEB37D775551F3B1F0F9F7D960CD'
           ,'3B3D42C6E256AA0D547814313137C65F'
           ,1
           ,1
           ,1
           ,1
           ,GETDATE()
           ,1
           ,GETDATE()
)

SELECT @USRIDENTITY = @@IDENTITY

INSERT INTO [dbo].[Addresses]
(
	[Line1], 
	[Line2], 
	[City], 
	[StateProvince], 
	[CountryId], 
	[Phone], 
	[Fax], 
	[Active], 
	[CreationUserId], 
	[CreationDateTime], 
	[UpdateUserId], 
	[UpdateDateTime]
)
VALUES
(	
	'123 Some st.' 
	,''
	,'Some Town'
	,'OH'
	,225
	,613-333-3333
	,''
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
)

SELECT @ADDRIDENTITY = @@IDENTITY


INSERT INTO [dbo].[UserAddresses]
(
	[UserId], 
	[AddressId], 
	[AddressTypeId]
)
VALUES
(
	 @USRIDENTITY 
	,@ADDRIDENTITY 
	,1
)


INSERT INTO [dbo].[UserRoles]
(
	[RoleId], 
	[UserId]
)
VALUES
(	
	1
	,@USRIDENTITY
)

GO

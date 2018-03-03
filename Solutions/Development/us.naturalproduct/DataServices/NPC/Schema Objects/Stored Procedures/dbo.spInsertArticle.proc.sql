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



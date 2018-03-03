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



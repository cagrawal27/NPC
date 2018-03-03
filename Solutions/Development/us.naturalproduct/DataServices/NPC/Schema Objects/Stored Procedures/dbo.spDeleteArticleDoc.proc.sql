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



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



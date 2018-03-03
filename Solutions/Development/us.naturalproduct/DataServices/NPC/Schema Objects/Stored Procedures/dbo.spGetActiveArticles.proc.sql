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



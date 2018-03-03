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



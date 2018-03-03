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



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



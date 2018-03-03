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



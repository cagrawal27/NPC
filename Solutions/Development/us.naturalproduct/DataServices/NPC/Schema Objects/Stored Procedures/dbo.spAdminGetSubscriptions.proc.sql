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



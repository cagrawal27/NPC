-- =============================================
-- Author:  Monish Nagisetty
-- Created: 01/04/2006
-- Modified: 01/04/2006
-- Description:	Gets all the issue documents for
-- the current issue and based on the user's 
-- subscriptions.
-- =============================================
CREATE PROCEDURE [dbo].[spGetIssueDocumentsBySubscription] 
(
	 @UserId			int
	,@VolumeIssueId		int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR				int,
			@HasSubscription	int
				
	--Returns 0 or more depending on whether
	--the user has a valid subscription
	SELECT @HasSubscription = [dbo].[IsUserIssueSubscriber](@UserId, @VolumeIssueId, GETDATE())

	--Returns a list of all issue documents for the current
	--issue
	SELECT
		idt.IssueDocTypeDescription
		,d.DocId
		,d.FileSizeKB
	FROM
		Documents d
	INNER JOIN IssueDocuments idoc ON
		d.DocId = idoc.DocId
	INNER JOIN IssueDocTypes idt ON
		idoc.IssueDocTypeId = idt.IssueDocTypeId
	WHERE
		idoc.VolumeIssueId = @VolumeIssueId
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
				idt.PubliclyAvailable = 1					
			)
		)		
	ORDER BY
		idt.IssueDocTypeDescription

	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
		GOTO FAIL
	ELSE
		RETURN 0


FAIL:
 BEGIN
	RAISERROR('An error occured while retrieving issue documents.', 16, 1)	
	RETURN -1
 END


END



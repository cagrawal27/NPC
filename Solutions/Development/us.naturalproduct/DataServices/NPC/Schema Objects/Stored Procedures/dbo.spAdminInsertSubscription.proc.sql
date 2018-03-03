-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/21/2006

--  Author: Parag Jagdale
-- ALTER date: 01/03/2007
-- Description:	Inserts a subscription into the database
-- =============================================
CREATE  PROCEDURE [dbo].[spAdminInsertSubscription]
(
	 @UserId			int
	,@VolumeIssueId		int
	,@ArticleId			int 
	,@EffectiveDate		datetime
	,@ExpirationDate	datetime
	,@Active			bit
	,@CreationUserId	int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int

	INSERT INTO [dbo].[Subscription]
	(
		[UserId], 
		[VolumeIssueId],
		[ArticleId],
		[EffectiveDate], 
		[ExpirationDate], 
		[Active], 
		[CreationUserId], 
		[CreationDateTime], 
		[UpdateUserId], 
		[UpdateDateTime]


	)
	VALUES
	(
		 @UserId
		,@VolumeIssueId
		,@ArticleId			
		,@EffectiveDate
		,@ExpirationDate
		,@Active
		,@CreationUserId
		,GETDATE()
		,@CreationUserId
		,GETDATE()
	)

	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
		GOTO FAIL

	RETURN 0

FAIL:
 BEGIN
	RAISERROR('An error occured while adding a subscription.', 16, 1)	
	RETURN -1
 END


END



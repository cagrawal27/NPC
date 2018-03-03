SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

/****** Object:  Stored Procedure dbo.spAdminUpdateSubscription    Script Date: 12/29/2006 10:39:38 PM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spAdminUpdateSubscription]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spAdminUpdateSubscription]
GO



-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/21/2006

--  Author: Parag Jagdale
-- ALTER date: 01/03/2007
-- Description:	updates subscription using SubscriptionId 
--		to select the correct row
-- =============================================
CREATE PROCEDURE [dbo].[spAdminUpdateSubscription]
(
	 @SubscriptionId		int
	,@VolumeIssueId		int
	,@ArticleId			int
	,@UserId			int
	,@EffectiveDate		datetime
	,@ExpirationDate	datetime
	,@Active			bit
	,@UpdateUserId		int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int

	/*
	TODO:  Modify this update statement to include the new parameters above
		VolumeIssueId
		ArticleId
	*/
	UPDATE [dbo].[Subscription]
	SET 
 		 [VolumeIssueId]	= @VolumeIssueId
		,[ArticleId]		= @ArticleId
		,[UserId]			= @UserId
		,[EffectiveDate]	= @EffectiveDate
		,[ExpirationDate]	= @ExpirationDate
		,[Active]			= @Active
		,[UpdateUserId]		= @UpdateUserId
		,[UpdateDateTime]	= GETDATE()
	WHERE
		SubscriptionId = @SubscriptionId

	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
		GOTO FAIL

	RETURN 0

FAIL:
 BEGIN
	RAISERROR('An error occured while updating a subscription.', 16, 1)	
	RETURN -1
 END


END




GO


SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

/****** Object:  Stored Procedure dbo.spAdminDeleteSubscription    Script Date: 12/29/2006 10:39:37 PM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spAdminDeleteSubscription]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spAdminDeleteSubscription]
GO




-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/21/2006
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[spAdminDeleteSubscription]
(
	 @SubscriptionId	int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int

	DELETE FROM [dbo].[Subscription]
	WHERE
		SubscriptionId = @SubscriptionId


	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
		GOTO FAIL

	RETURN 0

FAIL:
 BEGIN
	RAISERROR('An error occured while deleting a subscription.', 16, 1)	
	RETURN -1
 END


END















GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


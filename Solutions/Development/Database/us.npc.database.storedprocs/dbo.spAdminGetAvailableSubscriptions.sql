SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spAdminGetAvailableSubscriptions]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spAdminGetAvailableSubscriptions]
GO


/*=============================================
Author:  Monish Nagisetty
Created: 01/14/2007
Description:	This procedure returns the 
available subscriptions.
===============================================
Revision History
=============================================================================
Change   Initial Date        Description
    ID        AA mm/dd/yyyy  Describe reason for change.                            
=============================================================================*/
CREATE PROCEDURE [dbo].[spAdminGetAvailableSubscriptions]
(
	@UserId			int,
	@VolumeIssueId		int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int
	
	SELECT
		 a.ArticleId,
		CAST([PageNumber] AS VARCHAR)+' - '+LEFT(Title, 20)+'...' AS Title
	FROM
		Articles a
	INNER JOIN IssueArticles ia ON
		ia.ArticleId = a.ArticleId
	WHERE
		ia.VolumeIssueId	= @VolumeIssueId
		AND
		a.ArticleId NOT IN
			(
				SELECT 
					s.ArticleId
				FROM
					Subscription s
				WHERE
					s.VolumeIssueId		= @VolumeIssueId
					AND
					s.UserId			= @UserId	
			)
				
	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
		GOTO FAIL
	ELSE
		RETURN 0

FAIL:
 BEGIN
	RAISERROR('An error occured while retrieving the user''s available subscriptions.', 16, 1)	
	RETURN -1
 END


END





GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


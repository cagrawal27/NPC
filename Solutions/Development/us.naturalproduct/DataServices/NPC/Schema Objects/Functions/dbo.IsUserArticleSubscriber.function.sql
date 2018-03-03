/*=============================================
Author:  Monish Nagisetty
Created: 03/22/2006
Description:	This procedure returns user 
information.
===============================================
Revision History
=============================================================================
Change   Initial Date        Description
    ID        AA mm/dd/yyyy  Describe reason for change.                            
=============================================================================*/
CREATE FUNCTION dbo.IsUserArticleSubscriber
(
	 @UserId			int
	,@VolumeIssueId		int
	,@ArticleId			int
	,@CurrentDate		datetime
)  
RETURNS int
AS
BEGIN 
	DECLARE @IsUserSubscriber int
	SET @IsUserSubscriber = 0

	--Returns 0 or more depending on whether
	--the user has a valid subscription
	SELECT
		@IsUserSubscriber = COUNT(*)
	FROM
		Subscription s
	INNER JOIN Users u on 
		s.UserId = u.UserId
	WHERE
		u.UserId		= @UserId
		AND
		u.AccountStatus = 1 -- Account Active
		AND	
		u.Active		= 1 --User should be active
		AND 
		s.Active		= 1	--Subscription should be active
		AND
		s.VolumeIssueId = @VolumeIssueId -- User has access to issue
		AND
		s.ArticleId		= @ArticleId -- User has access to article
		AND
		DATEDIFF(dd, s.EffectiveDate, @CurrentDate) >= 0
		AND
		DATEDIFF(DD, s.ExpirationDate, @CurrentDate) <= 0		

	RETURN (@IsUserSubscriber)
END



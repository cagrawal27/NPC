/*=============================================
Author:  Monish Nagisetty
Created: 03/22/2006
Description:	This procedure determines if a
user is subscribed to all the articles in a 
issue.
===============================================
Revision History
=============================================================================
Change   Initial Date        Description
    ID        AA mm/dd/yyyy  Describe reason for change.                            
=============================================================================*/
CREATE FUNCTION dbo.IsUserIssueSubscriber
(
	 @UserId			int
	,@VolumeIssueId		int
	,@CurrentDate		datetime
)  
RETURNS int
AS
BEGIN 
	DECLARE @SubscriptionCount	int
	DECLARE @ArticleCount		int
	DECLARE @IsUserSubscriber	int
	SET @IsUserSubscriber = 0

	--Return total subscriptions for this issue
	SELECT DISTINCT
		@SubscriptionCount = COUNT(*)
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
		s.Active		= 1 --Subscription should be active
		AND
		s.VolumeIssueId = @VolumeIssueId --
		AND
		DATEDIFF(dd, s.EffectiveDate, @CurrentDate) >= 0
		AND
		DATEDIFF(DD, s.ExpirationDate, @CurrentDate) <= 0		

	--Return total articles for this issue
	SELECT
		@ArticleCount = COUNT(*)
	FROM
		Articles a
	INNER JOIN IssueArticles ia ON
		ia.ArticleId = a.ArticleId
	WHERE
		ia.VolumeIssueId = @VolumeIssueId
		AND
		a.Active = 1

	IF (@SubscriptionCount >= @ArticleCount)
		SET @IsUserSubscriber = 1
	ELSE
		SET @IsUserSubscriber = 0

	RETURN (@IsUserSubscriber)
END



-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/17/2006
-- Description:	This procedure returns the password
--				and salt for a given user.
-- =============================================
CREATE PROCEDURE [dbo].[spAdminGetUserInfo] 
(
	@UserId		int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int

	SELECT 
		u.[UserId], 
		u.[Email], 
		u.[FirstName], 
		u.[LastName], 
		u.[MiddleInitial], 
		--u.[AccountTypeId], 
		acTypes.[Description] as AcctTypeDescription,
		--u.[AccountStatus], 
		acct.[AccountStatusDescription] as AcctStatusDescription,
		u.[Active]
	FROM 
		[dbo].[Users] u
	INNER JOIN AccountStatus acct ON
		acct.AccountStatusID = u.AccountStatus
	INNER JOIN AccountTypes acTypes ON
		acTypes.AccountTypeId = u.AccountTypeId
	WHERE
		UserId = @UserId

	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
		GOTO FAIL
	ELSE
		RETURN 0	

	FAIL:
	 BEGIN
		--AN ERROR OCCURED
		RAISERROR('Failed to get user information.', 16, 1)
		RETURN -1
	 END
END



-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/19/2006
-- Description:	
-- =============================================
CREATE  PROCEDURE [dbo].[spGetAllUsersDetailed]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int

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

	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
	 BEGIN
		RETURN 0
	 END
	ELSE
	 BEGIN
		RAISERROR('An error occured while getting all users.', 16, 1)
		RETURN -1
	 END

END



-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/17/2006
-- Description:	Inserts a user's address into 
--				the user's table
-- =============================================
CREATE PROCEDURE [dbo].[spInsertUserRole] 
(
	 @UserId	int
	,@RoleId	int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int

	INSERT INTO [dbo].[UserRoles]
	(
		[RoleId], 
		[UserId]
	)
	VALUES
	(
		 @RoleId
		,@UserId
	)

	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
		GOTO FAIL
	ELSE
		GOTO SUCCESS

	SUCCESS:
		RETURN 0

	FAIL:
	 BEGIN
		RAISERROR('Failed to add role for new user.', 16, 1)
		RETURN -1
	 END
END



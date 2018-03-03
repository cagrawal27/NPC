-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 04/17/2007
-- Description:	This procedure returns the password
--				and salt for a given user.
-- =============================================
CREATE  PROCEDURE [dbo].[spGetUserRolesByUserId] 
(
	@UserId		int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int,
		@RC		int

	SELECT
		RoleId
	FROM
		UserRoles ur
	INNER JOIN Users u ON
		u.UserId = ur.UserId
	WHERE
		u.UserId = @UserId

	SELECT @ERROR = @@ERROR, @RC = @@ROWCOUNT

	IF (@ERROR <> 0 OR @RC <> 1)
		GOTO FAIL
	ELSE
		RETURN 0	

	FAIL:
	 BEGIN
		--AN ERROR OCCURED
		RAISERROR('An error occured while retrieving a UserRoles record.', 10, 1)
		RETURN -1
	 END
END



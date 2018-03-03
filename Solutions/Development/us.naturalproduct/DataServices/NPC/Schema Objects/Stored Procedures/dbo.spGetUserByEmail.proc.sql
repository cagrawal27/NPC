-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 04/17/2007
-- Description:	This procedure returns the password
--				and salt for a given user.
-- =============================================
CREATE   PROCEDURE [dbo].[spGetUserByEmail] 
(
	@Email	varchar(50)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int,
		@RC		int

	SELECT 
		[UserId], 
		[Email], 
		[PasswordHash], 
		[PasswordSalt], 
		[FirstName], 
		[LastName], 
		[MiddleInitial], 
		[SecretQuestion1Id], 
		[SecretQuestion2Id], 
		[SecretAnswer1Hash], 
		[SecretAnswer2Hash], 
		[AccountTypeId], 
		[AccountStatus], 
		[Active], 
		[CreationUserId], 
		[CreationDateTime], 
		[UpdateUserId], 
		[UpdateDateTime] 
	FROM 
		[dbo].[Users]
	WHERE 
		Email = @Email

	SELECT @ERROR = @@ERROR, @RC = @@ROWCOUNT

	IF (@ERROR <> 0 OR @RC <> 1)
		GOTO FAIL
	ELSE
		RETURN 0	

	FAIL:
	 BEGIN
		--AN ERROR OCCURED
		RAISERROR('An error occured or returned more than one record when retrieving a User record.', 10, 1)
		RETURN -1
	 END
END



-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/20/2006
-- Description:	
-- =============================================
CREATE  PROCEDURE [dbo].[spUpdateUserPasswordHash]
(
	@Email		varchar(50),
	@PasswordHash	varchar(100),
	@AccountStatus	int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int,
		@RC	int

	UPDATE [dbo].[Users]
	SET 
		[PasswordHash] = @PasswordHash,
		[AccountStatus] = @AccountStatus
	WHERE 
		Email 	= @Email

	SELECT @ERROR = @@ERROR, @RC = @@ROWCOUNT

	IF (@ERROR = 0 AND @RC = 1)
	 BEGIN
		RETURN 0
	 END
	ELSE
	 BEGIN
		RETURN -1
	 END
END



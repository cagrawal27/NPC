-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 09/30/2006
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateUserPasswordRecoveryInfo]
(
	@Email			varchar(50),
	@SecretQuestion1Id	int,
	@SecretQuestion2Id	int,
	@SecretAnswer1Hash	varchar(200),
	@SecretAnswer2Hash	varchar(200)
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
		SecretQuestion1Id = @SecretQuestion1Id,
		SecretQuestion2Id = @SecretQuestion2Id,
		SecretAnswer1Hash = @SecretAnswer1Hash,
		SecretAnswer2Hash = @SecretAnswer2Hash
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



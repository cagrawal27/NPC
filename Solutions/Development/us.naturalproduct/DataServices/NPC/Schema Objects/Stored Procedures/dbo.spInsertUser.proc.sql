-- =============================================
-- Author:		Monish Nagisetty
-- ALTER  date: 03/03/2006
-- Description:	Inserts a record into the Users table.
-- =============================================
CREATE PROCEDURE [dbo].[spInsertUser] 
(
	@Email			varchar(50),
	@PasswordHash		varchar(100),
	@PasswordSalt		varchar(50),
	@FirstName		varchar(50),
	@LastName		varchar(50),
	@MiddleInitial		varchar(5),
	@SecretQuestion1Id	int,
	@SecretQuestion2Id	int,
	@SecretAnswer1Hash	varchar(200),
	@SecretAnswer2Hash	varchar(200),
	@AccountTypeId		int,
	@AccountStatus		int,
	@Active			bit,
	@CreationUserId		int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int,
			@RC			int,
			@IDENTITY	int

	INSERT INTO [dbo].[Users]
	(
		 [Email]
		,[PasswordHash]
		,[PasswordSalt]
		,[FirstName]
		,[LastName]
		,[MiddleInitial]
		,[SecretQuestion1Id]
		,[SecretQuestion2Id]
		,[SecretAnswer1Hash]
		,[SecretAnswer2Hash]
		,[AccountTypeId]
		,[AccountStatus]
		,[Active]
		,[CreationUserId]
		,[CreationDateTime]
		,[UpdateUserId]
		,[UpdateDateTime]
	)
	VALUES
	(
		@Email,
		@PasswordHash,
		@PasswordSalt,
		@FirstName,
		@LastName,
		@MiddleInitial,
		@SecretQuestion1Id,
		@SecretQuestion2Id,
		@SecretAnswer1Hash, 
		@SecretAnswer2Hash,
		@AccountTypeId,
		@AccountStatus,
		@Active,
		@CreationUserId,
		GETDATE(),
		@CreationUserId,
		GETDATE()
	)

	SELECT @ERROR = @@ERROR, @IDENTITY = @@IDENTITY

	IF (@ERROR = 0)
	 BEGIN
		RETURN @IDENTITY
	 END
	ELSE
	 BEGIN
		RAISERROR(@ERROR, 16, 1)
		RETURN -1
	 END

END



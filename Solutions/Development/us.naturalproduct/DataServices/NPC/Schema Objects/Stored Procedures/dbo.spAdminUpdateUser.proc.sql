-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/20/2006
-- Description:	
-- =============================================
CREATE  PROCEDURE [dbo].[spAdminUpdateUser]
(
	 @UserId		int
	,@FirstName		varchar(50)
	,@LastName		varchar(50)
	,@MiddleInitial		varchar(5)
	,@AccountTypeId		int
	,@AccountStatus		int
	,@Active		bit
	,@UpdateUserId		int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int,
		@RC	int

	BEGIN TRAN
	
	UPDATE [dbo].[Users]
	SET 
		 [FirstName]		= @FirstName
		,[LastName]		= @LastName
		,[MiddleInitial]	= @MiddleInitial
		,[AccountTypeId]	= @AccountTypeId
		,[AccountStatus]	= @AccountStatus
		,[Active]		= @Active
		,[UpdateUserId]		= @UpdateUserId
		,[UpdateDateTime] 	= GETDATE()
	WHERE 
		UserId 	= @UserId

	SELECT @ERROR = @@ERROR, @RC = @@ROWCOUNT

	IF (@ERROR = 0 AND @RC = 1)
	 BEGIN
		COMMIT TRAN
		RETURN 0
	 END
	ELSE
	 BEGIN
		ROLLBACK TRAN
		RETURN -1
	 END
END



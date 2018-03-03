-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/09/2006
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateIssueDetails]
(
	 @IssueId	int
	,@IssueName	varchar(50)
	,@Active	bit
	,@UpdateUserId	int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int

	BEGIN TRAN

	UPDATE [dbo].[Issues]
		SET 
		 [IssueName] 		= @IssueName
		,[Active]		= @Active
		,[UpdateUserId]		= @UpdateUserId
		,[UpdateDateTime]	= GETDATE()
	WHERE 
		IssueId = @IssueId

	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
	 BEGIN
		COMMIT TRAN
		RETURN 0
	 END
	ELSE
	 BEGIN
		ROLLBACK TRAN
		RAISERROR('An error occured while updating issue details.', 16, 1)
		RETURN -1
	 END
END



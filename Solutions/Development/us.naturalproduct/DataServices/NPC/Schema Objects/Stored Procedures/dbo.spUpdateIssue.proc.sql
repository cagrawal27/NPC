-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/09/2006
-- Description:	
-- =============================================
CREATE  PROCEDURE [dbo].[spUpdateIssue]
(
	 @VolumeIssueId		int
	,@IssueName		varchar(50)
	,@Active		bit
	,@UpdateUserId		int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int

	UPDATE [dbo].[Issues] 
	SET 
		 [IssueName] 		= @IssueName
		,[Active]		= @Active
		,[UpdateUserId]		= @UpdateUserId
		,[UpdateDateTime]	= GETDATE()
	FROM
		VolumeIssues vi
	WHERE 
		vi.IssueId 	= Issues.IssueId
		AND
		VolumeIssueId 	= @VolumeIssueId

	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
		RETURN 0
	ELSE
	 BEGIN
		RAISERROR('An error occured while updating issue details.', 16, 1)
		RETURN -1
	 END
END



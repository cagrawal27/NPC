-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/19/2006
-- Description:	
-- =============================================
CREATE   PROCEDURE [dbo].[spInsertIssue]
(
	 @VolumeId		int
	,@IssueName		varchar(50)
	,@Active		bit
	,@CreationUserId	int
	,@VolumeIssueId		int 	output
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int,
		@IssueId 	int

	BEGIN TRAN

	--Insert the issue
	INSERT INTO [dbo].[Issues]
	(
		[IssueName], 
		[Active], 
		[CreationUserId], 
		[CreationDateTime], 
		[UpdateUserId], 
		[UpdateDateTime]
	)
	VALUES
	(
		 @IssueName
		,@Active
		,@CreationUserId
		,GETDATE()
		,@CreationUserId
		,GETDATE()
	)

	SELECT @ERROR = @@ERROR, @IssueId = @@IDENTITY

	IF (@ERROR <> 0)
		GOTO FAIL

	--Insert Issue into volume issue table
	INSERT INTO [dbo].[VolumeIssues]
	(
		[VolumeId], 
		[IssueId]
	)
	VALUES
	(
		 @VolumeId
		,@IssueId
	)

	SELECT @ERROR = @@ERROR, @VolumeIssueId = @@IDENTITY

	IF (@ERROR <> 0)
		GOTO FAIL
	ELSE
	 BEGIN
		COMMIT TRAN
		RETURN -1
	 END

FAIL:
 BEGIN
	ROLLBACK TRAN
	RAISERROR('An error occured while adding an issue.', 16, 1)
	RETURN -1	
 END
END



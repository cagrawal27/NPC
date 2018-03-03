-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/09/2006
-- Description:	Gets a list of all issues for
--		a volume
-- =============================================
CREATE   PROCEDURE [dbo].[spAdminGetIssue]
(
	@VolumeIssueId		int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int

	SELECT 
		i.[IssueId], 
		[IssueName], 
		[Active]
	FROM 
		[dbo].[Issues] i
	INNER JOIN VolumeIssues vi ON
		vi.IssueId = i.IssueId	
	WHERE
		vi.VolumeIssueId = @VolumeIssueId

	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
		GOTO FAIL

	EXEC dbo.spAdminGetIssueDocs @VolumeIssueId	

	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
		RETURN 0
	ELSE
		GOTO FAIL
	

FAIL:
 BEGIN
	RAISERROR('An error occured while getting issue details.', 16, 1)
	RETURN -1
 END


END



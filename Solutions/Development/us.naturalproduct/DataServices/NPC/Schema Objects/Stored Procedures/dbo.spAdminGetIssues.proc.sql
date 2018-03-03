-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/09/2006
-- Description:	Gets a list of all issues for
--		a volume
-- =============================================
CREATE    PROCEDURE [dbo].[spAdminGetIssues]
(
	@VolumeId	int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int

	SELECT 
		 vi.[VolumeIssueId]
		  ,[IssueName]
	FROM 
		[dbo].[Issues] i
	INNER JOIN VolumeIssues vi ON
		i.IssueId = vi.IssueId
	WHERE
		vi.VolumeId = @VolumeId

	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
	 BEGIN
		RETURN 0
	 END
	ELSE
	 BEGIN
		RAISERROR('An error occured while getting all issues.', 16, 1)
		RETURN -1
	 END


END



-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/09/2006
-- Description:	
-- =============================================
CREATE     PROCEDURE [dbo].[spGetAllIssuesDetailed]
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
		vi.VolumeIssueId,
		i.[IssueId], 
		[IssueName], 
		[Active], 
		(
			SELECT
				COUNT(*)
			FROM
				IssueDocuments idoc
			WHERE
				idoc.VolumeIssueId = vi.VolumeIssueId
		) AS Documents,
		(
			SELECT
				COUNT(*)
			FROM
				IssueArticles ia
			WHERE
				ia.VolumeIssueId = vi.VolumeIssueId
		) AS Articles			
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



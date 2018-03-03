-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/09/2006
-- Description:	Gets the volume and issue name.
-- =============================================
CREATE  PROCEDURE [dbo].[spGetVolumeAndIssueName]
(
	@VolumeIssueId	int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int

	--Return the Volume Name, Year and issue name
	SELECT
		 VolumeName
		,VolumeYear
		,IssueName	
	FROM
		 VolumeIssues vi
		,Volumes v
		,Issues i
	WHERE
		vi.VolumeIssueId = @VolumeIssueId
		AND
		vi.VolumeId = v.VolumeId
		AND
		vi.IssueId = i.IssueId
		AND
		v.Active = 1
		AND
		i.Active = 1

	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
	 BEGIN
		RETURN 0
	 END
	ELSE
	 BEGIN
		RAISERROR('An error occurred while retrieving the volume and issue name.', 16, 1)
		RETURN -1
	 END


END



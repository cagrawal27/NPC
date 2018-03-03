-- =============================================
-- Author:		Monish Nagisetty
-- ALTER  date: 03/09/2006
-- Description:	Gets a list of issues for the
--				a given volume.
-- =============================================
CREATE     PROCEDURE [dbo].[spGetActiveIssues]
(
	@VolumeId	int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int

	--Return the Volume Name
	SELECT
		VolumeName
		,VolumeYear
	FROM
		dbo.Volumes
	WHERE
		VolumeId = @VolumeId
		AND
		Active = 1

	--Return all active issues in the volume
	SELECT 
		 vi.[VolumeIssueId]
		,i.[IssueName]
	FROM 
		[dbo].[Issues] i
	INNER JOIN VolumeIssues vi ON
		vi.IssueId = i.IssueId
	WHERE
		i.Active = 1
		AND
		vi.VolumeId = @VolumeId

	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
	 BEGIN
		RETURN 0
	 END
	ELSE
	 BEGIN
		RAISERROR(@ERROR, 16, 1)
		RETURN -1
	 END


END



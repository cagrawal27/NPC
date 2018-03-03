-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/09/2006
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[spGetAllVolumesDetailed]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int

	SELECT 
		[VolumeId], 
		[VolumeName], 
		[VolumeYear], 
		[Active], 
		(
			SELECT
				COUNT(*)
			FROM
				VolumeIssues
			WHERE	
				VolumeId = v.VolumeId
		) AS Issues
	FROM 
		[dbo].[Volumes] v

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



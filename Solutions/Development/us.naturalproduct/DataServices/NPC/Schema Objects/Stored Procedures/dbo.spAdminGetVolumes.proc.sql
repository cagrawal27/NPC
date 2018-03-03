-- =============================================
-- Author:		Monish Nagisetty
-- ALTER  date: 03/09/2006
-- Description:	Gets a list of all volumes
-- =============================================
CREATE   PROCEDURE [dbo].[spAdminGetVolumes]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int

	SELECT 
		 [VolumeId]
		,[VolumeName]+' ('+[VolumeYear]+')' as 'VolumeName'
	FROM 
		[dbo].[Volumes]

	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
	 BEGIN
		RETURN 0
	 END
	ELSE
	 BEGIN
		RAISERROR('An error occured while getting all volumes.', 16, 1)
		RETURN -1
	 END


END



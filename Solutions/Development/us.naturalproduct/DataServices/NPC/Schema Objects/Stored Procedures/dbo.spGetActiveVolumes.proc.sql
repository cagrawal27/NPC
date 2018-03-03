-- =============================================
-- Author:		Monish Nagisetty
-- ALTER  date: 03/09/2006
-- Description:	Gets a list of active volumes
-- =============================================
CREATE   PROCEDURE [dbo].[spGetActiveVolumes]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int

	SELECT 
		 [VolumeId]
		,[VolumeName]
		,[VolumeYear]
	FROM 
		[dbo].[Volumes]
	WHERE
		Active = 1

	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
	 BEGIN
		RETURN 0
	 END
	ELSE
	 BEGIN
		RAISERROR('An error occured while getting active volumes.', 16, 1)
		RETURN -1
	 END


END



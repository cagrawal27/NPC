-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/09/2006
-- Description:	Gets the volume and issue name.
-- =============================================
CREATE PROCEDURE [dbo].[spGetVolume]
(
	@VolumeId	int
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
		,Active
		,CreationUserId
		,CreationDateTime
		,UpdateUserId
		,UpdateDateTime
	FROM
		Volumes
	WHERE
		VolumeId = @VolumeId

	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
	 BEGIN
		RETURN 0
	 END
	ELSE
	 BEGIN
		RAISERROR('An error occurred while retrieving a volume.', 16, 1)
		RETURN -1
	 END

END



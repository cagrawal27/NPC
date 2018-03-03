-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/19/2006
-- Description:	
-- =============================================
CREATE  PROCEDURE [dbo].[spUpdateVolume]
(
	 @VolumeId	int
	,@VolumeName	varchar(30)
	,@VolumeYear	varchar(10)
	,@Active	bit
	,@UpdateUserId	int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int

	BEGIN TRAN

	UPDATE [dbo].[Volumes]
	SET 
		 [VolumeName]		= @VolumeName
		,[VolumeYear]		= @VolumeYear
		,[Active]		= @Active
		,[UpdateUserId]		= @UpdateUserId
		,[UpdateDateTime]	= GETDATE()
	WHERE 
		VolumeId = @VolumeId

	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
	 BEGIN
		COMMIT TRAN
		RETURN 0
	 END
	ELSE
	 BEGIN
		ROLLBACK TRAN
		RAISERROR('An error occured while updating volume details.', 16, 1)
		RETURN -1
	 END
END



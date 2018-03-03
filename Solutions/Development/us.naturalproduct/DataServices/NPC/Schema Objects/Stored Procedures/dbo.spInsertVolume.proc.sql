-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 01/03/2009
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[spInsertVolume]
(
	 @VolumeName		varchar(30)
	,@VolumeYear		varchar(10)	
	,@Active			bit
	,@CreationUserId	int
	,@VolumeId			int 	output
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int

	--Insert the volume
	INSERT INTO [dbo].[Volumes]
    (
			[VolumeName]
           ,[VolumeYear]
           ,[Active]
           ,[CreationUserId]
           ,[CreationDateTime]
           ,[UpdateUserId]
           ,[UpdateDateTime])
     VALUES
           (@VolumeName
           ,@VolumeYear
           ,@Active
           ,@CreationUserId
           ,GETDATE()
           ,@CreationUserId
           ,GETDATE()
	)

	SELECT @ERROR = @@ERROR, @VolumeId = @@IDENTITY

	IF (@ERROR <> 0)
		GOTO FAIL
	ELSE
	 BEGIN
		RETURN -1
	 END

FAIL:
 BEGIN
	RAISERROR('An error occured while adding a volume.', 16, 1)
	RETURN -1	
 END
END



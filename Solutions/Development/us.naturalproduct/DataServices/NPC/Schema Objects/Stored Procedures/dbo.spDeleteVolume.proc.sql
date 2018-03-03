-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 01/03/2009
-- Description:	Deletes records from the documents
--		and issuedocuments tables
-- =============================================
CREATE PROCEDURE [dbo].[spDeleteVolume] 
(
	@VolumeId		int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int,
			@ERRMSG		varchar(50),
			@RC			int	

	SELECT
		@RC = COUNT(*)
	FROM
		VolumeIssues
	WHERE
		VolumeId = @VolumeId	

	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
	BEGIN
		SET @ERRMSG = 'An error occurred while determining the number of issues in the provided volume.'
		GOTO FAIL
	END

	IF (@RC > 0)
	BEGIN
		SET @ERRMSG = 'The selected volume cannot be deleted because it contains multiple active issues.'
		GOTO FAIL
	END
	ELSE
	BEGIN
		DELETE 
		FROM
			Volumes 
		WHERE
			VolumeId = @VolumeId
	
		SELECT @ERROR = @@ERROR
	END

	IF (@ERROR <> 0)
	BEGIN
		SET @ERRMSG = 'An error occurred when attempting to delete the selected volume.'
		GOTO FAIL
	END
	ELSE
		RETURN 0

FAIL:
 BEGIN
	RAISERROR(@ERRMSG, 16, 1)	
	RETURN -1
 END


END



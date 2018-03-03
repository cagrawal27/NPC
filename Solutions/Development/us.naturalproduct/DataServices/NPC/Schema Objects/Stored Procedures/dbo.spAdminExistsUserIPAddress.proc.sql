/****** Object:  Stored Procedure dbo.spAdminExistsUserIPAddress    Script Date: 4/10/2007 11:18:08 PM ******/

-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 04/10/2007
-- Description:	Checks if an IP address already
--		exists.
-- =============================================
CREATE    PROCEDURE [dbo].[spAdminExistsUserIPAddress] 
(
	@IPOctet1Begin	int,
	@IPOctet2Begin	int,
	@IPOctet3Begin	int,
	@IPOctet3End	int,
	@IPOctet4Begin	int,
	@IPOctet4End	int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int,
		@RC	int,
		@Exists	int
				
	SET @Exists = 0

	SELECT
		@RC = COUNT(*) 
	FROM
		UserIPAddresses
	WHERE
		IPOctet1Begin = @IPOctet1Begin
		AND
		IPOctet2Begin = @IPOctet2Begin
		AND
		(
			@IPOctet3Begin	BETWEEN IPOctet3Begin AND IPOctet3End	
			OR
			@IPOctet3End	BETWEEN IPOctet3Begin AND IPOctet3End				
			OR
			(
				@IPOctet3Begin < IPOctet3Begin
				AND
				@IPOctet3End > IPOctet3End
			)
		)
		AND
		(
			@IPOctet4Begin	BETWEEN IPOctet4Begin AND IPOctet4End	
			OR
			@IPOctet4End	BETWEEN IPOctet4Begin AND IPOctet4End				
			OR
			(
				@IPOctet4Begin < IPOctet4Begin
				AND
				@IPOctet4End > IPOctet4End
			)
		)
		
	SELECT @ERROR = @@ERROR
	
	IF (@ERROR = 0)
	BEGIN
		IF (@RC > 0)
			SET @Exists = 1		
	
		SELECT @EXISTS AS [Exists]

	END
	ELSE
		RAISERROR('Failed to determine if IP address exists.', 10, 1)

END



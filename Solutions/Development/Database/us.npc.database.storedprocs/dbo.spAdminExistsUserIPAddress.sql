/****** Object:  Stored Procedure dbo.spAdminExistsUserIPAddress    Script Date: 4/10/2007 11:18:08 PM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spAdminExistsUserIPAddress]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spAdminExistsUserIPAddress]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

/****** Object:  Stored Procedure dbo.spAdminExistsUserIPAddress    Script Date: 4/10/2007 11:18:08 PM ******/

-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 04/10/2007
-- Description:	Checks if an IP address already
--		exists.
-- =============================================
CREATE   PROCEDURE [dbo].[spAdminExistsUserIPAddress] 
(
	@IPOctet1Begin	int,
	@IPOctet2Begin	int,
	@IPOctet3Begin	int,
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
		IPOctet3Begin = @IPOctet3Begin
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
		RETURN -1

END












GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


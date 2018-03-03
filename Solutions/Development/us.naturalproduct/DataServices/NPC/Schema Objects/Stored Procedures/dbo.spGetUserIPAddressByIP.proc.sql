/****** Object:  Stored Procedure dbo.spGetUserIPAddressByIP   ****/

-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 04/10/2007
-- Description:	Checks if an IP address already
--		exists.
-- =============================================
CREATE   PROCEDURE [dbo].[spGetUserIPAddressByIP] 
(
	@IPOctet1	int,
	@IPOctet2	int,
	@IPOctet3	int,
	@IPOctet4	int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int,
		@UserId	int

	SELECT
		UserId 
	FROM
		UserIPAddresses
	WHERE
		IPOctet1Begin = @IPOctet1
		AND
		IPOctet2Begin = @IPOctet2
		AND
		@IPOctet3 BETWEEN IPOctet3Begin AND IPOctet3End
		AND
		@IPOctet4	BETWEEN IPOctet4Begin AND IPOctet4End	
		
	SELECT @ERROR = @@ERROR
	
	IF (@ERROR <> 0)
		RAISERROR('Failed to determine UserId from IP address', 10, 1)

END



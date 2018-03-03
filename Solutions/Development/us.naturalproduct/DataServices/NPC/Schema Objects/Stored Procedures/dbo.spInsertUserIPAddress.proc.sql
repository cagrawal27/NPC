-- =============================================
-- Author:		Monish Nagisetty
-- ALTER  date: 06/04/2006
-- Description:	Inserts a record into the Users table.
-- =============================================
CREATE PROCEDURE [dbo].[spInsertUserIPAddress] 
(
	@UserId		int,
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

	DECLARE @ERROR		int,
		@RC		int
		
	INSERT INTO [dbo].[UserIPAddresses]
	(
		[UserId], 
		[IPOctet1Begin], 
		[IPOctet2Begin], 
		[IPOctet3Begin], 
		[IPOctet3End],
		[IPOctet4Begin],
		[IPOctet4End]
	)
	VALUES
	( 
	   	@UserId,
		@IPOctet1Begin, 
		@IPOctet2Begin, 
		@IPOctet3Begin, 
		@IPOctet3End,
		@IPOctet4Begin,
		@IPOctet4End
	)
	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
		RETURN 0
	ELSE
		RETURN -1

END



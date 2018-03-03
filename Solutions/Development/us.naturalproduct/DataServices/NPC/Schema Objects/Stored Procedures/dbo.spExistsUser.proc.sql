-- =============================================
-- Author:		Monish Nagisetty
-- ALTER  date: 04/17/2007
-- Description:	Checks if a user exists with 
--				with the gaven email address.
-- =============================================
CREATE PROCEDURE [dbo].[spExistsUser] 
(
	@Email	varchar(50)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int,
		@RC		int,
		@EXISTS		int

	SELECT 
			@RC = COUNT(*)
	FROM
			dbo.Users
	WHERE
			Email = @Email

	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
		GOTO FAIL

	-- EMAIL DOES NOT EXIST
	IF (@RC = 0)	
	 BEGIN
		SET @EXISTS = 0			
	 END
	ELSE -- EMAIL ALREADY EXISTS
	 BEGIN
		SET @EXISTS = 1
	 END

	SELECT @EXISTS AS [Exists]

	FAIL:
	 BEGIN
		--AN ERROR OCCURED
		RAISERROR('Failed to determine if user exists.', 10, 1)
	 END

END



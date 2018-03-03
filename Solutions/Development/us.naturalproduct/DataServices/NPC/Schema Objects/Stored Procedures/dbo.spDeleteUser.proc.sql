﻿-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/19/2006
-- Description:	Deletes records from the documents
--		and issuedocuments tables
-- =============================================
CREATE  PROCEDURE [dbo].[spDeleteUser] 
(
	@UserId		int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int
		
	DELETE dbo.Addresses
	FROM
		UserAddresses ua
	WHERE
		ua.AddressId = Addresses.AddressId
		and
		ua.UserId = @UserId
		
	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
		GOTO FAIL

	DELETE FROM [dbo].[Users]
	WHERE 
		UserId = @UserId

	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
		GOTO FAIL
	ELSE
		RETURN 0

FAIL:
 BEGIN
	RAISERROR('An error occured deleting an user.', 16, 1)	
	RETURN -1
 END


END


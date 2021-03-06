/****** Object:  Stored Procedure dbo.spAdminInsertUserIPAddress    Script Date: 4/8/2007 10:50:25 PM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spAdminInsertUserIPAddress]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spAdminInsertUserIPAddress]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

/****** Object:  Stored Procedure dbo.spAdminInsertUserIPAddress    Script Date: 4/8/2007 10:50:25 PM ******/
-- =============================================
-- Author:		Monish Nagisetty
-- ALTER  date: 06/04/2006
-- Description:	Inserts a record into the Users table.
-- =============================================
CREATE  PROCEDURE [dbo].[spAdminInsertUserIPAddress] 
(
	@UserId		int,
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

	DECLARE @ERROR		int,
		@RC		int
		
	INSERT INTO [dbo].[UserIPAddresses]
	(
		[UserId], 
		[IPOctet1Begin], 
		[IPOctet2Begin], 
		[IPOctet3Begin], 
		[IPOctet4Begin],
		[IPOctet4End]
	)
	VALUES
	( 
	   	@UserId,
		@IPOctet1Begin, 
		@IPOctet2Begin, 
		@IPOctet3Begin, 
		@IPOctet4Begin,
		@IPOctet4End
	)
	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
		RETURN 0
	ELSE
		RETURN -1

END











GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


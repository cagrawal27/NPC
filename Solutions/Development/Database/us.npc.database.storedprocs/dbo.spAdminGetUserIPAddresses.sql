/****** Object:  Stored Procedure dbo.spAdminGetUserIPAddresses    Script Date: 4/15/2007 8:05:25 PM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spAdminGetUserIPAddresses]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spAdminGetUserIPAddresses]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

/****** Object:  Stored Procedure dbo.spAdminGetUserIPAddresses    Script Date: 4/15/2007 8:05:25 PM ******/




-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/17/2006
-- Description:	This procedure returns the password
--				and salt for a given user.
-- =============================================
CREATE  PROCEDURE [dbo].[spAdminGetUserIPAddresses] 
(
	@UserId		int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int

	SELECT 
		[UserIPId], 
		[UserId], 
		[IPOctet1Begin], 
		[IPOctet2Begin], 
		[IPOctet3Begin], 
		[IPOctet4Begin], 
		[IPOctet4End] 
	FROM 
		[dbo].[UserIPAddresses]
	WHERE
		UserId = @UserId

	SELECT @ERROR = @@ERROR

	IF (@ERROR <> 0)
		GOTO FAIL
	ELSE
		RETURN 0	

	FAIL:
	 BEGIN
		--AN ERROR OCCURED
		RAISERROR('Failed to get user IP information.', 16, 1)
		RETURN -1
	 END
END



GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


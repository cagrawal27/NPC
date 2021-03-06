/****** Object:  Stored Procedure dbo.spGetUserInfo    Script Date: 1/7/2007 6:21:18 PM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spGetUserInfo]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spGetUserInfo]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

/****** Object:  Stored Procedure dbo.spGetUserInfo    Script Date: 1/7/2007 6:21:18 PM ******/


/*=============================================
Author:  Monish Nagisetty
Created: 03/17/2006
Description:	This procedure returns user 
information.
===============================================
Revision History
=============================================================================
Change   Initial Date        Description
     1       MN  01/07/2007  Removed sql to return subscription status since
							 subscriptions are now handled differently.                                 
=============================================================================*/
CREATE    PROCEDURE [dbo].[spGetUserInfo] 
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
		@UserId		int

	SELECT 
		[UserId], 
		[FirstName], 
		[LastName], 
		[MiddleInitial], 
		[AccountTypeId], 
		[AccountStatus]
	FROM 
		[dbo].[Users]
	WHERE 
		Email = @Email
		AND
		Active = 1

	SELECT @ERROR = @@ERROR, @RC = @@ROWCOUNT

	IF (@ERROR <> 0 OR @RC <> 1)
		GOTO FAIL

	SELECT
		RoleId
	FROM
		UserRoles ur
	INNER JOIN Users u ON
		u.UserId = ur.UserId
	WHERE
		u.Email = @Email
		AND
		u.Active = 1

	SELECT @ERROR = @@ERROR, @RC = @@ROWCOUNT

	IF (@ERROR <> 0 OR @RC = 0)
		GOTO FAIL
	ELSE
		RETURN 0	


	--Returns 0 or more depending on whether
	--the user has a valid subscription
	/*
	1.  See revision history
	SELECT [dbo].[IsUserSubscriber](@Email, GETDATE()) as 'IsUserSubscriber'		

	IF (@ERROR <> 0 OR @RC = 0)
		GOTO FAIL
	ELSE
		RETURN 0	
	*/

	FAIL:
	 BEGIN
		--AN ERROR OCCURED
		RAISERROR('Failed to get user information.', 16, 1)
		RETURN -1
	 END
END








GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


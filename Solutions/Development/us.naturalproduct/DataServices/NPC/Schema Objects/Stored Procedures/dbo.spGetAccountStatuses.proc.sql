-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/20/2006
-- Description:	
-- =============================================
CREATE  PROCEDURE [dbo].[spGetAccountStatuses]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int
	SELECT 
		[AccountStatusID], 
		[AccountStatusDescription]
	FROM 
		[dbo].[AccountStatus]
	WHERE
		Active = 1

	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
		RETURN 0
	ELSE
		RETURN -1
END



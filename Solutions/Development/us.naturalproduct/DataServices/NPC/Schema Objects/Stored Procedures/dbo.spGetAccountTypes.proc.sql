-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/05/2006
-- Description:	Gets countries from the Countries
--				table.
-- =============================================
CREATE PROCEDURE [dbo].[spGetAccountTypes]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int

	SELECT 
		[AccountTypeId], 
		[Description]
	FROM
		[dbo].[AccountTypes]
	WHERE
		Active = 1	

	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
	 BEGIN
		RETURN 0
	 END
	ELSE
	 BEGIN
		RETURN -1
	 END

END



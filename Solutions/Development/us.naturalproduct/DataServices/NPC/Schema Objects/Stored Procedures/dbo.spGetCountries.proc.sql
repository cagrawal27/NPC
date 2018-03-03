﻿-- =============================================
-- Author:		Monish Nagisetty
-- ALTER  date: 03/05/2006
-- Description:	Gets countries from the Countries
--				table.
-- =============================================
CREATE  PROCEDURE [dbo].[spGetCountries]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int,
			@RC		int

	SELECT 
		[CountryId]
		,[CountryName]
	FROM 
		[dbo].[Countries]
	ORDER BY
		CountryName
	
	SELECT @ERROR = @@ERROR, @RC = @@ROWCOUNT

	IF (@ERROR = 0)
	 BEGIN
		RETURN 0
	 END
	ELSE
	 BEGIN
		RETURN -1
	 END

END


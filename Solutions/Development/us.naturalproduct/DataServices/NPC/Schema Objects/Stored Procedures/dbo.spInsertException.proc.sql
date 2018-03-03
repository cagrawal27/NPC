-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/24/2006
-- Description:	
-- =============================================
CREATE  PROC dbo.spInsertException
(
	 @Source	varchar(100)
	,@LogDateTime	dateTime
	,@Message	varchar(1000)
	,@Form		varchar(4000)
	,@QueryString	varchar(2000)
	,@TargetSite	varchar(300)
	,@StackTrace	varchar(4000)
	,@Referrer 	varchar(250)
)
AS
	SET NOCOUNT ON;

	DECLARE @ERROR		int,
		@IDENTITY	int

	INSERT INTO ExceptionLog
	(
		Source, 
		LogDateTime,
		Message,
		Form,
		QueryString,
		TargetSite,
		StackTrace,
		Referrer
	)
	Values 
	( 
		@Source,
		@LogDateTime,
		@Message,
		@Form,
		@QueryString,
		@TargetSite,
		@StackTrace,
		@Referrer
	)


	SELECT @ERROR = @@ERROR, @IDENTITY = @@IDENTITY
	
	IF (@ERROR = 0)
	 BEGIN
		RETURN @IDENTITY
	 END
	ELSE
	 BEGIN
		RAISERROR('An error occured inserting into the exceptionlog table.', 16, 1)
		RETURN -1
	 END



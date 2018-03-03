-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/09/2006
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateArticle]
(
	 @ArticleId		int
	,@Title			varchar(1000)
	,@Authors		varchar(1000)
	,@Keywords		varchar(1000)
	,@PageNumber		int
	,@Active		bit
	,@UpdateUserId		int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int

	UPDATE [Articles]
	SET 
		 [Title] 		= @Title
		,[Authors]		= @Authors
		,[Keywords]		= @Keywords
		,[PageNumber]		= @PageNumber
		,[Active]		= @Active
		,[UpdateUserId]		= @UpdateUserId
		,[UpdateDateTime]	= GETDATE()
	WHERE 
		ArticleId 		= @ArticleId

	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
		RETURN 0
	ELSE
	 BEGIN
		RAISERROR('An error occured while updating article details.', 16, 1)
		RETURN -1
	 END
END



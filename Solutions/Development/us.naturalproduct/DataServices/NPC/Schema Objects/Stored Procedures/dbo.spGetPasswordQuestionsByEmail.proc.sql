-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/17/2006
-- Description:	This procedure returns the password
--				and salt for a given user.
-- =============================================
CREATE PROCEDURE [dbo].[spGetPasswordQuestionsByEmail] 
(
	@Email	varchar(50)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int,
		@RC		int

	SELECT 
		[QuestionId],
		[Description]
	FROM 
		[PasswordRecoveryQuestions] prq
	INNER JOIN Users u ON
		prq.QuestionId = u.SecretQuestion1Id
		or
		prq.QuestionId = u.SecretQuestion2Id
	WHERE 
		u.Email = @Email
		AND
		u.Active = 1

	SELECT @ERROR = @@ERROR, @RC = @@ROWCOUNT

	IF (@ERROR <> 0 OR @RC = 0)
		GOTO FAIL
	ELSE
		RETURN 0	

	FAIL:
	 BEGIN
		--AN ERROR OCCURED
		RAISERROR('Failed to get user security information.', 16, 1)
		RETURN -1
	 END
END



-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date:  03/05/2006
-- Description:  Inserts a user's address into 
--				the user's table
-- =============================================
CREATE  PROCEDURE [dbo].[spInsertUserAddress] 
(
	 @UserId			int
	,@AddressTypeId		int
	,@Line1				varchar(50)
	,@Line2				varchar(50)
	,@City				varchar(50)
	,@StateProvince		varchar(50)
	,@CountryId			int
	,@Phone				varchar(30)
	,@Fax				varchar(30)
	,@Active			bit
	,@CreationUserId	int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR		int,
			@RC			int,
			@IDENTITY	int

--START TRANSACTION
	BEGIN TRAN

INSERT INTO [dbo].[Addresses]
    (
			[Line1]
           ,[Line2]
           ,[City]
           ,[StateProvince]
           ,[CountryId]
           ,[Phone]
           ,[Fax]
           ,[Active]
           ,[CreationUserId]
           ,[CreationDateTime]
           ,[UpdateUserId]
           ,[UpdateDateTime])
     VALUES
	 (
			@Line1
           ,@Line2
           ,@City
           ,@StateProvince
           ,@CountryId
           ,@Phone
           ,@Fax
           ,@Active
           ,@CreationUserId
           ,GETDATE()
           ,@CreationUserId
           ,GETDATE()
	)

	SELECT @ERROR = @@ERROR, @IDENTITY = @@IDENTITY

	IF (@ERROR <> 0)
		GOTO FAIL
	ELSE
	 BEGIN
		INSERT INTO [dbo].[UserAddresses]
		(
				[UserId]
			   ,[AddressId]
			   ,[AddressTypeId]
		)
		VALUES
		(
				@UserId
			   ,@IDENTITY
			   ,@AddressTypeId
		)		

		SELECT @ERROR = @@ERROR

		IF (@ERROR <> 0)
			GOTO FAIL
		ELSE
			GOTO SUCCESS

	 END


	SUCCESS:
	 BEGIN
		COMMIT TRAN
		RETURN 0
	 END

	FAIL:
	 BEGIN
		ROLLBACK TRAN
		RAISERROR('Failed to add address for user.', 16, 1)
		RETURN -1
	 END
END



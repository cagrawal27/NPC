-- =============================================
-- Script Template
-- =============================================
DECLARE @USRIDENTITY INT
DECLARE @ADDRIDENTITY INT

--INSER ADMIN USER
INSERT INTO [dbo].[Users]
(
	    [Email]
           ,[PasswordHash]
	   ,[PasswordSalt]
           ,[FirstName]
           ,[LastName]
           ,[MiddleInitial]
           ,[SecretQuestion1Id]
           ,[SecretQuestion2Id]
           ,[SecretAnswer1Hash]
           ,[SecretAnswer2Hash]
           ,[AccountTypeId]
           ,[AccountStatus]
           ,[Active]
           ,[CreationUserId]
           ,[CreationDateTime]
           ,[UpdateUserId]
           ,[UpdateDateTime])
     VALUES
           (
	    'mnagisetty@yahoo.com'
           ,'A0D78BA0445642CBD00CC4811F0D234A'
	   ,'tiv816EPXJi6iw=='           
           ,'Monish'
           ,'Nagisetty'
	   ,''
           ,1
           ,2
           ,'5265CEB37D775551F3B1F0F9F7D960CD'
           ,'3B3D42C6E256AA0D547814313137C65F'
           ,1
           ,1
           ,1
           ,1
           ,GETDATE()
           ,1
           ,GETDATE()
)

SELECT @USRIDENTITY = @@IDENTITY

INSERT INTO [dbo].[Addresses]
(
	[Line1], 
	[Line2], 
	[City], 
	[StateProvince], 
	[CountryId], 
	[Phone], 
	[Fax], 
	[Active], 
	[CreationUserId], 
	[CreationDateTime], 
	[UpdateUserId], 
	[UpdateDateTime]
)
VALUES
(	
	'123 Some st.' 
	,''
	,'Some Town'
	,'OH'
	,225
	,613-333-3333
	,''
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
)

SELECT @ADDRIDENTITY = @@IDENTITY


INSERT INTO [dbo].[UserAddresses]
(
	[UserId], 
	[AddressId], 
	[AddressTypeId]
)
VALUES
(
	 @USRIDENTITY 
	,@ADDRIDENTITY 
	,1
)


INSERT INTO [dbo].[UserRoles]
(
	[RoleId], 
	[UserId]
)
VALUES
(	
	1
	,@USRIDENTITY
)

INSERT INTO [dbo].[UserRoles]
(
	[RoleId], 
	[UserId]
)
VALUES
(	
	2
	,@USRIDENTITY
)


--INSER REGULAR USER
INSERT INTO [dbo].[Users]
(
	    [Email]
           ,[PasswordHash]
	   ,[PasswordSalt]
           ,[FirstName]
           ,[LastName]
           ,[MiddleInitial]
           ,[SecretQuestion1Id]
           ,[SecretQuestion2Id]
           ,[SecretAnswer1Hash]
           ,[SecretAnswer2Hash]
           ,[AccountTypeId]
           ,[AccountStatus]
           ,[Active]
           ,[CreationUserId]
           ,[CreationDateTime]
           ,[UpdateUserId]
           ,[UpdateDateTime])
     VALUES
           (
	    'm_nagisetty@yahoo.com'
           ,'A0D78BA0445642CBD00CC4811F0D234A'
	   ,'tiv816EPXJi6iw=='           
           ,'Monish'
           ,'Nagisetty'
	   ,''
           ,1
           ,2
           ,'5265CEB37D775551F3B1F0F9F7D960CD'
           ,'3B3D42C6E256AA0D547814313137C65F'
           ,1
           ,1
           ,1
           ,1
           ,GETDATE()
           ,1
           ,GETDATE()
)

SELECT @USRIDENTITY = @@IDENTITY

INSERT INTO [dbo].[Addresses]
(
	[Line1], 
	[Line2], 
	[City], 
	[StateProvince], 
	[CountryId], 
	[Phone], 
	[Fax], 
	[Active], 
	[CreationUserId], 
	[CreationDateTime], 
	[UpdateUserId], 
	[UpdateDateTime]
)
VALUES
(	
	'123 Some st.' 
	,''
	,'Some Town'
	,'OH'
	,225
	,613-333-3333
	,''
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
)

SELECT @ADDRIDENTITY = @@IDENTITY


INSERT INTO [dbo].[UserAddresses]
(
	[UserId], 
	[AddressId], 
	[AddressTypeId]
)
VALUES
(
	 @USRIDENTITY 
	,@ADDRIDENTITY 
	,1
)


INSERT INTO [dbo].[UserRoles]
(
	[RoleId], 
	[UserId]
)
VALUES
(	
	1
	,@USRIDENTITY
)

-- =============================================
-- Script Template
-- =============================================
--ADDS REFERENCE DATA

--ADDS Numbers
DECLARE @NumberCount int
SET @NumberCount = 1

WHILE @NumberCount < 8002
 BEGIN -- this will loop thru all your own procs and grant Execute privileges on each one

	INSERT INTO [dbo].[Numbers]([NullCol])
    VALUES(null)

	SET @NumberCount = @NumberCount + 1
 END

--ACCOUNT STATUSES
INSERT INTO [dbo].[AccountStatus]
(
	[AccountStatusID], 
	[AccountStatusDescription], 
	[Active], 
	[CreateUserId], 
	[CreateDateTime], 
	[UpdateUserId], 
	[UpdateDateTime]
)
VALUES(
	1 
	,'Account Active'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
)

INSERT INTO [dbo].[AccountStatus]
(
	[AccountStatusID], 
	[AccountStatusDescription], 
	[Active], 
	[CreateUserId], 
	[CreateDateTime], 
	[UpdateUserId], 
	[UpdateDateTime]
)
VALUES(
	2 
	,'Account Locked'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
)


INSERT INTO [dbo].[AccountStatus]
(
	[AccountStatusID], 
	[AccountStatusDescription], 
	[Active], 
	[CreateUserId], 
	[CreateDateTime], 
	[UpdateUserId], 
	[UpdateDateTime]
)
VALUES(
	3 
	,'Account Stale'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
)

--ADD ACCOUNT TYPES
INSERT INTO [dbo].[AccountTypes]
(
	[AccountTypeId], 
	[Description], 
	[Active], 
	[CreationUserId], 
	[CreationDateTime], 
	[UpdateUserId], 
	[UpdateDateTime]
)
VALUES(
	1 
	,'Personal'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
)
INSERT INTO [dbo].[AccountTypes]
(
	[AccountTypeId], 
	[Description], 
	[Active], 
	[CreationUserId], 
	[CreationDateTime], 
	[UpdateUserId], 
	[UpdateDateTime]
)
VALUES(
	2 
	,'Institutional'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
)

--ADD ADDRESSTYPES

INSERT INTO [dbo].[AddressTypes]
(
	[AddressTypeId], 
	[AddressTypeDescription], 
	[Active], 
	[CreationUserId], 
	[CreationDateTime], 
	[UpdateUserId], 
	[UpdateDateTime]
)
VALUES
(
	1 
	,'Default'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
)

INSERT INTO [dbo].[AddressTypes]
(
	[AddressTypeId], 
	[AddressTypeDescription], 
	[Active], 
	[CreationUserId], 
	[CreationDateTime], 
	[UpdateUserId], 
	[UpdateDateTime]
)
VALUES
(
	2 
	,'Billing'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
)

--ADD COUNTRIES
DECLARE @CSV varchar(8000)
DECLARE @CSV2 varchar(8000)

SET @CSV = 'AP,EU,AD,AE,AF,AG,AI,AL,AM,AN,AO,AQ,AR,AS,AT,AU,AW,AZ,BA,BB,BD,BE,BF,BG,BH,BI,BJ,BM,BN,BO,BR,BS,BT,BV,BW,BY,BZ,CA,CC,CD,CF,CG,CH,CI,CK,CL,CM,CN,CO,CR,CU,CV,CX,CY,CZ,DE,DJ,DK,DM,DO,DZ,EC,EE,EG,EH,ER,ES,ET,FI,FJ,FK,FM,FO,FR,FX,GA,GB,GD,GE,GF,GH,GI,GL,GM,GN,GP,GQ,GR,GS,GT,GU,GW,GY,HK,HM,HN,HR,HT,HU,ID,IE,IL,IN,IO,IQ,IR,IS,IT,JM,JO,JP,KE,KG,KH,KI,KM,KN,KP,KR,KW,KY,KZ,LA,LB,LC,LI,LK,LR,LS,LT,LU,LV,LY,MA,MC,MD,MG,MH,MK,ML,MM,MN,MO,MP,MQ,MR,MS,MT,MU,MV,MW,MX,MY,MZ,NA,NC,NE,NF,NG,NI,NL,NO,NP,NR,NU,NZ,OM,PA,PE,PF,PG,PH,PK,PL,PM,PN,PR,PS,PT,PW,PY,QA,RE,RO,RU,RW,SA,SB,SC,SD,SE,SG,SH,SI,SJ,SK,SL,SM,SN,SO,SR,ST,SV,SY,SZ,TC,TD,TF,TG,TH,TJ,TK,TM,TN,TO,TP,TR,TT,TV,TW,TZ,UA,UG,UM,US,UY,UZ,VA,VC,VE,VG,VI,VN,VU,WF,WS,YE,YT,CS,ZA,ZM,ZR,ZW,A1,A2'
SET @CSV2 = 'Asia/Pacific Region;Europe;Andorra;United Arab Emirates;Afghanistan;Antigua and Barbuda;Anguilla;Albania;Armenia;Netherlands Antilles;Angola;Antarctica;Argentina;American Samoa;Austria;Australia;Aruba;Azerbaijan;Bosnia and Herzegovina;Barbados;Bangladesh;Belgium;Burkina Faso;Bulgaria;Bahrain;Burundi;Benin;Bermuda;Brunei Darussalam;Bolivia;Brazil;Bahamas;Bhutan;Bouvet Island;Botswana;Belarus;Belize;Canada;Cocos (Keeling) Islands;Congo, The Democratic Republic of the;Central African Republic;Congo;Switzerland;Cote D''Ivoire;Cook Islands;Chile;Cameroon;China;Colombia;Costa Rica;Cuba;Cape Verde;Christmas Island;Cyprus;Czech Republic;Germany;Djibouti;Denmark;Dominica;Dominican Republic;Algeria;Ecuador;Estonia;Egypt;Western Sahara;Eritrea;Spain;Ethiopia;Finland;Fiji;Falkland Islands (Malvinas);Micronesia, Federated States of;Faroe Islands;France;France, Metropolitan;Gabon;United Kingdom;Grenada;Georgia;French Guiana;Ghana;Gibraltar;Greenland;Gambia;Guinea;Guadeloupe;Equatorial Guinea;Greece;South Georgia and the South Sandwich Islands;Guatemala;Guam;Guinea-Bissau;Guyana;Hong Kong;Heard Island and McDonald Islands;Honduras;Croatia;Haiti;Hungary;Indonesia;Ireland;Israel;India;British Indian Ocean Territory;Iraq;Iran, Islamic Republic of;Iceland;Italy;Jamaica;Jordan;Japan;Kenya;Kyrgyzstan;Cambodia;Kiribati;Comoros;Saint Kitts and Nevis;Korea, Democratic People''s Republic of;Korea, Republic of;Kuwait;Cayman Islands;Kazakstan;Lao People''s Democratic Republic;Lebanon;Saint Lucia;Liechtenstein;Sri Lanka;Liberia;Lesotho;Lithuania;Luxembourg;Latvia;Libyan Arab Jamahiriya;Morocco;Monaco;Moldova, Republic of;Madagascar;Marshall Islands;Macedonia;Mali;Myanmar;Mongolia;Macau;Northern Mariana Islands;Martinique;Mauritania;Montserrat;Malta;Mauritius;Maldives;Malawi;Mexico;Malaysia;Mozambique;Namibia;New Caledonia;Niger;Norfolk Island;Nigeria;Nicaragua;Netherlands;Norway;Nepal;Nauru;Niue;New Zealand;Oman;Panama;Peru;French Polynesia;Papua New Guinea;Philippines;Pakistan;Poland;Saint Pierre and Miquelon;Pitcairn Islands;Puerto Rico;Palestinian Territory;Portugal;Palau;Paraguay;Qatar;Reunion;Romania;Russian Federation;Rwanda;Saudi Arabia;Solomon Islands;Seychelles;Sudan;Sweden;Singapore;Saint Helena;Slovenia;Svalbard and Jan Mayen;Slovakia;Sierra Leone;San Marino;Senegal;Somalia;Suriname;Sao Tome and Principe;El Salvador;Syrian Arab Republic;Swaziland;Turks and Caicos Islands;Chad;French Southern Territories;Togo;Thailand;Tajikistan;Tokelau;Turkmenistan;Tunisia;Tonga;East Timor;Turkey;Trinidad and Tobago;Tuvalu;Taiwan;Tanzania, United Republic of;Ukraine;Uganda;United States Minor Outlying Islands;United States;Uruguay;Uzbekistan;Holy See (Vatican City State);Saint Vincent and the Grenadines;Venezuela;Virgin Islands, British;Virgin Islands, U.S.;Vietnam;Vanuatu;Wallis and Futuna;Samoa;Yemen;Mayotte;Serbia and Montenegro;South Africa;Zambia;Zaire;Zimbabwe;Anonymous Proxy;Satellite Provider'

INSERT INTO [dbo].[Countries]
(
	[CountryId]
	,[CountryCode]
	,[CountryName]
)
SELECT 
		a.ID AS CountryId,	
		LTRIM(RTRIM(a.Value)) as CountryCode,
		LTRIM(RTRIM(b.Value)) as CountryName
FROM 
	[TabulateCSV]  (@CSV) a
INNER JOIN [TabulateCharSV]  (@CSV2, ';') b ON
	a.ID = b.ID

--ADD ARTICLE DOCUMENT TYPES

INSERT INTO [dbo].[ArticleDocTypes]
(
	[ArtDocTypeDescription], 
	[Active], 
	[CreationUserId], 
	[CreationDateTime], 
	[UpdateUserId], 
	[UpdateDateTime],
	[PubliclyAvailable]
)
VALUES
(	
	'Abstract'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
	,1
)

INSERT INTO [dbo].[ArticleDocTypes]
(
	[ArtDocTypeDescription], 
	[Active], 
	[CreationUserId], 
	[CreationDateTime], 
	[UpdateUserId], 
	[UpdateDateTime],
	[PubliclyAvailable]
)
VALUES
(	
	'Full-Text'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
	,0
)

INSERT INTO [dbo].[ArticleDocTypes]
(
	[ArtDocTypeDescription], 
	[Active], 
	[CreationUserId], 
	[CreationDateTime], 
	[UpdateUserId], 
	[UpdateDateTime],
	[PubliclyAvailable]
)
VALUES
(	
	'Supplementary Data'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
	,0
)


--ADD Issue Doc Types

INSERT INTO [dbo].[IssueDocTypes]
(
	[IssueDocTypeDescription], 
	[Active], 
	[CreationUserId], 
	[CreationDateTime], 
	[UpdateUserId], 
	[UpdateDateTime],
	[PubliclyAvailable]
)
VALUES
(
	'Table of Contents'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
	,1
)

INSERT INTO [dbo].[IssueDocTypes]
(
	[IssueDocTypeDescription], 
	[Active], 
	[CreationUserId], 
	[CreationDateTime], 
	[UpdateUserId], 
	[UpdateDateTime],
	[PubliclyAvailable]
)
VALUES
(
	'Manuscripts in Press'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
	,1
)

INSERT INTO [dbo].[IssueDocTypes]
(
	[IssueDocTypeDescription], 
	[Active], 
	[CreationUserId], 
	[CreationDateTime], 
	[UpdateUserId], 
	[UpdateDateTime],
	[PubliclyAvailable]
)
VALUES
(
	'Authors'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
	,1
)

INSERT INTO [dbo].[IssueDocTypes]
(
	[IssueDocTypeDescription], 
	[Active], 
	[CreationUserId], 
	[CreationDateTime], 
	[UpdateUserId], 
	[UpdateDateTime],
	[PubliclyAvailable]
)
VALUES
(
	'Full Issue'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
	,0
)


--ADD PASSWORD RECOVERY QUESTIONS

INSERT INTO [dbo].[PasswordRecoveryQuestions]
(	
	[Description], 
	[Active], 
	[CreationUserId], 
	[CreationDateTime], 
	[UpdateUserId], 
	[UpdateDateTime]
)
VALUES
(
	'Mother''s Maiden Name'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
)

INSERT INTO [dbo].[PasswordRecoveryQuestions]
(	
	[Description], 
	[Active], 
	[CreationUserId], 
	[CreationDateTime], 
	[UpdateUserId], 
	[UpdateDateTime]
)
VALUES
(
	'City of Birth'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
)

INSERT INTO [dbo].[PasswordRecoveryQuestions]
(	
	[Description], 
	[Active], 
	[CreationUserId], 
	[CreationDateTime], 
	[UpdateUserId], 
	[UpdateDateTime]
)
VALUES
(
	'Name of Favorite Teacher/Professor'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
)

INSERT INTO [dbo].[PasswordRecoveryQuestions]
(	
	[Description], 
	[Active], 
	[CreationUserId], 
	[CreationDateTime], 
	[UpdateUserId], 
	[UpdateDateTime]
)
VALUES
(
	'Name of First School'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
)

--ADD ROLES

INSERT INTO [dbo].[Roles]
(
	[RoleDescription], 
	[Active], 
	[CreationUserId], 
	[CreationDateTime], 
	[UpdateUserId], 
	[UpdateDateTime]
)
VALUES
(	
	'Member'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
)

INSERT INTO [dbo].[Roles]
(
	[RoleDescription], 
	[Active], 
	[CreationUserId], 
	[CreationDateTime], 
	[UpdateUserId], 
	[UpdateDateTime]
)
VALUES
(	
	'Admin'
	,1
	,1
	,GETDATE()
	,1
	,GETDATE()
)
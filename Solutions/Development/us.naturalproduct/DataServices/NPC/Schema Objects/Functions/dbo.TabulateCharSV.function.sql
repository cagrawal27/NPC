/********************************************************************
**
**	NAME: 	TabulateCharSV
**
**	AUTHOR: Monish Nagisetty
**
**	DATE: 	01/04/2005
**
**	DESC: 	Converts a list of character seperated values into a table.  
**		Can return up to 50 items each with a max length of 100 characters.
**
**	INPUT:	@CSV - Comma seperated value 
**		@Char - The delimiting character
**		Ex:  ab,cdef,gh,ijklmn,op
**
**	OUTPUT:	Result set containing a list of successful/unsuccesful
**		cases of the bulk 
**
**	UPDATES:
**	DATE	UPDATED BY		COMMENTS
**	
**
**
********************************************************************/
--Multi-statement Table-Valued User-Defined Function
CREATE     FUNCTION dbo.TabulateCharSV
(
	@CSV varchar(8000),
	@Char char(1)
)
RETURNS @TabularData Table
(
	[ID]		int,
	[Value]		varchar(1000)
)
AS  
BEGIN 
	INSERT INTO @TabularData
	(
		[ID],
		[Value]
	)
	(
		SELECT 
			1 - LEN(REPLACE(LEFT(@CSV, Number), @Char, SPACE(0))) + Number AS [ID],
			SUBSTRING(@CSV, Number, CHARINDEX(@Char, @CSV + @Char, Number) - Number) AS [Value]
		FROM 
			Numbers
		WHERE 
			SUBSTRING(@Char + @CSV, Number, 1) = @Char
			AND 
			Number < LEN(@CSV) + 1
	)
	
	RETURN
END



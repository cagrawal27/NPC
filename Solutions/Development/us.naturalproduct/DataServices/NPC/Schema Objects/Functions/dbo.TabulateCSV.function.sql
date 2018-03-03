/********************************************************************
**
**	NAME: 	TabulateCSV
**
**	AUTHOR: Monish Nagisetty
**
**	DATE: 	12/30/2004
**
**	DESC: 	Converts a csv into a table.  Can return up to
**		50 items each with a max length of 100 characters.
**
**	INPUT:	@CSV - Comma seperated value 
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
CREATE     FUNCTION dbo.TabulateCSV
(
	@CSV varchar(8000)
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
			1 - LEN(REPLACE(LEFT(@CSV, Number), ',', SPACE(0))) + Number AS [ID],
			SUBSTRING(@CSV, Number, CHARINDEX(',', @CSV + ',', Number) - Number) AS [Value]
		FROM 
			Numbers
		WHERE 
			SUBSTRING(',' + @CSV, Number, 1) = ','
			AND 
			Number < LEN(@CSV) + 1
	)
	
	RETURN
END



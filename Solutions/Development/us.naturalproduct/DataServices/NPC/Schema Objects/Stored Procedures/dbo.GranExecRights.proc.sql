/* ------------------------------------------------------------
PROCEDURE: prc_gen_CreateGrants

DESCRIPTION: Grants Execute permissions on all procs in database
for Login MyLogin

AUTHOR: Brian Lockwood 3/15/00 5:38:48 PM
------------------------------------------------------------ */
CREATE    PROCEDURE dbo.GranExecRights
AS

DECLARE @LoginName	varchar(50)
DECLARE @ExecSQL 	varchar(100)

SET @LoginName = 'db_npcwebrole'

DECLARE curGrants CURSOR FOR
SELECT 
	'GRANT EXECUTE ON ' + NAME + ' TO '+@LoginName
FROM 
	dbo.sysobjects
WHERE 
	TYPE = 'P'
	AND 
	LEFT(NAME,2) = 'sp' 

OPEN curGrants
FETCH NEXT FROM curGrants
INTO @ExecSQL


WHILE @@FETCH_STATUS = 0
 BEGIN -- this will loop thru all your own procs and grant Execute privileges on each one

	Exec(@ExecSQL)

	IF @@ERROR <> 0
	 BEGIN
 		RETURN 1 -- return 1 if there is an error
	 END
	
	Print @ExecSQL
	
	FETCH NEXT FROM curGrants INTO @ExecSQL

 END

CLOSE curGrants
DEALLOCATE curGrants



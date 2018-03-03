-- =============================================
-- Author:  Monish Nagisetty
-- ALTER  date: 03/09/2006
-- Description:	Gets a list of all issues for
--		a volume
-- =============================================
CREATE     PROCEDURE [dbo].[spAdminGetIssueDocs]
(
	@VolumeIssueId		int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ERROR	int

	SELECT
		 d.DocId
		,FileName
		,[id].IssueDocTypeId
		,IssueDocTypeDescription
		,d.Active
	FROM
		Documents d
	INNER JOIN IssueDocuments [id] ON
		d.DocId = [id].DocId
	INNER JOIN IssueDocTypes idt ON
		idt.IssueDocTypeId = [id].IssueDocTypeId
	WHERE
		[id].VolumeIssueId = @VolumeIssueId


	SELECT @ERROR = @@ERROR

	IF (@ERROR = 0)
	 BEGIN
		RETURN 0
	 END
	ELSE
	 BEGIN
		RAISERROR('An error occured while getting issue details.', 16, 1)
		RETURN -1
	 END


END



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fnGetSubordinateDocs]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fnGetSubordinateDocs]
GO


CREATE FUNCTION [dbo].[fnGetSubordinateDocs]
(
	@DocId int
)
RETURNS TABLE
AS
RETURN  
(
	WITH SubordinateDocs
	AS 
	( 
	SELECT d.DocId, dr.ParentDocId
	FROM Docs d 
		inner join DocRelations dr on d.DocId = dr.DocId
		WHERE d.DocId = @DocId
	UNION ALL
	SELECT d.DocId, dr.ParentDocId 
	FROM Docs d  
		inner join DocRelations dr on d.DocId = dr.DocId
		inner join SubordinateDocs acu ON dr.ParentDocId = acu.DocId
	)
	SELECT DocId 
	FROM SubordinateDocs
)
GO
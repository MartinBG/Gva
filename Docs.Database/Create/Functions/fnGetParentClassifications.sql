IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fnGetParentClassifications]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fnGetParentClassifications]
GO


CREATE FUNCTION [dbo].[fnGetParentClassifications]
(
	@ClassificationId int
)
RETURNS TABLE
AS
RETURN  
(
	WITH ParentClassifications
	AS 
	( 
	SELECT u.ClassificationId, ur.ParentClassificationId 
	FROM Classifications u 
		inner join ClassificationRelations ur on u.ClassificationID = ur.ClassificationId 
		WHERE u.ClassificationID = @ClassificationId
	UNION ALL
	SELECT u.ClassificationId, ur.ParentClassificationId 
	FROM Classifications u 
		inner join ClassificationRelations ur on u.ClassificationID = ur.ClassificationId 
		inner join ParentClassifications acu ON ur.ClassificationId = acu.ParentClassificationId
	)
	SELECT ClassificationId 
	FROM ParentClassifications
)
GO
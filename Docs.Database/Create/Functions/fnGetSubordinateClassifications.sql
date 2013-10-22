IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fnGetSubordinateClassifications]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fnGetSubordinateClassifications]
GO


CREATE FUNCTION [dbo].[fnGetSubordinateClassifications]
(
	@ClassificationId int
)
RETURNS TABLE
AS
RETURN  
(
	WITH SubordinateClassifications
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
		inner join SubordinateClassifications acu ON ur.ParentClassificationId = acu.ClassificationId
	)
	SELECT ClassificationId 
	FROM SubordinateClassifications
)
GO
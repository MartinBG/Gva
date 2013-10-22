IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fnGetSubordinateUnits]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fnGetSubordinateUnits]
GO


CREATE FUNCTION [dbo].[fnGetSubordinateUnits]
(
	@UnitId int
)
RETURNS TABLE
AS
RETURN  
(
	WITH SubordinateUnits
	AS 
	( 
	SELECT u.UnitId, ur.ParentUnitId 
	FROM Units u 
		inner join UnitRelations ur on u.UnitID = ur.UnitId 
		WHERE u.UnitID = @UnitId
	UNION ALL
	SELECT u.UnitId, ur.ParentUnitId 
	FROM Units u 
		inner join UnitRelations ur on u.UnitID = ur.UnitId 
		inner join SubordinateUnits acu ON ur.ParentUnitId = acu.UnitId
	)
	SELECT UnitId 
	FROM SubordinateUnits
)
GO
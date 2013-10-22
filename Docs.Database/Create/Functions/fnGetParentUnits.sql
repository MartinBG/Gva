IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fnGetParentUnits]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fnGetParentUnits]
GO


CREATE FUNCTION [dbo].[fnGetParentUnits]
(
	@UnitId int
)
RETURNS TABLE
AS
RETURN  
(
	WITH ParentUnits
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
		inner join ParentUnits acu ON ur.UnitId = acu.ParentUnitId
	)
	SELECT UnitId 
	FROM ParentUnits
)
GO
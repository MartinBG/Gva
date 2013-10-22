print 'spGetUnitClassifications'
GO

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'spGetUnitClassifications'))
DROP PROCEDURE spGetUnitClassifications
GO

CREATE PROCEDURE spGetUnitClassifications
	  @UnitId int
AS
BEGIN
	SET NOCOUNT ON
	select * from UnitClassifications where UnitId in (select UnitID from dbo.fnGetParentUnits(@UnitId))
END
GO
print 'spSetDeactiveUnit'
GO

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'spSetDeactiveUnit'))
DROP PROCEDURE spSetDeactiveUnit
GO

CREATE PROCEDURE spSetDeactiveUnit
	  @UnitId int
AS
BEGIN
	SET NOCOUNT ON
    update Units Set IsActive = 0 where UnitId in (select UnitID from dbo.fnGetSubordinateUnits(@UnitId))

    exec spSetUserDocs @UnitId
END
GO
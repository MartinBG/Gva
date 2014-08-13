GO
IF OBJECT_ID(N'dbo.ufnGetNomValuesByTextContentProperty', N'IF') IS NOT NULL
    DROP FUNCTION dbo.ufnGetNomValuesByTextContentProperty;
GO

CREATE FUNCTION dbo.ufnGetNomValuesByTextContentProperty(@nomAlias NVARCHAR(100), @textContentProperty NVARCHAR(100), @valueAsString NVARCHAR(100))
RETURNS TABLE 
AS
RETURN 
SELECT  nv.[NomValueId],
        nv.[NomId],
        nv.[Code],
        nv.[Name],
        nv.[NameAlt],
        nv.[ParentValueId],
        nv.[Alias],
        nv.[TextContent],
        nv.[IsActive],
        nv.[Order],
        nv.[OldId]
    FROM NomValues nv
    CROSS APPLY dbo.ufnParseJSON(nv.TextContent) as j
    WHERE nv.NomId in (SELECT NomId FROM Noms WHERE Alias = @nomAlias) AND
	nv.TextContent IS NOT NULL AND
    j.NAME = @textContentProperty AND
    j.StringValue = @valueAsString
GO

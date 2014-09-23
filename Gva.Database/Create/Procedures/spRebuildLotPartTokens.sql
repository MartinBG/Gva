IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'spRebuildLotPartTokens'))
DROP PROCEDURE spRebuildLotPartTokens
GO

CREATE PROCEDURE spRebuildLotPartTokens
AS
BEGIN

DECLARE @LotPartTokens TABLE (
      LotPartId int,
      Token nvarchar(200) not null,
      CreateToken nvarchar(200)
    );

--определяне на token на part-а
--token за схемите по които е класиран part-а, като включваме класификациите на горните нива
insert into @LotPartTokens(LotPartId, Token, CreateToken)
    select distinct lp.LotPartId, 'classification#' + CONVERT(varchar(10), c.ClassificationId), 'classification'
    from LotParts lp
        INNER JOIN GvaLotFiles lf on lp.LotPartId = lf.LotPartId
        INNER JOIN GvaCaseTypes ct on lf.GvaCaseTypeId = ct.GvaCaseTypeId
        CROSS APPLY dbo.fnGetParentClassifications(ct.ClassificationId) c

insert into @LotPartTokens(LotPartId, Token, CreateToken)
    select distinct lp.LotPartId, 'classification#' + CONVERT(varchar(10), c.ClassificationId), 'classification'
    from LotParts lp
        INNER JOIN Lots l on lp.LotId = l.LotId
        INNER JOIN GvaCaseTypes ct on l.LotSetId = ct.LotSetId and ct.IsDefault = 1
        CROSS APPLY dbo.fnGetParentClassifications(ct.ClassificationId) c
    where NOT EXISTS(select 1 from GvaLotFiles lf where lf.LotPartId = lp.LotPartId)

--token за самия part
insert into @LotPartTokens(LotPartId, Token, CreateToken)
    select distinct LotPartId, 'lotPart#' + CONVERT(varchar(10), LotPartId), 'lotPart'
    from LotParts


delete from LotPartTokens

INSERT LotPartTokens(LotPartId, Token, CreateToken)
    select LotPartId, Token, CreateToken from @LotPartTokens

END
GO

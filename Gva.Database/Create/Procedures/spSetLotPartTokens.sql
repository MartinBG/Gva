IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'spSetLotPartTokens'))
DROP PROCEDURE spSetLotPartTokens
GO

CREATE PROCEDURE spSetLotPartTokens
    @LotPartId int
AS
BEGIN

DECLARE @LotParts TABLE (
        LotPartId int
    );

DECLARE @LotPartTokens TABLE (
      LotPartId int,
      Token nvarchar(200) not null,
      CreateToken nvarchar(200)
    );

insert into @LotParts(LotPartId)
    select LotPartId from LotParts
        where (LotPartId = LotPartId or LotPartId is null)

--определяне на token на part-а
insert into @LotPartTokens(LotPartId, Token, CreateToken)
    --token за схемите по които е класиран part-а, като включваме класификациите на горните нива
    select distinct lp.LotPartId, 'classification#' + CONVERT(varchar(10), c.ClassificationId), 'classification'
        from LotParts lp
            INNER JOIN GvaLotFiles lf on lp.LotPartId = lf.LotPartId
            INNER JOIN GvaCaseTypes ct on lf.GvaCaseTypeId = ct.GvaCaseTypeId
            CROSS APPLY dbo.fnGetParentClassifications(ct.ClassificationId) c
        where lp.LotPartId in (select LotPartId from @LotParts)
    union all
        select distinct lp.LotPartId, 'classification#' + CONVERT(varchar(10), c.ClassificationId), 'classification'
            from LotParts lp
                CROSS APPLY dbo.fnGetParentClassifications(
                 (select ct.ClassificationId
                  from LotParts lp2
                     inner join Lots l on lp2.LotId = l.LotId
                     inner join GvaCaseTypes ct on l.LotSetId = ct.LotSetId
                  where lp2.LotPartId = lp.LotPartId and ct.IsDefault = 1)) c
            where lp.LotPartId in (select LotPartId from @LotParts) and NOT EXISTS(select 1 from GvaLotFiles lf where lf.LotPartId = lp.LotPartId)
    union all 
    --token за самия part
    select distinct LotPartId, 'lotPart#' + CONVERT(varchar(10), LotPartId), 'lotPart'
        from @LotParts


--merge Token
MERGE LotPartTokens AS t
USING @LotPartTokens AS s 
    ON t.LotPartId = s.LotPartId and t.Token = s.Token and t.CreateToken = s.CreateToken
WHEN NOT MATCHED BY TARGET THEN
    INSERT (LotPartId, Token, CreateToken)
    VALUES (s.LotPartId, s.Token, s.CreateToken)
WHEN NOT MATCHED BY SOURCE AND t.LotPartId in (select LotPartId from @LotPartTokens) THEN
    DELETE;

END
GO


print 'vwLotPartUsers'
GO

CREATE VIEW vwLotPartUsers WITH SCHEMABINDING
AS
    select lp.LotPartId, u.UserId, ut.ClassificationPermissionId,  COUNT_BIG(*) as _c1
    from dbo.LotParts lp
        inner join dbo.LotPartTokens lpt on lp.LotPartId = lpt.LotPartId 
        inner join dbo.UnitTokens ut on ut.Token = lpt.Token 
        inner join dbo.UnitUsers u on ut.UnitId = u.UnitId and u.IsActive = 1
    group by lp.LotPartId, u.UserId, ut.ClassificationPermissionId
GO

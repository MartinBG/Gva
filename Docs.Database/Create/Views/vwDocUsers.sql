print 'vwDocUsers'
GO 

--drop VIEW vwDocUsers
CREATE VIEW vwDocUsers WITH SCHEMABINDING
AS 
select d.DocId, ut.UnitId, ut.DocUnitPermissionId,  COUNT_BIG(*) as _c1 
from dbo.Docs d
	inner join  dbo.DocTokens dt on d.DocId = dt.DocId 
	inner join dbo.UnitTokens ut on 
		ut.Token = dt.Token 
	inner join dbo.Units u on ut.UnitId = u.UnitId and u.IsActive = 1
group by d.DocId, ut.UnitId, ut.DocUnitPermissionId 
go

CREATE UNIQUE CLUSTERED INDEX vwDocUsers_PK 
	ON vwDocUsers (UnitId, DocUnitPermissionId, DocId  )

GO
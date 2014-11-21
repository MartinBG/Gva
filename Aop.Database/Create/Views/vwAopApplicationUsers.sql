print 'vwAopApplicationUsers'
GO 

--drop VIEW vwAopApplicationUsers
CREATE VIEW vwAopApplicationUsers WITH SCHEMABINDING
AS 
select d.AopApplicationId, ut.UnitId, ut.ClassificationPermissionId,  COUNT_BIG(*) as _c1 
from dbo.AopApplications d
	inner join  dbo.AopApplicationTokens dt on d.AopApplicationId = dt.AopApplicationId 
	inner join dbo.UnitTokens ut on 
		ut.Token = dt.Token 
	inner join dbo.Units u on ut.UnitId = u.UnitId and u.IsActive = 1
group by d.AopApplicationId, ut.UnitId, ut.ClassificationPermissionId 
go

CREATE UNIQUE CLUSTERED INDEX vwAopApplicationUsers_PK 
	ON vwAopApplicationUsers (UnitId, ClassificationPermissionId, AopApplicationId  )

GO
﻿print 'Finalize'
GO

print 'Insert UnitUsers for users system and admin'
insert into UnitUsers(UserId, UnitId, IsActive)
select u.UserId, uu.UnitId, 1
from Users u 
inner join 
(select 
	UnitId, 
	case Name 
		when 'Системен администратор' then 'admin' 
		when 'Системен потребител' then 'system' 
        when 'Тестов потребител' then 'test1' 
		else Name 
	end as Name
	from Units) uu on uu.Name=u.Username
where Username in ('admin', 'system', 'test1')
GO

print 'Set system to all Classifications'
insert into UnitClassifications (UnitId, ClassificationId, ClassificationPermissionId)
	select UnitId, ClassificationId, ClassificationPermissionId
	from (
		select (select uu.UnitId from Users u inner join UnitUsers uu on u.UserId=uu.UserId where u.Username = 'system') as UnitId, c.ClassificationId, r.ClassificationPermissionId   
		from Classifications c cross join 
			ClassificationPermissions r) a
	where not (ClassificationId=1 and ClassificationPermissionId=2)
		and not exists (select null from UnitClassifications uc1 
			where uc1.UnitId = a.UnitId 
			and uc1.ClassificationId = a.ClassificationId 
			and  uc1.ClassificationPermissionId = a.ClassificationPermissionId)

GO
print 'Set admin to all Classifications'
insert into UnitClassifications (UnitId, ClassificationId, ClassificationPermissionId)
	select UnitId, ClassificationId, ClassificationPermissionId
	from (
		select (select uu.UnitId from Users u inner join UnitUsers uu on u.UserId=uu.UserId where u.Username = 'admin') as UnitId, c.ClassificationId, r.ClassificationPermissionId   
		from Classifications c cross join 
			ClassificationPermissions r) a
	where not (ClassificationId=1 and ClassificationPermissionId=2)
		and not exists (select null from UnitClassifications uc1 
			where uc1.UnitId = a.UnitId 
			and uc1.ClassificationId = a.ClassificationId 
			and  uc1.ClassificationPermissionId = a.ClassificationPermissionId)
	
GO
print 'Set test1 to all Classifications'
insert into UnitClassifications (UnitId, ClassificationId, ClassificationPermissionId)
	select UnitId, ClassificationId, ClassificationPermissionId
	from (
		select (select uu.UnitId from Users u inner join UnitUsers uu on u.UserId=uu.UserId where u.Username = 'test1') as UnitId, c.ClassificationId, r.ClassificationPermissionId   
		from Classifications c cross join 
			ClassificationPermissions r) a
	where not (ClassificationId=1 and ClassificationPermissionId=2)
		and not exists (select null from UnitClassifications uc1 
			where uc1.UnitId = a.UnitId 
			and uc1.ClassificationId = a.ClassificationId 
			and  uc1.ClassificationPermissionId = a.ClassificationPermissionId)
	
GO

--print 'Set admin users to admin role'
--INSERT INTO UserRoles (UserId, RoleId)
--select UserId, 1 from users
--	where Username in ('system', 'admin', 'peter', 'testUser', 'systemUser')
--GO


update dt set IsActive = 0 
--select *
from DocTypes dt
	where DocTypeID not in (select DocTypeID from DocTypeUnitRoles )
	and isNull(ElectronicServiceTypeApplication, '') <> ''
	and isNull(ElectronicServiceProvider , '') <> ''
GO

exec spSetUnitTokens null
go



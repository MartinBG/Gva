print 'Finalize'
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
		else Name 
	end as Name
	from Units) uu on uu.Name=u.Username
where Username in ('admin', 'system', 'peter')
GO

print 'Set admin users to admin unit'
update UnitUsers set unitid = (select UnitID from Units where Name = 'Системен администратор')
    where UserId in (select UserId from Users where Username in ('admin', 'peter', 'testUser', 'systemUser'))
GO
print 'Set system to all Classifications'
insert into UnitClassifications (UnitId, ClassificationId, ClassificationRoleId)
	select UnitId, ClassificationId, ClassificationRoleId
	from (
		select (select uu.UnitId from Users u inner join UnitUsers uu on u.UserId=uu.UserId where u.Username = 'system') as UnitId, c.ClassificationId, r.ClassificationRoleId   
		from Classifications c cross join 
			ClassificationRoles r) a
	where not (ClassificationId=1 and ClassificationRoleId=2)
		and not exists (select null from UnitClassifications uc1 
			where uc1.UnitId = a.UnitId 
			and uc1.ClassificationId = a.ClassificationId 
			and  uc1.ClassificationRoleId = a.ClassificationRoleId)

GO
print 'Set admin to all Classifications'
insert into UnitClassifications (UnitId, ClassificationId, ClassificationRoleId)
	select UnitId, ClassificationId, ClassificationRoleId
	from (
		select (select uu.UnitId from Users u inner join UnitUsers uu on u.UserId=uu.UserId where u.Username = 'admin') as UnitId, c.ClassificationId, r.ClassificationRoleId   
		from Classifications c cross join 
			ClassificationRoles r) a
	where not (ClassificationId=1 and ClassificationRoleId=2)
		and not exists (select null from UnitClassifications uc1 
			where uc1.UnitId = a.UnitId 
			and uc1.ClassificationId = a.ClassificationId 
			and  uc1.ClassificationRoleId = a.ClassificationRoleId)
	
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



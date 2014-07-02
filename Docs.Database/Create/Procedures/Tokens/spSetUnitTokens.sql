IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'spSetUnitTokens'))
DROP PROCEDURE spSetUnitTokens
GO

CREATE PROCEDURE spSetUnitTokens
	@UnitId int
AS
BEGIN

DECLARE @Units TABLE (
		UnitId int
	);

DECLARE @UnitTokens TABLE (
      UnitId int,
	  Token nvarchar(200), 
	  CreateToken nvarchar(200),
	  DocUnitPermissionId int
	);

--@Units инициализиране на таблицата с звената
	if @UnitId is null
	begin 
		insert into @Units(UnitId)
			select UnitId from units
	end
	else
	begin		
		insert into @Units(UnitId)	
			select UnitID 
			from dbo.fnGetSubordinateUnits(@UnitId)
	end

--определяне на token на звеното без специалната роля Execution, само за листата 
insert into @UnitTokens(UnitId, Token, CreateToken, DocUnitPermissionId)
	select distinct u.UnitId, 'classification#' + CONVERT(varchar(10), ucpc.ClassificationId), 'classification'
	, (select p.DocUnitPermissionId from DocUnitPermissions p where p.Alias = cr.Alias)
--	, case when cr.Alias = 'Read' then (select DocUnitPermissionId from DocUnitPermissions where Alias = 'Read') 
--			when cr.Alias = 'Register' then (select DocUnitPermissionId from DocUnitPermissions where Alias = 'Register') 
--			when cr.Alias = 'Management' then (select DocUnitPermissionId from DocUnitPermissions where Alias = 'Management') 
--			when cr.Alias = 'ESign' then (select DocUnitPermissionId from DocUnitPermissions where Alias = 'ESign') 
--			when cr.Alias = 'Finish' then (select DocUnitPermissionId from DocUnitPermissions where Alias = 'Finish') 
--			when cr.Alias = 'Reverse' then (select DocUnitPermissionId from DocUnitPermissions where Alias = 'Reverse') 
--			else null end
		from Units u 
			inner join UnitTypes ut  on u.UnitTypeId  = ut.UnitTypeId
		    CROSS APPLY dbo.fnGetParentUnits(u.UnitId) pu
			inner join UnitClassifications uc on uc.UnitId = pu.UnitId 
			inner join ClassificationRoles cr on cr.ClassificationRoleId = uc.ClassificationRoleId 
			CROSS APPLY dbo.fnGetParentClassifications(uc.ClassificationId) ucpc
		where u.IsActive = 1 
		and ut.Alias = 'Employee'
		and u.UnitId in (select UnitId from @Units)
		and cr.Alias <> 'Execution'

--merge Token

insert into UnitTokens (UnitId, Token, CreateToken, DocUnitPermissionId) 
	select UnitId, Token, CreateToken, DocUnitPermissionId 
	from @UnitTokens s
	where not exists (select null from UnitTokens t 
		where t.UnitId = s.UnitId and t.Token = s.Token and t.CreateToken = s.CreateToken and t.DocUnitPermissionId = s.DocUnitPermissionId)

delete from t
	from UnitTokens t
	where not exists (select null from @UnitTokens s
		where t.UnitId = s.UnitId and t.Token = s.Token and t.CreateToken = s.CreateToken and t.DocUnitPermissionId = s.DocUnitPermissionId)
	and t.UnitId in (select UnitId from @Units)
	and t.CreateToken in ('classification')
END
GO

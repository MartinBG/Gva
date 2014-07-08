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
	  ClassificationPermissionId int
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
insert into @UnitTokens(UnitId, Token, CreateToken, ClassificationPermissionId)
	select distinct u.UnitId, 'classification#' + CONVERT(varchar(10), ucpc.ClassificationId), 'classification', uc.ClassificationPermissionId
		from Units u 
			inner join UnitTypes ut  on u.UnitTypeId  = ut.UnitTypeId
		    CROSS APPLY dbo.fnGetParentUnits(u.UnitId) pu
			inner join UnitClassifications uc on uc.UnitId = pu.UnitId 
			CROSS APPLY dbo.fnGetParentClassifications(uc.ClassificationId) ucpc
		where u.IsActive = 1 
		and ut.Alias = 'Employee'
		and u.UnitId in (select UnitId from @Units)

--merge Token

insert into UnitTokens (UnitId, Token, CreateToken, ClassificationPermissionId) 
	select UnitId, Token, CreateToken, ClassificationPermissionId 
	from @UnitTokens s
	where not exists (select null from UnitTokens t 
		where t.UnitId = s.UnitId and t.Token = s.Token and t.CreateToken = s.CreateToken and t.ClassificationPermissionId = s.ClassificationPermissionId)

delete from t
	from UnitTokens t
	where not exists (select null from @UnitTokens s
		where t.UnitId = s.UnitId and t.Token = s.Token and t.CreateToken = s.CreateToken and t.ClassificationPermissionId = s.ClassificationPermissionId)
	and t.UnitId in (select UnitId from @Units)
	and t.CreateToken in ('classification')
END
GO

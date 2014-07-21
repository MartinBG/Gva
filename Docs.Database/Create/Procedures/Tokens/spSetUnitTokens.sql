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
--заместник
--union
--	select distinct su.SubstitutionUnitId, 'classification#' + CONVERT(varchar(10), ucpc.ClassificationId), 'classification'
--	, (select p.DocUnitPermissionId from DocUnitPermissions p where p.Alias = cr.Alias)
--		from Units u 
--			inner join UnitTypes ut  on u.UnitTypeId  = ut.UnitTypeId	
--			inner join UnitSubstitutions us on u.UnitId = us.UnitId
--			inner join Units u2 on u2.UnitId = su.SubstitutionUnitId
--			inner join UnitTypes ut2  on u2.UnitTypeId  = ut2.UnitTypeId	
--		    CROSS APPLY dbo.fnGetParentUnits(u.UnitId) pu
--			inner join UnitClassifications uc on uc.UnitId = pu.UnitId 
--			inner join ClassificationRoles cr on cr.ClassificationRoleId = uc.ClassificationRoleId 
--			CROSS APPLY dbo.fnGetParentClassifications(uc.ClassificationId) ucpc
--		where u.IsActive = 1 
--		and ut.Alias = 'Employee'
--		and u.UnitId in (select UnitId from @Units)
--		and cr.Alias <> 'Execution'
--		and su.IsActive = 1
--		and u.IsActive = 1
--		and ut2.Alias = 'Employee'

--merge Token
MERGE UnitTokens AS t
USING @UnitTokens AS s 
	ON t.UnitId = s.UnitId and t.Token = s.Token and t.CreateToken = s.CreateToken and t.ClassificationPermissionId = s.ClassificationPermissionId
WHEN NOT MATCHED BY TARGET THEN
	INSERT (UnitId, Token, CreateToken, ClassificationPermissionId)
	VALUES (s.UnitId, s.Token, s.CreateToken, s.ClassificationPermissionId)
WHEN NOT MATCHED BY SOURCE 
	AND t.CreateToken IN ('classification') 
	AND t.UnitId IN (select UnitId from @Units)
	THEN
    DELETE;

END
GO

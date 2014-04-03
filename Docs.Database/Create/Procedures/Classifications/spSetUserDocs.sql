--//? has to be re-done for the new permissions
print 'spSetUserDocs'
GO

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'spSetUserDocs'))
DROP PROCEDURE spSetUserDocs
GO

CREATE PROCEDURE spSetUserDocs
	  @UnitId int
AS
BEGIN
	SET NOCOUNT ON

		declare @currentDate datetime = getdate();

		DECLARE @Units TABLE (
			UnitId int,
			IsActive bit
		);

		DECLARE @ParentUnits TABLE (
			UnitId int
		);

		DECLARE @EmployeeUnits TABLE (
			UnitId int
		);

		DECLARE @UnitClassifications TABLE (
			UnitId int, 
			ClassificationId int,
			ClassificationRoleId int,
			ClassificationRoleAlias nvarchar(200)
		);

		DECLARE @DocClassificationRoles TABLE (
			DocId int,
			ClassificationRoleAlias nvarchar(200)
		);

		 DECLARE @EmployeUnitClassifications TABLE (
			DocId int, 
			UnitId int, 
			Alias nvarchar(200)
		);

		DECLARE @DocUnits TABLE (
			DocId int, 
			UnitId int, 
			DocUnitRoleId int
		);

		DECLARE @DocUsers TABLE (
			DocId int,
			UnitId int, 
			DocUnitPermissionId int
		);

		DECLARE @DeactivatedEmployeeUnits TABLE (
			UnitId int
		);
			
	--@Docs събиране на активните units
	insert into @Units (UnitId, IsActive)
		select UnitId, IsActive
		from Units
		where UnitId=@UnitId

	--@EmployeeUnits събиране на активните units, които са служители
	insert into @EmployeeUnits (UnitId)
	select su.UnitId
	from 
		@Units u 
		CROSS APPLY dbo.fnGetSubordinateUnits(u.UnitId) su
		inner join Units u2  on su.UnitId  = u2.UnitId 
		inner join UnitTypes ut  on u2.UnitTypeId  = ut.UnitTypeId
	where 
		ut.Alias = 'Employee' and
		u.UnitId = 1 and
		u2.IsActive = 1 --!

	--@ParentUnits събиране на активните parent unit-ите
	insert into @ParentUnits (UnitId)
	select upu.UnitId
	from @Units u
		CROSS APPLY dbo.fnGetParentUnits(u.UnitId) upu
		inner join Units u2  on upu.UnitId  = u2.UnitId 
	where 
		u.UnitId = 1 and
		u2.IsActive = 1 --!

	--@UnitClassifications събиране на класификациите, които са класирани по unit-ите в ParentUnits
	insert into @UnitClassifications (UnitId, ClassificationId, ClassificationRoleId, ClassificationRoleAlias)
	select pu.UnitId, usc.ClassificationId, cr.ClassificationRoleId, cr.Alias
	from @ParentUnits pu
		inner join UnitClassifications uc on uc.UnitId = pu.UnitId
		inner join ClassificationRoles cr on uc.ClassificationRoleId = cr.ClassificationRoleId 
		CROSS APPLY dbo.fnGetSubordinateClassifications(uc.ClassificationId) usc

	--@DocClassificationRoles събиране на документите, които са класирани по схемите от UnitClassifications
	insert into @DocClassificationRoles (DocId, ClassificationRoleAlias)
	select distinct dc.DocId, uc.ClassificationRoleAlias
	from @UnitClassifications uc
		inner join DocClassifications dc on uc.ClassificationId = dc.ClassificationId
		inner join Docs d on d.DocId = dc.DocId
	where 
		d.DocCasePartTypeId in (select DocCasePartTypeId from DocCasePartTypes where Alias in ('Public', 'Internal'))

	--@EmployeUnitClassifications събиране на служителите от @EmployeeUnits класирани по схемите
	insert into @EmployeUnitClassifications (DocId, UnitId, Alias)
	select dcr.DocId, eu.UnitId, dcr.ClassificationRoleAlias
	from @DocClassificationRoles dcr
		CROSS APPLY @EmployeeUnits eu



	--DocUnit събиране на адресатите
	insert into @DocUnits (DocId, UnitId, DocUnitRoleId)
		select du.DocId, dusu.UnitId, du.DocUnitRoleId
		from DocUnits du 
			inner join Docs d on du.DocId = d.DocId
			CROSS APPLY dbo.fnGetSubordinateUnits(du.UnitId) dusu
			inner join @EmployeeUnits eu on eu.UnitId=dusu.UnitId
		where 
			d.DocCasePartTypeId in (select DocCasePartTypeId from DocCasePartTypes where Alias in ('Public', 'Internal'))

	insert into @DocUnits (DocId, UnitId, DocUnitRoleId)
		select du.DocId, dusu.UnitId, du.DocUnitRoleId
		from DocUnits du 
			inner join Docs d on du.DocId = d.DocId
			CROSS APPLY dbo.fnGetSubordinateUnits(du.UnitId) dusu
			inner join @EmployeeUnits eu on eu.UnitId=dusu.UnitId
			inner join DocUnitRoles dur on du.DocUnitRoleId = dur.DocUnitRoleId 
		where 
			d.DocCasePartTypeId in (select DocCasePartTypeId from DocCasePartTypes where Alias in ('Control')) and
			dur.Alias in ('ImportedBy', 'MadeBy', 'Editors')



    insert into @DocUsers (DocId, UnitId, DocUnitPermissionId)
		select DocId, UnitId, (select DocUnitPermissionId from DocUnitPermissions where Alias = 'Read') 
			from @EmployeUnitClassifications where Alias = 'Read' 

    insert into @DocUsers (DocID, UnitId, DocUnitPermissionId)
		select DocId, UnitId, (select DocUnitPermissionId from DocUnitPermissions where Alias = 'Register') 
			from @EmployeUnitClassifications where Alias = 'Register' 
		union all select DocId, UnitId, (select DocUnitPermissionId from DocUnitPermissions where Alias = 'Read') 
			from @EmployeUnitClassifications where Alias = 'Register' 
			
    insert into @DocUsers (DocId, UnitId, DocUnitPermissionId)
		select DocId, UnitId, (select DocUnitPermissionId from DocUnitPermissions where Alias = 'Management') 
			from @EmployeUnitClassifications where Alias = 'Management' 
		union all select DocId, UnitId, (select DocUnitPermissionId from DocUnitPermissions where Alias = 'Read') 
			from @EmployeUnitClassifications where Alias = 'Management' 

    insert into @DocUsers (DocId, UnitId, DocUnitPermissionId)
		select DocId, UnitId, (select DocUnitPermissionId from DocUnitPermissions where Alias = 'ESign') 
			from @EmployeUnitClassifications where Alias = 'ESign' 
		union all select DocId, UnitId, (select DocUnitPermissionId from DocUnitPermissions where Alias = 'Read') 
			from @EmployeUnitClassifications where Alias = 'ESign' 

    insert into @DocUsers (DocId, UnitId, DocUnitPermissionId)
		select DocId, UnitId, (select DocUnitPermissionId from DocUnitPermissions where Alias = 'Finish') 
			from @EmployeUnitClassifications where Alias = 'Finish' 
		union all select DocId, UnitId, (select DocUnitPermissionId from DocUnitPermissions where Alias = 'Read') 
			from @EmployeUnitClassifications where Alias = 'Finish' 


    insert into @DocUsers (DocId, UnitId, DocUnitPermissionId)
		select DocId, UnitId, (select DocUnitPermissionId from DocUnitPermissions where Alias = 'Reverse') 
			from @EmployeUnitClassifications where Alias = 'Reverse' 
		union all select DocId, UnitId, (select DocUnitPermissionId from DocUnitPermissions where Alias = 'Read') 
			from @EmployeUnitClassifications where Alias = 'Reverse' 

	insert into @DocUsers (DocId, UnitId, DocUnitPermissionId)
		select uc.DocId, uc.UnitId, (select DocUnitPermissionId from DocUnitPermissions where Alias = 'Read') 
			from @EmployeUnitClassifications uc
				inner join @DocUnits du on uc.UnitId = du.UnitId and uc.DocId = du.DocId 
				inner join DocUnitRoles dur on du.DocUnitRoleId = dur.DocUnitRoleId 
			where uc.Alias = 'Execution' 

	insert into @DocUsers (DocId, UnitId, DocUnitPermissionId)
		select uc.DocId, uc.UnitId, (select DocUnitPermissionId from DocUnitPermissions where Alias = 'Edit') 
			from @EmployeUnitClassifications uc
				inner join @DocUnits du on uc.UnitId = du.UnitId and uc.DocId = du.DocId
				inner join DocUnitRoles dur on du.DocUnitRoleId = dur.DocUnitRoleId 
			where uc.Alias = 'Execution' 
				and dur.Alias in ('ImportedBy', 'MadeBy', 'Editors')
				
	insert into @DocUsers (DocId, UnitId, DocUnitPermissionId)
		select uc.DocId, uc.UnitId, (select DocUnitPermissionId from DocUnitPermissions where Alias = 'Finish') 
			from @EmployeUnitClassifications uc
				inner join @DocUnits du on uc.UnitId = du.UnitId and uc.DocId = du.DocId 
				inner join DocUnitRoles dur on du.DocUnitRoleId = dur.DocUnitRoleId 
			where uc.Alias = 'Execution' 
				and dur.Alias in ('ImportedBy', 'MadeBy', 'Editors')


	
	update du set du.IsActive = 0, DeactivateDate=@currentDate 
	from DocUsers du 
		where du.IsActive = 1
			and not exists (select null from @DocUsers newdu 
							where newdu.DocId = du.DocId
							and newdu.UnitId = du.UnitId 
							and newdu.DocUnitPermissionId = du.DocUnitPermissionId )
			and UnitId in (select UnitId from @EmployeeUnits)
			 			
	--print '--Reactivated'
	update du set du.IsActive = 1, ActivateDate=@currentDate 
	from DocUsers du 
		where du.IsActive = 0
			and exists (select null from @DocUsers newdu 
							where newdu.DocId = du.DocId
							and newdu.UnitId = du.UnitId 
							and newdu.DocUnitPermissionId = du.DocUnitPermissionId )
				and UnitId in (select UnitId from @EmployeeUnits)
			
	--print '--New active'
	insert into DocUsers (DocId, UnitId, DocUnitPermissionId, HasRead, IsActive, ActivateDate, DeactivateDate)
		select DocId, UnitID, DocUnitPermissionId, 0, 1, @currentDate, null 
			from (select distinct DocId, UnitId, DocUnitPermissionId from @DocUsers) newdu 
			where not exists (select null from DocUsers du
							where newdu.DocId = du.DocId
							and newdu.UnitId = du.UnitId 
							and newdu.DocUnitPermissionId = du.DocUnitPermissionId )


	--@DeactivatedEmployeeUnits събиране на неактивните units, които са служители
	insert into @DeactivatedEmployeeUnits (UnitId)
	select su.UnitId
	from 
		@Units u 
		CROSS APPLY dbo.fnGetSubordinateUnits(u.UnitId) su
		inner join Units u2  on su.UnitId  = u2.UnitId 
		inner join UnitTypes ut  on u2.UnitTypeId  = ut.UnitTypeId
	where 
		ut.Alias = 'Employee' and
		(u.UnitId = 0 or u2.IsActive = 0)

	--Премахване записи свързани с неактивни units
	update du set du.IsActive = 0, DeactivateDate=@currentDate 
	from DocUsers du 
		where du.IsActive = 1 and UnitId in (select UnitId from @DeactivatedEmployeeUnits)


	SET NOCOUNT OFF

    select ''
END

GO

print 'spSetDocUsers'
GO

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'spSetDocUsers'))
DROP PROCEDURE spSetDocUsers
GO

CREATE PROCEDURE spSetDocUsers
	  @DocId int
AS
BEGIN
	SET NOCOUNT ON

    declare @currentDate datetime = getdate();

    DECLARE @Docs TABLE (
		DocId int
	);

    DECLARE @DocUnits TABLE (
		DocId int, 
		UnitId int, 
		DocUnitRoleId int
	);
    
    DECLARE @DocClassifications TABLE (
      DocId int,
	  ClassificationId int 
	);
	
    DECLARE @UnitClassifications TABLE (
		DocId int, 
		UnitId int, 
		ClassificationRoleId int,
		Alias nvarchar(200)
	);

    DECLARE @DocUsers TABLE (
		DocId int,
		UnitId int, 
		DocUnitPermissionId int
	);

	--Docs събиране на документите
	insert into @Docs (DocId)
		select @DocId
			
	--DocUnit събиране на адресатите и надолу с подчинените, но само служители 
	insert into @DocUnits (DocId, UnitId, DocUnitRoleId)
		select du.DocId, dusu.UnitId, du.DocUnitRoleId
		from DocUnits du 
			inner join Docs d on du.DocId = d.DocId
			CROSS APPLY dbo.fnGetSubordinateUnits(du.UnitId) dusu
			inner join Units u  on dusu.UnitId  = u.UnitId 
			inner join UnitTypes ut  on u.UnitTypeId  = ut.UnitTypeId
		where d.DocCasePartTypeId in (select DocCasePartTypeId from DocCasePartTypes where Alias in ('Public', 'Internal'))
			and ut.Alias = 'Employee'
			and du.DocId in (select DocId from @Docs)
            and u.IsActive = 1 --!
			
	insert into @DocUnits (DocId, UnitId, DocUnitRoleId)
		select du.DocId, dusu.UnitId, du.DocUnitRoleId
		from DocUnits du 
			inner join Docs d on du.DocId = d.DocId
			CROSS APPLY dbo.fnGetSubordinateUnits(du.UnitId) dusu
			inner join Units u  on dusu.UnitId  = u.UnitId 
			inner join UnitTypes ut  on u.UnitTypeId  = ut.UnitTypeId
			inner join DocUnitRoles dur on du.DocUnitRoleId = dur.DocUnitRoleId 
		where d.DocCasePartTypeId in (select DocCasePartTypeId from DocCasePartTypes where Alias in ('Control'))
			and ut.Alias = 'Employee'
			and du.DocId in (select DocId from @Docs)
			and dur.Alias in ('ImportedBy', 'MadeBy', 'Editors')
            and u.IsActive = 1 --!


	--debug trace @DocUnits
	/*
	select * from @DocUnits d 
		inner join Units u on d.UnitId = u.UnitId
	*/

	--DocClassifications събиране на кл.схеми на преписката, и техните родители
    insert into @DocClassifications(DocId, ClassificationId)
		select d.DocId, dcpc.ClassificationId
		from Docs d 
			inner join DocRelations dr on d.DocId = dr.DocId
			inner join DocRelations drr on dr.RootDocId  = drr.RootDocId 
			inner join DocClassifications dc on drr.DocId = dc.DocId and dc.IsActive = 1 
			CROSS APPLY dbo.fnGetParentClassifications(dc.ClassificationId) dcpc
		where d.DocId in (select DocId from @Docs)

	--debug trace DocClassifications
	/*
	select * from @DocClassifications d
		inner join Classifications c on  d.ClassificationId = c.ClassificationId 
	*/

	--UnitClassifications събиране на звената по кл.схемите и надолу с подчинените, но само служители 
	insert into @UnitClassifications(DocId, UnitId, ClassificationRoleId, Alias)
		select distinct c.DocId, ucsu.UnitId, uc.ClassificationRoleId, cr.Alias   
		from @DocClassifications c 
			inner join UnitClassifications uc on c.ClassificationId = uc.ClassificationId 
			inner join ClassificationRoles cr on uc.ClassificationRoleId = cr.ClassificationRoleId 
			CROSS APPLY dbo.fnGetSubordinateUnits(uc.UnitId) ucsu
			inner join Units u  on ucsu.UnitId  = u.UnitId 
			inner join UnitTypes ut  on u.UnitTypeId  = ut.UnitTypeId
			where ut.Alias = 'Employee'
            and u.IsActive = 1 --!

	--debug trace UnitClassifications
	/*
	select u.*, r.* from @UnitClassifications d
		inner join Units u on d.UnitId = u.UnitId 
		inner join ClassificationRoles r on r.ClassificationRoleId = d.ClassificationRoleId 
	order by u.Name 
	*/

    insert into @DocUsers (DocId, UnitId, DocUnitPermissionId)
		select DocId, UnitId, (select DocUnitPermissionId from DocUnitPermissions where Alias = 'Read') 
			from @UnitClassifications where Alias = 'Read' 

    insert into @DocUsers (DocID, UnitId, DocUnitPermissionId)
		select DocId, UnitId, (select DocUnitPermissionId from DocUnitPermissions where Alias = 'Register') 
			from @UnitClassifications where Alias = 'Register' 
		union all select DocId, UnitId, (select DocUnitPermissionId from DocUnitPermissions where Alias = 'Read') 
			from @UnitClassifications where Alias = 'Register' 
			
    insert into @DocUsers (DocId, UnitId, DocUnitPermissionId)
		select DocId, UnitId, (select DocUnitPermissionId from DocUnitPermissions where Alias = 'Management') 
			from @UnitClassifications where Alias = 'Management' 
		union all select DocId, UnitId, (select DocUnitPermissionId from DocUnitPermissions where Alias = 'Read') 
			from @UnitClassifications where Alias = 'Management' 

    insert into @DocUsers (DocId, UnitId, DocUnitPermissionId)
		select DocId, UnitId, (select DocUnitPermissionId from DocUnitPermissions where Alias = 'ESign') 
			from @UnitClassifications where Alias = 'ESign' 
		union all select DocId, UnitId, (select DocUnitPermissionId from DocUnitPermissions where Alias = 'Read') 
			from @UnitClassifications where Alias = 'ESign' 

    insert into @DocUsers (DocId, UnitId, DocUnitPermissionId)
		select DocId, UnitId, (select DocUnitPermissionId from DocUnitPermissions where Alias = 'Finish') 
			from @UnitClassifications where Alias = 'Finish' 
		union all select DocId, UnitId, (select DocUnitPermissionId from DocUnitPermissions where Alias = 'Read') 
			from @UnitClassifications where Alias = 'Finish' 

    insert into @DocUsers (DocId, UnitId, DocUnitPermissionId)
		select DocId, UnitId, (select DocUnitPermissionId from DocUnitPermissions where Alias = 'Reverse') 
			from @UnitClassifications where Alias = 'Reverse' 
		union all select DocId, UnitId, (select DocUnitPermissionId from DocUnitPermissions where Alias = 'Read') 
			from @UnitClassifications where Alias = 'Reverse' 

	--new permissions--
	insert into @DocUsers (DocId, UnitId, DocUnitPermissionId)
		select DocId, UnitId, (select DocUnitPermissionId from DocUnitPermissions where Alias = 'SubstituteManagement') 
			from @UnitClassifications where Alias = 'SubstituteManagement' 
		union all select DocId, UnitId, (select DocUnitPermissionId from DocUnitPermissions where Alias = 'Read') 
			from @UnitClassifications where Alias = 'SubstituteManagement' 

	insert into @DocUsers (DocId, UnitId, DocUnitPermissionId)
		select DocId, UnitId, (select DocUnitPermissionId from DocUnitPermissions where Alias = 'DeleteManagement') 
			from @UnitClassifications where Alias = 'DeleteManagement' 
		union all select DocId, UnitId, (select DocUnitPermissionId from DocUnitPermissions where Alias = 'Read') 
			from @UnitClassifications where Alias = 'DeleteManagement' 

	insert into @DocUsers (DocId, UnitId, DocUnitPermissionId)
		select DocId, UnitId, (select DocUnitPermissionId from DocUnitPermissions where Alias = 'EditTech') 
			from @UnitClassifications where Alias = 'EditTech' 
		union all select DocId, UnitId, (select DocUnitPermissionId from DocUnitPermissions where Alias = 'Read') 
			from @UnitClassifications where Alias = 'EditTech' 

	insert into @DocUsers (DocId, UnitId, DocUnitPermissionId)
		select DocId, UnitId, (select DocUnitPermissionId from DocUnitPermissions where Alias = 'EditTechElectronicServiceStage') 
			from @UnitClassifications where Alias = 'EditTechElectronicServiceStage' 
		union all select DocId, UnitId, (select DocUnitPermissionId from DocUnitPermissions where Alias = 'Read') 
			from @UnitClassifications where Alias = 'EditTechElectronicServiceStage' 

	insert into @DocUsers (DocId, UnitId, DocUnitPermissionId)
		select DocId, UnitId, (select DocUnitPermissionId from DocUnitPermissions where Alias = 'DocCasePartManagement') 
			from @UnitClassifications where Alias = 'DocCasePartManagement' 
		union all select DocId, UnitId, (select DocUnitPermissionId from DocUnitPermissions where Alias = 'Read') 
			from @UnitClassifications where Alias = 'DocCasePartManagement' 
	--end of new permissions--

	insert into @DocUsers (DocId, UnitId, DocUnitPermissionId)
		select uc.DocId, uc.UnitId, (select DocUnitPermissionId from DocUnitPermissions where Alias = 'Read') 
			from @UnitClassifications uc
				inner join @DocUnits du on uc.UnitId = du.UnitId and uc.DocId = du.DocId 
				inner join DocUnitRoles dur on du.DocUnitRoleId = dur.DocUnitRoleId 
			where uc.Alias = 'Execution' 

	insert into @DocUsers (DocId, UnitId, DocUnitPermissionId)
		select uc.DocId, uc.UnitId, (select DocUnitPermissionId from DocUnitPermissions where Alias = 'Edit') 
			from @UnitClassifications uc
				inner join @DocUnits du on uc.UnitId = du.UnitId and uc.DocId = du.DocId
				inner join DocUnitRoles dur on du.DocUnitRoleId = dur.DocUnitRoleId 
			where uc.Alias = 'Execution' 
				and dur.Alias in ('ImportedBy', 'MadeBy', 'Editors')
				
	insert into @DocUsers (DocId, UnitId, DocUnitPermissionId)
		select uc.DocId, uc.UnitId, (select DocUnitPermissionId from DocUnitPermissions where Alias = 'Finish') 
			from @UnitClassifications uc
				inner join @DocUnits du on uc.UnitId = du.UnitId and uc.DocId = du.DocId 
				inner join DocUnitRoles dur on du.DocUnitRoleId = dur.DocUnitRoleId 
			where uc.Alias = 'Execution' 
				and dur.Alias in ('ImportedBy', 'MadeBy', 'Editors')
	--debug trace @DocUsers
	/*	
	select distinct u.*, p.* from @DocUsers d 
		inner join Units u on d.UnitId = u.UnitId 
		inner join DocUnitPermissions p on p.DocUnitPermissionId = d.DocUnitPermissionId 
	*/
	--print '--Deactivated'		
	update du set du.IsActive = 0, DeactivateDate=@currentDate 
	from DocUsers du 
		where du.IsActive = 1
			and not exists (select null from @DocUsers newdu 
							where newdu.DocId = du.DocId
							and newdu.UnitId = du.UnitId 
							and newdu.DocUnitPermissionId = du.DocUnitPermissionId )
			and DocId in (select DocId from @Docs) 
			 			
	--print '--Reactivated'
	update du set du.IsActive = 1, ActivateDate=@currentDate 
	from DocUsers du 
		where du.IsActive = 0
			and exists (select null from @DocUsers newdu 
							where newdu.DocId = du.DocId
							and newdu.UnitId = du.UnitId 
							and newdu.DocUnitPermissionId = du.DocUnitPermissionId )
				and DocId in (select DocId from @Docs)
			
	--print '--New active'
	insert into DocUsers (DocId, UnitId, DocUnitPermissionId, HasRead, IsActive, ActivateDate, DeactivateDate)
		select DocId, UnitID, DocUnitPermissionId, 0, 1, @currentDate, null 
			from (select distinct DocId, UnitId, DocUnitPermissionId from @DocUsers) newdu 
			where not exists (select null from DocUsers du
							where newdu.DocId = du.DocId
							and newdu.UnitId = du.UnitId 
							and newdu.DocUnitPermissionId = du.DocUnitPermissionId )

	SET NOCOUNT OFF

    select ''
END

GO

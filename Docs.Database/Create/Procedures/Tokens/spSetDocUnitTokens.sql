IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'spSetDocUnitTokens'))
DROP PROCEDURE spSetDocUnitTokens
GO

CREATE PROCEDURE spSetDocUnitTokens
	@DocId int,
	@AllCase bit = 0
AS
BEGIN

DECLARE @Docs TABLE (
		DocId int
	);

DECLARE @DocTokens TABLE (
		DocId int,
		Token nvarchar(200),
		DocCasePartTypeId int
	);

DECLARE @CaseTokens TABLE (
		RootDocId int,
		DocId int, 
		Token nvarchar(200) 
	);

DECLARE @UnitTokens TABLE (
      UnitId int,
	  Token nvarchar(200), 
	  CreateToken nvarchar(200),
	  ClassificationPermissionId int
	);

--@Docs инициализиране на таблицата с документите
	if (@AllCase = 0)
	begin
		insert into @Docs(DocId)
			select DocId from docs
				where (DocId = @DocId or @DocId is null)
	end 
	else
	begin
		insert into @Docs(DocId)
			select drr.DocId
				from DocRelations drr
				where drr.RootDocId in (
					select dr.RootDocId 
					from DocRelations dr
					where dr.DocId = @DocId
					)	
	end

insert into @DocTokens (DocId, Token, DocCasePartTypeId)
	select d.DocId, 'doc#' + CONVERT(varchar(10), d.DocId), d2.DocCasePartTypeId 
	from @Docs d 
		inner join Docs d2 on d.DocId = d2.DocId

insert into @CaseTokens(RootDocId, DocId, Token)
	select drr.RootDocId, drr.DocId, 'doc#' + CONVERT(varchar(10), drr.DocId)
	from DocRelations drr
	where drr.RootDocId in (
		select dr.RootDocId 
		from @Docs d 
			inner join DocRelations dr on d.DocId = dr.DocId
		)

--определяне на token за четене на документ, за листата на звената по документа
insert into @UnitTokens(UnitId, Token, CreateToken, ClassificationPermissionId)
	select s1.UnitId, s1.Token, s1.CreateToken, s2.ClassificationPermissionId
	from (
		select distinct u.UnitId, d.Token, 'doc' as CreateToken
		from @DocTokens d
			inner join DocUnits du on du.DocID = d.DocID
			CROSS APPLY dbo.fnGetSubordinateUnits(du.UnitId) dusu
			inner join Units u on dusu.UnitId  = u.UnitId 
			inner join UnitTypes ut  on u.UnitTypeId  = ut.UnitTypeId
		where d.DocCasePartTypeId in (select DocCasePartTypeId from DocCasePartTypes where Alias in ('Public', 'Internal'))
			and u.IsActive = 1 
			and ut.Alias = 'Employee'
		) s1, 
	(
		select ClassificationPermissionId 
		from ClassificationPermissions 
		where 
		Alias = 'Read'
		) s2
	union 
	--определяне на token за редакция на документ, за листата на звената създали документа
	select s1.UnitId, s1.Token, s1.CreateToken, s2.ClassificationPermissionId
	from (
		select distinct u.UnitId, d.Token, 'doc' as CreateToken
		from @DocTokens d
			inner join DocUnits du on du.DocID = d.DocID
			inner join DocUnitRoles dur on du.DocUnitRoleId = dur.DocUnitRoleId 
			CROSS APPLY dbo.fnGetSubordinateUnits(du.UnitId) dusu
			inner join Units u on dusu.UnitId  = u.UnitId 
			inner join UnitTypes ut  on u.UnitTypeId  = ut.UnitTypeId
		where 
			dur.Alias in ('ImportedBy', 'MadeBy', 'Editors')
			and u.IsActive = 1 
			and ut.Alias = 'Employee'
		) s1, 
	(
		select ClassificationPermissionId 
		from ClassificationPermissions 
		where 
		Alias in ('Read', 'Edit')
		) s2

--определяне на token за четене на преписката, за листата на звената по документите резолюция и задача
insert into @UnitTokens(UnitId, Token, CreateToken, ClassificationPermissionId)
	select s1.UnitId, s1.Token, s1.CreateToken, s2.ClassificationPermissionId
	from (
		select distinct u.UnitId, 'doc#' + CONVERT(varchar(10), dr2.DocId) as Token, 'case' as CreateToken
		from @CaseTokens c
			inner join Docs specd on specd.DocId = c.DocId
			inner join DocUnits du on du.DocID = specd.DocID
			inner join DocUnitRoles dur on du.DocUnitRoleId = dur.DocUnitRoleId 
			CROSS APPLY dbo.fnGetSubordinateUnits(du.UnitId) dusu
			inner join Units u on dusu.UnitId  = u.UnitId 
			inner join UnitTypes ut  on u.UnitTypeId  = ut.UnitTypeId
			--CROSS APPLY dbo.fnGetParentUnits(u.UnitId) pu
			--inner join UnitClassifications uc on uc.UnitId = pu.UnitId 
			--CROSS APPLY dbo.fnGetParentClassifications(uc.ClassificationId) ucpc
			inner join DocRelations dr2 on dr2.RootDocId = c.RootDocId
			--inner join DocClassifications dc on dc.DocID = dr2.DocID
			--CROSS APPLY dbo.fnGetParentClassifications(dc.ClassificationId) dcpc
		where 
			specd.DocEntryTypeId in (select DocEntryTypeId from DocEntryTypes where Alias in ('Resolution', 'Task'))
			and specd.DocCasePartTypeId in (select DocCasePartTypeId from DocCasePartTypes where Alias in ('Public', 'Internal'))
			and dur.Alias in ('InCharge', 'Controlling')
			and u.IsActive = 1 
			and ut.Alias = 'Employee'
			--and ucpc.ClassificationId = dcpc.ClassificationId
		) s1, 
	(
		select ClassificationPermissionId 
		from ClassificationPermissions 
		where 
		Alias = 'Read'
		) s2

--merge Token
MERGE UnitTokens AS t
USING @UnitTokens AS s 
	ON t.UnitId = s.UnitId and t.Token = s.Token and t.CreateToken = s.CreateToken and t.ClassificationPermissionId = s.ClassificationPermissionId
WHEN NOT MATCHED BY TARGET THEN
	INSERT (UnitId, Token, CreateToken, ClassificationPermissionId)
	VALUES (s.UnitId, s.Token, s.CreateToken, s.ClassificationPermissionId)
WHEN NOT MATCHED BY SOURCE 
	AND ((t.CreateToken IN ('doc') AND t.Token IN (select token from @DocTokens)) OR (t.CreateToken IN ('case') AND t.Token IN (select token from @CaseTokens)))
	THEN
    DELETE;

END
GO


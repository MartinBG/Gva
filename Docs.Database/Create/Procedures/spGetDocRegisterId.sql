print 'spGetDocRegisterId'
GO

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'spGetDocRegisterId'))
DROP PROCEDURE spGetDocRegisterId
GO

CREATE PROCEDURE spGetDocRegisterId
		@DocId int
AS
BEGIN
	declare @RegisterIndexID int
	declare @Year int
	
	--------------------------------------
	--get case RegisterIndex and Year
	declare @IsRoot int
	declare @PrimaryRegisterIndexID int
	declare @CasePrimaryRegisterIndexID int
    declare @CaseSecondaryRegisterIndexID int
	select 
		  @Year = datepart(yyyy,getdate())
		, @IsRoot = case when cased.DocId = d.DocId then 1 else 0 end 
		, @CasePrimaryRegisterIndexID = casedt.PrimaryRegisterIndexID
		, @CaseSecondaryRegisterIndexID = casedt.SecondaryRegisterIndexID
		, @PrimaryRegisterIndexID = dt.PrimaryRegisterIndexID
	from Docs d
			inner join DocTypes dt on d.DocTypeId = dt.DocTypeId 
			inner join DocRelations dr on d.DocID = dr.DocId 
			inner join Docs cased on dr.RootDocId = cased.DocId
			inner join DocTypes casedt on cased.DocTypeId = casedt.DocTypeId 
			
		where 
			d.DocID = @DocId
	IF @IsRoot = 1
	BEGIN
		IF @PrimaryRegisterIndexID is null
		BEGIN
		--Неопределен тип : Определяме на общия регистърен идекс
			select @RegisterIndexID = RegisterIndexID from RegisterIndexes where Alias = N'Others'
		END
		ELSE 
		BEGIN
			--Първи документ от преписка : Определяме от типа на документ PrimaryRegisterIndexID
			select @RegisterIndexID = @PrimaryRegisterIndexID
		END
	END
	ELSE
	BEGIN
		IF @CaseSecondaryRegisterIndexID is null
		BEGIN
			select @RegisterIndexID = @PrimaryRegisterIndexID
		END
		ELSE
		BEGIN
			select @RegisterIndexID = @CaseSecondaryRegisterIndexID	
		END
	END

	--------------------------------------
	--Определяне на регистъра на документа : RegisterIndex.Code-Year
	--@RegisterIndexID, @Year
	declare @DocRegisterID int
	declare @DocRegisterAlias as nvarchar(200)	
	select @DocRegisterAlias = (select Code from RegisterIndexes where @RegisterIndexID = RegisterIndexID ) + '-' + CONVERT(NVARCHAR(4), @Year)
	select @DocRegisterID = DocRegisterID from DocRegisters where Alias = @DocRegisterAlias
	IF @DocRegisterID is null
	BEGIN
    --Регистърът на документа не е инициализиран за текущата година
		insert into DocRegisters(RegisterIndexId, Alias, CurrentNumber)
			select @RegisterIndexID, @DocRegisterAlias, 0
		select @DocRegisterID = SCOPE_IDENTITY()
	END

	SELECT @DocRegisterID as Result;
END

GO
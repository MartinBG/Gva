print 'spGetDocRegisterIdByRegisterIndexId'
GO

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'spGetDocRegisterIdByRegisterIndexId'))
DROP PROCEDURE spGetDocRegisterIdByRegisterIndexId
GO

CREATE PROCEDURE spGetDocRegisterIdByRegisterIndexId
		@RegisterIndexId int
AS
BEGIN
	declare @Year int = datepart(yyyy,getdate())

	--------------------------------------
	--Определяне на регистъра на документа : RegisterIndex.Code-Year
	--@RegisterIndexID, @Year
	declare @DocRegisterID int
	declare @DocRegisterAlias as nvarchar(200)	
	select @DocRegisterAlias = (select Code from RegisterIndexes where @RegisterIndexId = RegisterIndexId ) + '-' + CONVERT(NVARCHAR(4), @Year)
	select @DocRegisterID = DocRegisterID from DocRegisters where Alias = @DocRegisterAlias
	IF @DocRegisterID is null
	BEGIN
    --Регистърът на документа не е инициализиран за текущата година
		insert into DocRegisters(RegisterIndexId, Alias, CurrentNumber)
			select @RegisterIndexId, @DocRegisterAlias, 0
		select @DocRegisterID = SCOPE_IDENTITY()
	END

	SELECT @DocRegisterID as Result;
END

GO
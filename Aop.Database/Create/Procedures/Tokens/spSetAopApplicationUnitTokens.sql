IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'spSetAopApplicationUnitTokens'))
DROP PROCEDURE spSetAopApplicationUnitTokens
GO

CREATE PROCEDURE spSetAopApplicationUnitTokens
	@AopApplicationId int
AS
BEGIN

DECLARE @AopApplications TABLE (
		AopApplicationId int
	);

DECLARE @AopApplicationTokens TABLE (
		AopApplicationId int,
		Token nvarchar(200)
	);


DECLARE @UnitTokens TABLE (
      UnitId int,
	  Token nvarchar(200), 
	  CreateToken nvarchar(200),
	  ClassificationPermissionId int
	);

--@AopApplications инициализиране на таблицата с обектите
	insert into @AopApplications(AopApplicationId)
		select AopApplicationId from AopApplications
			where (AopApplicationId = @AopApplicationId or @AopApplicationId is null)

--Token на текущ документ
insert into @AopApplicationTokens (AopApplicationId, Token)
	select d.AopApplicationId, 'AopApplication#' + CONVERT(varchar(10), d.AopApplicationId)
	from @AopApplications d 


--определяне на token за четене на документ, за листата на звената по документа
insert into @UnitTokens(UnitId, Token, CreateToken, ClassificationPermissionId)
	select s1.UnitId, s1.Token, s1.CreateToken, s2.ClassificationPermissionId
	from (
		select distinct u.UnitId, d.Token, 'AopApplication' as CreateToken
		from @AopApplicationTokens d
			inner join AopApplications du on du.AopApplicationID = d.AopApplicationID
			CROSS APPLY dbo.fnGetSubordinateUnits(du.CreateUnitId) dusu
			inner join Units u on dusu.UnitId  = u.UnitId 
			inner join UnitTypes ut  on u.UnitTypeId  = ut.UnitTypeId
		where u.IsActive = 1 
			and ut.Alias = 'Employee'
		) s1, 
	(
		select ClassificationPermissionId 
		from ClassificationPermissions 
		where 
		Alias in ('Read', 'Edit')
		) s2

--merge Token
MERGE UnitTokens AS t
USING @UnitTokens AS s 
    ON t.UnitId = s.UnitId and t.Token = s.Token and t.CreateToken = s.CreateToken and t.ClassificationPermissionId = s.ClassificationPermissionId
WHEN NOT MATCHED BY TARGET THEN
    INSERT (UnitId, Token, CreateToken, ClassificationPermissionId)
    VALUES (s.UnitId, s.Token, s.CreateToken, s.ClassificationPermissionId)
WHEN NOT MATCHED BY SOURCE 
    AND (
        (t.CreateToken IN ('AopApplication') AND t.Token IN (select token from @AopApplicationTokens)) 
        )
    THEN
    DELETE;

END
GO

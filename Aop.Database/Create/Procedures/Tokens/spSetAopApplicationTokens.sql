IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'spSetAopApplicationTokens'))
DROP PROCEDURE spSetAopApplicationTokens
GO

CREATE PROCEDURE spSetAopApplicationTokens
	@AopApplicationId int
AS
BEGIN

DECLARE @AopApplications TABLE (
		AopApplicationId int
	);

DECLARE @AopApplicationTokens TABLE (
      AopApplicationId int,
	  Token nvarchar(200),
	  CreateToken nvarchar(200)
	);

--@AopApplications инициализиране на таблицата с обектите
insert into @AopApplications(AopApplicationId)
	select AopApplicationId from AopApplications
		where (AopApplicationId = @AopApplicationId or @AopApplicationId is null)

--определяне на token на обектите
insert into @AopApplicationTokens(AopApplicationId, Token, CreateToken)
	--token за схемите по които е класиран документа, като включваме класификациите на горните нива
	select distinct dc.AopApplicationId, 'classification#' + CONVERT(varchar(10), dcpc.ClassificationId), 'classification'
		from (select d.AopApplicationId, c.ClassificationId from @AopApplications d, Classifications c where c.Alias in ('AopApplication')) dc 
			CROSS APPLY dbo.fnGetParentClassifications(dc.ClassificationId) dcpc
	union all 
	--token за самия обект
	select distinct AopApplicationId, 'AopApplication#' + CONVERT(varchar(10), AopApplicationId), 'AopApplication'
		from @AopApplications 


--merge Token
MERGE AopApplicationTokens AS t
USING @AopApplicationTokens AS s 
	ON t.AopApplicationId = s.AopApplicationId and t.Token = s.Token and t.CreateToken = s.CreateToken
WHEN NOT MATCHED BY TARGET THEN
	INSERT (AopApplicationId, Token, CreateToken)
	VALUES (s.AopApplicationId, s.Token, s.CreateToken)
WHEN NOT MATCHED BY SOURCE AND t.AopApplicationId in (select AopApplicationId from @AopApplications) THEN
    DELETE;

END
GO

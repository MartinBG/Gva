IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'spSetDocTokens'))
DROP PROCEDURE spSetDocTokens
GO

CREATE PROCEDURE spSetDocTokens
	@DocId int
AS
BEGIN

DECLARE @Docs TABLE (
		DocId int
	);

DECLARE @DocTokens TABLE (
      DocId int,
	  Token nvarchar(200),
	  CreateToken nvarchar(200)
	);

--@Docs инициализиране на таблицата с документите
insert into @Docs(DocId)
	select DocId from docs
		where (DocId = @DocId or @DocId is null)

--определяне на token на документа
insert into @DocTokens(DocId, Token, CreateToken)
	--token за схемите по които е класиран документа, като включваме класификациите на горните нива
	select distinct dc.DocId, 'classification#' + CONVERT(varchar(10), dcpc.ClassificationId), 'classification'
		from DocClassifications dc 
			CROSS APPLY dbo.fnGetParentClassifications(dc.ClassificationId) dcpc
		where dc.IsActive = 1 
		and dc.DocId in (select DocId from @Docs)
	union all 
	--token за самия документ
	select distinct DocId, 'doc#' + CONVERT(varchar(10), DocId), 'doc'
		from @Docs 
	union all 
	--token за преписката на документа
	select distinct dr.DocId, 'case#' + CONVERT(varchar(10), dr.RootDocId), 'case'
		from DocRelations dr
		where dr.DocId in (select DocId from @Docs)


--merge Token
insert into DocTokens (DocId, Token, CreateToken) 
	select DocId, Token, CreateToken
	from @DocTokens s
	where not exists (select null from DocTokens t 
		where t.DocId = s.DocId and t.Token = s.Token and t.CreateToken = s.CreateToken)

delete from t
	from DocTokens t
	where not exists (select null from @DocTokens s
		where t.DocId = s.DocId and t.Token = s.Token and t.CreateToken = s.CreateToken)
	and t.DocId in (select DocId from @Docs)

END
GO


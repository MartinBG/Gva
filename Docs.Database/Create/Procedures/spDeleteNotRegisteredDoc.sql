print 'spDeleteNotRegisteredDoc'
GO

IF  EXISTS (SELECT * FROM sys.procedures WHERE object_id = OBJECT_ID(N'spDeleteNotRegisteredDoc'))
DROP PROCEDURE spDeleteNotRegisteredDoc
GO

CREATE PROCEDURE spDeleteNotRegisteredDoc
		@Storage nvarchar(200)
	  , @DocId int
	  , @CheckChildDocs bit
AS
BEGIN
	
	SET NOCOUNT ON

	declare @returnValue nvarchar(200) = '';
	
	if (@CheckChildDocs=1)
	begin
		set @returnValue = dbo.fnCheckForRegisteredChildDocs(@DocId);
	end

	if (@returnValue = '')
	begin
		
		declare @childDocId int;
		declare @lastCheckedDocId int = NULL;

		while(1=1)
		begin
			
			if (@lastCheckedDocId is null)
			begin
				set @childDocId = (select top 1 dr.DocId from DocRelations dr where dr.ParentDocId=@DocId order by dr.DocId);
			end
			else
			begin
				set @childDocId = (select top 1 dr.DocId from DocRelations dr where dr.ParentDocId=@DocId and dr.DocId > @lastCheckedDocId order by dr.DocId)
			end

			if (@childDocId is not null)
			begin
				exec dbo.spDeleteNotRegisteredDoc @Storage, @childDocId, 0;

				set @lastCheckedDocId = @childDocId;
			end
			else
			begin
				break;
			end
		end



		delete from DocRelations where DocId=@DocId
		delete from DocFiles where DocId=@DocId
		delete from DocIncomingDocs where DocId=@DocId
		delete from DocUsers where DocId=@DocId
		delete from DocWorkflows where DocId=@DocId
		delete from DocClassifications where DocId=@DocId
		delete from DocElectronicServiceStages where DocId=@DocId
		delete from DocUnits where DocId=@DocId
		delete from DocCorrespondentContacts where DocId=@DocId
		delete from DocCorrespondents where DocId=@DocId
		delete from Docs where DocId=@DocId

		create table #dummy_temp (deleteRows bit null)
		insert into #dummy_temp exec dbo.spDeleteDocFileContentByDocId @Storage, @DocId;
		drop table #dummy_temp

	end

	SET NOCOUNT OFF

	select @returnValue;
END

GO

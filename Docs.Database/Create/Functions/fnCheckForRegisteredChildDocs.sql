IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fnCheckForRegisteredChildDocs]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fnCheckForRegisteredChildDocs]
GO


CREATE FUNCTION [dbo].[fnCheckForRegisteredChildDocs]
(
	@DocId int
)
RETURNS NVARCHAR(200)
AS
BEGIN
	
	declare @returnValue NVARCHAR(200) = ''; 
	declare @docExist bit = case when (select count(*) from Docs d where d.DocId=@DocId) > 0 then 1 else 0 end;
	declare @regUri nvarchar(200) = (select d.RegUri from Docs d where d.DocId=@DocId);

	if (@docExist=0)
	begin
		set @returnValue = 'Документ с DocId=' + convert(nvarchar(200), @DocId)+ ' не съществува.';
	end
	else if (@regUri is not null)
	begin
		set @returnValue = 'Документът с DocId=' + convert(nvarchar(200), @DocId)+ ' e регистриран.';
	end
	else
	begin

		declare @childDocId int;
		declare @childDocRegStatus nvarchar(200);
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

				set @lastCheckedDocId = @childDocId;
				set @childDocRegStatus = [dbo].[fnCheckForRegisteredChildDocs](@childDocId);

				if (@childDocRegStatus != '')
				begin
					set @returnValue = @childDocRegStatus;
					break;
				end

			end
			else
			begin
				break;
			end
		end
	end

	return @returnValue;
END


GO
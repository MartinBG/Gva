IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fnGetCorrespondentDisplayName]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fnGetCorrespondentDisplayName]
GO


CREATE FUNCTION [dbo].[fnGetCorrespondentDisplayName]
(
	@CorrespondentTypeId		int,
	@BgCitizenFirstName			NVARCHAR (200),
	@BgCitizenLastName			NVARCHAR (200),
	@BgCitizenUIN				NVARCHAR (50),
	@ForeignerFirstName			NVARCHAR (200),
	@ForeignerLastName			NVARCHAR (200),
	@LegalEntityName			NVARCHAR (200),
	@LegalEntityBulstat			NVARCHAR (50),
	@FLegalEntityName			NVARCHAR (200)
)
RETURNS NVARCHAR(400)
WITH SCHEMABINDING
AS
BEGIN
	DECLARE @ReturnValue		NVARCHAR(400) = '';
	
	if (@CorrespondentTypeId = 1)
	begin
		set @ReturnValue = LTRIM(RTRIM(ISNULL(@BgCitizenFirstName, ''))) + ' ' + LTRIM(RTRIM(ISNULL(@BgCitizenLastName, ''))) + ' ' + LTRIM(RTRIM(ISNULL(@BgCitizenUIN, '')));
	end
	else if (@CorrespondentTypeId = 2)
	begin
		set @ReturnValue = LTRIM(RTRIM(ISNULL(@ForeignerFirstName, ''))) + ' ' + LTRIM(RTRIM(ISNULL(@ForeignerLastName, '')));
	end
	else if (@CorrespondentTypeId = 3)
	begin
		set @ReturnValue = LTRIM(RTRIM(ISNULL(@LegalEntityName, ''))) + ' ' + LTRIM(RTRIM(ISNULL(@LegalEntityBulstat, '')));
	end
	else if (@CorrespondentTypeId = 4)
	begin
		set @ReturnValue = LTRIM(RTRIM(ISNULL(@FLegalEntityName, '')));
	end

	RETURN LTRIM(RTRIM(@ReturnValue));
END


GO
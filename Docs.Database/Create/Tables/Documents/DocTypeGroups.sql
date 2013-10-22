print 'DocTypeGroups'
GO 

CREATE TABLE DocTypeGroups
(
    DocTypeGroupId	INT            IDENTITY (1, 1) NOT NULL,
    Name			NVARCHAR (200) NOT NULL,
	IsElectronicService	BIT        NOT NULL,
    IsActive		BIT            NOT NULL,
    Version			ROWVERSION     NOT NULL,
    CONSTRAINT PK_DocTypeGroups PRIMARY KEY CLUSTERED (DocTypeGroupId)

)
GO 

exec spDescTable  N'DocTypeGroups', N'Групи типове документи.'
exec spDescColumn N'DocTypeGroups', N'DocTypeGroupId', N'Уникален системно генериран идентификатор.'

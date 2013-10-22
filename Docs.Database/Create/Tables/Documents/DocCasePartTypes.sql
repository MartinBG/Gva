print 'DocCasePartTypes'
GO 

CREATE TABLE DocCasePartTypes
(
    DocCasePartTypeId		INT            IDENTITY (1, 1) NOT NULL,
    Name				NVARCHAR (200) NOT NULL,
	Alias				NVARCHAR (200) NOT NULL,
	Description			NVARCHAR (MAX) NULL,
    IsActive		BIT            NOT NULL,
    Version				ROWVERSION     NOT NULL,
    CONSTRAINT PK_DocCasePartTypes PRIMARY KEY CLUSTERED (DocCasePartTypeId),
)
GO 

exec spDescTable  N'DocCasePartTypes', N'Номенклатура'
exec spDescColumn N'DocCasePartTypes', N'DocCasePartTypeId', N'Уникален системно генериран идентификатор.'
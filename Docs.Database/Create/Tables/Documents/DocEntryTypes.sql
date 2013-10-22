print 'DocEntryTypes'
GO 

CREATE TABLE DocEntryTypes
(
    DocEntryTypeId		INT            IDENTITY (1, 1) NOT NULL,
    Name				NVARCHAR (200) NOT NULL,
	Alias				NVARCHAR (200) NOT NULL,
    IsActive			BIT            NOT NULL,
    Version				ROWVERSION     NOT NULL,
    CONSTRAINT PK_DocEntryTypes PRIMARY KEY CLUSTERED (DocEntryTypeId),
)
GO 

exec spDescTable  N'DocEntryTypes', N'Съдържание на формата на документа Документ, Задача и т.н.'
exec spDescColumn N'DocEntryTypes', N'DocEntryTypeId', N'Уникален системно генериран идентификатор.'
--'DOCUMENT', 'RESOLUTION', 'TASK'
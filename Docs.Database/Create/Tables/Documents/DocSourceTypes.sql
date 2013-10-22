print 'DocSourceTypes'
GO 

CREATE TABLE DocSourceTypes
(
    DocSourceTypeId		INT            IDENTITY (1, 1) NOT NULL,
    Name				NVARCHAR (200) NOT NULL,
	Alias				NVARCHAR (200) NOT NULL,
    IsActive			BIT            NOT NULL,
    Version				ROWVERSION     NOT NULL,
    CONSTRAINT PK_DocSourceTypes PRIMARY KEY CLUSTERED (DocSourceTypeId),
)
GO 

exec spDescTable  N'DocSourceTypes', N'Източник на документа, т.е. получено чрез'
exec spDescColumn N'DocSourceTypes', N'DocSourceTypeId', N'Уникален системно генериран идентификатор.'

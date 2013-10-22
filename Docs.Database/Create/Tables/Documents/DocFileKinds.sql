print 'DocFileKinds'
GO 

CREATE TABLE DocFileKinds
(
    DocFileKindId	INT            IDENTITY (1, 1) NOT NULL,
    Name			NVARCHAR (200) NOT NULL,
	Alias		NVARCHAR (200) NOT NULL,
    IsActive		BIT            NOT NULL,
    Version			ROWVERSION     NOT NULL,
    CONSTRAINT PK_DocFileKinds PRIMARY KEY CLUSTERED (DocFileKindId),
)
GO 

exec spDescTable  N'DocFileKinds', N'Видове файлове.'
exec spDescColumn N'DocFileKinds', N'DocFileKindId', N'Уникален системно генериран идентификатор.'

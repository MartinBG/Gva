print 'DocFiles'
GO 

--//TODO2
CREATE TABLE DocFiles
(
    DocFileId			INT				IDENTITY (1, 1) NOT NULL,
	DocId				INT				NOT NULL,
	DocFileTypeId		INT				NOT NULL,
    DocFileKindId		INT				NOT NULL,
    Name    			NVARCHAR (1000)	NULL,
    --Alias    			NVARCHAR (200)	NULL,
	DocFileName			NVARCHAR (1000)	NULL,

	--DocContentHashName	NVARCHAR (50)	NULL,
	--DocContentHash		NVARCHAR (100)	NULL,
	DocContentStorage	NVARCHAR (50)	NOT NULL,
	DocFileContentId	uniqueidentifier NOT NULL,

	--DocFileNameUnique	NVARCHAR (100)	NOT NULL,--=DocContentId
	--DocFileExtention	NVARCHAR (100)	NOT NULL,
	--DocContent		VARBINARY(MAX) FILESTREAM NULL,
	--DocContent		VARBINARY(MAX) NULL,
	SignDate			DATETIME		NULL,
    IsPrimary           BIT             NOT NULL,
	IsSigned			BIT            NOT NULL,
    IsActive			BIT            NOT NULL,
    Version				ROWVERSION     NOT NULL,
    CONSTRAINT PK_DocFiles PRIMARY KEY CLUSTERED (DocFileId),
	CONSTRAINT [FK_DocFiles_Docs] FOREIGN KEY ([DocId]) REFERENCES [dbo].[Docs] ([DocId]),
	CONSTRAINT [FK_DocFiles_DocFileTypes] FOREIGN KEY ([DocFileTypeId]) REFERENCES [dbo].[DocFileTypes] ([DocFileTypeId]),
    CONSTRAINT [FK_DocFiles_DocFileKinds] FOREIGN KEY ([DocFileKindId]) REFERENCES [dbo].[DocFileKinds] ([DocFileKindId]),
)
GO 

exec spDescTable  N'DocFiles', N'Файлове по документ.'
exec spDescColumn N'DocFiles', N'DocFileId', N'Уникален системно генериран идентификатор.'

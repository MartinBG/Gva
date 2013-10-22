print 'IncomingDocFiles'
GO 

CREATE TABLE IncomingDocFiles
(
    IncomingDocFileId			INT				IDENTITY (1, 1) NOT NULL,
	IncomingDocId				INT				NOT NULL,
	DocFileTypeId		INT				NOT NULL,
    Name    			NVARCHAR (1000)	NULL,
	DocFileName			NVARCHAR (1000)	NOT NULL,

	--DocContentHashName	NVARCHAR (50)	NULL,
	--DocContentHash		NVARCHAR (100)	NULL,
	DocContentStorage	NVARCHAR (50)	NOT NULL,
	DocFileContentId	uniqueidentifier NULL,

    Version				ROWVERSION     NOT NULL,
    CONSTRAINT [PK_IncomingDocFiles] PRIMARY KEY CLUSTERED (IncomingDocFileId),
	CONSTRAINT [FK_IncomingDocFiles_IncomingDocs] FOREIGN KEY ([IncomingDocId]) REFERENCES [dbo].[IncomingDocs] ([IncomingDocId]),
	CONSTRAINT [FK_IncomingDocFiles_DocFileTypes] FOREIGN KEY ([DocFileTypeId]) REFERENCES [dbo].[DocFileTypes] ([DocFileTypeId]),
)
GO 

exec spDescTable  N'IncomingDocFiles', N'Файлове по документ.'
exec spDescColumn N'IncomingDocFiles', N'IncomingDocFileId', N'Уникален системно генериран идентификатор.'

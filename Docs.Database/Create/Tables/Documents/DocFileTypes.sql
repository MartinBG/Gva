print 'DocFileTypes'
GO 
--moje da stane FileTypes, zashoto se izplozwa i w IncomingDocFiles
CREATE TABLE DocFileTypes
(
    DocFileTypeId	INT            IDENTITY (1, 1) NOT NULL,
    Name			NVARCHAR (200) NOT NULL,
	Alias		NVARCHAR (200) NOT NULL,
	DocTypeUri		NVARCHAR (200) NOT NULL,
    HasEmbeddedUri  BIT NOT NULL,
	MimeType		NVARCHAR (200) NOT NULL,
	Extention		NVARCHAR (100)	NOT NULL,
	IsEditable		BIT            NOT NULL,
    IsActive		BIT            NOT NULL,
    Version			ROWVERSION     NOT NULL,
    CONSTRAINT PK_DocFileTypes PRIMARY KEY CLUSTERED (DocFileTypeId),
)
GO 

exec spDescTable  N'DocFileTypes', N'Типове файлове.'
exec spDescColumn N'DocFileTypes', N'DocFileTypeId', N'Уникален системно генериран идентификатор.'

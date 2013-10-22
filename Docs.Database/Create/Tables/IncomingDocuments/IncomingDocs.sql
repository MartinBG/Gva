print 'IncomingDocs'
GO 

CREATE TABLE IncomingDocs
(
	IncomingDocId			INT		IDENTITY(1, 1)	NOT NULL,
	DocumentGuid			uniqueidentifier		NOT NULL,
	IncomingDate			datetime				NOT NULL,
	ModifyDate				datetime				NULL,
	IncomingDocStatusId		INT						NOT NULL,
	Version					ROWVERSION				NOT NULL,

	CONSTRAINT PK_IncomingDocs PRIMARY KEY CLUSTERED (IncomingDocId),
	CONSTRAINT [FK_IncomingDocs_IncomingDocStatuses] FOREIGN KEY ([IncomingDocStatusId]) REFERENCES [dbo].[IncomingDocStatuses] ([IncomingDocStatusId]),
)
GO 

exec spDescTable  N'IncomingDocs', N'Входящи документи.'
exec spDescColumn N'IncomingDocs', N'IncomingDocId', N'Уникален системно генериран идентификатор.'

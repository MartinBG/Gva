print 'DocIncomingDocs'
GO 

CREATE TABLE DocIncomingDocs
(
    DocIncomingDocId		INT			IDENTITY (1, 1) NOT NULL,
	DocId					INT			NOT NULL,
	IncomingDocId			INT			NOT NULL,
	IsDocInitial			BIT			NOT NULL,
	CreateDate				DATETIME	NOT NULL,
    CONSTRAINT PK_DocIncomingDocs PRIMARY KEY CLUSTERED (DocIncomingDocId),
	CONSTRAINT [FK_DocIncomingDocs_Docs] FOREIGN KEY ([DocId]) REFERENCES [dbo].[Docs] ([DocId]),
	CONSTRAINT [FK_DocIncomingDocs_IncomingDocs] FOREIGN KEY ([IncomingDocId]) REFERENCES [dbo].[IncomingDocs] ([IncomingDocId])
)
GO 

exec spDescTable  N'DocIncomingDocs', N'Входящи документи по документи.'
exec spDescColumn N'DocIncomingDocs', N'DocIncomingDocId', N'Уникален системно генериран идентификатор.'

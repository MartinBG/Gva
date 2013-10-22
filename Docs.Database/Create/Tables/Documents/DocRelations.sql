print 'DocRelations'
GO 

CREATE TABLE DocRelations
(
    DocRelationId			INT				IDENTITY (1, 1) NOT NULL,
	DocId					INT				NOT NULL,
	ParentDocId				INT				NULL,
	RootDocId				INT				NULL,
    Version					ROWVERSION		NOT NULL,
    CONSTRAINT PK_DocRelations PRIMARY KEY CLUSTERED (DocRelationId),
	CONSTRAINT [FK_DocRelations_Docs] FOREIGN KEY ([DocId]) REFERENCES [dbo].[Docs] ([DocId]),
	CONSTRAINT [FK_DocRelations_DocsParent] FOREIGN KEY ([ParentDocId]) REFERENCES [dbo].[Docs] ([DocId]),
	CONSTRAINT [FK_DocRelations_DocsRoot] FOREIGN KEY ([RootDocId]) REFERENCES [dbo].[Docs] ([DocId]),
)
GO 

exec spDescTable  N'DocRelations', N'Свързаност на документите.'
exec spDescColumn N'DocRelations', N'DocRelationId', N'Уникален системно генериран идентификатор.'

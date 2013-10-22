print 'DocCorrespondents'
GO 

CREATE TABLE DocCorrespondents
(
    DocCorrespondentId			INT				IDENTITY (1, 1) NOT NULL,
	DocId						INT				NOT NULL,
	CorrespondentId				INT				NOT NULL,
    Version						ROWVERSION     NOT NULL,
    CONSTRAINT PK_DocCorrespondents PRIMARY KEY CLUSTERED (DocCorrespondentId),
	CONSTRAINT [FK_DocCorrespondents_Docs] FOREIGN KEY ([DocId]) REFERENCES [dbo].[Docs] ([DocId]),
	CONSTRAINT [FK_DocCorrespondents_Correspondents] FOREIGN KEY ([CorrespondentId]) REFERENCES [dbo].[Correspondents] ([CorrespondentId]),
)
GO 

exec spDescTable  N'DocCorrespondents', N'Кореспонденти по документ.'
exec spDescColumn N'DocCorrespondents', N'DocCorrespondentId', N'Уникален системно генериран идентификатор.'

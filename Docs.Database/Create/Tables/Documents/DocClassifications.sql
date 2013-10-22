print 'DocClassifications'
GO 

CREATE TABLE DocClassifications
(
    DocClassificationId		INT			IDENTITY (1, 1) NOT NULL,
	DocId					INT			NOT NULL,
	ClassificationId		INT			NOT NULL,
	ClassificationByUserId	INT			NOT NULL,
	ClassificationDate		datetime	NOT NULL,
	IsActive				BIT         NOT NULL,
    Version					ROWVERSION  NOT NULL,
    CONSTRAINT PK_DocClassifications PRIMARY KEY CLUSTERED (DocClassificationId),
	CONSTRAINT [FK_DocClassifications_Docs] FOREIGN KEY ([DocId]) REFERENCES [dbo].[Docs] ([DocId]),
	CONSTRAINT [FK_DocClassifications_Classifications] FOREIGN KEY ([ClassificationId]) REFERENCES [dbo].[Classifications] ([ClassificationId])
)
GO 

exec spDescTable  N'DocClassifications', N'Класификационни схеми на документ.'
exec spDescColumn N'DocClassifications', N'DocClassificationId', N'Уникален системно генериран идентификатор.'

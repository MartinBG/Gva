print 'DocCasePartMovements'
GO 

CREATE TABLE DocCasePartMovements
(
    DocCasePartMovementId		INT IDENTITY (1, 1) NOT NULL,
	DocId						INT NOT NULL,
	DocCasePartTypeId			INT NOT NULL,
    MovementDate				DATETIME NOT NULL,
	UserId						INT NOT NULL,
    Version						ROWVERSION     NOT NULL,
    CONSTRAINT PK_DocCasePartMovements PRIMARY KEY CLUSTERED (DocCasePartMovementId),
	CONSTRAINT [FK_DocCasePartMovements_Docs] FOREIGN KEY ([DocId]) REFERENCES [dbo].[Docs] ([DocId]),
	CONSTRAINT [FK_DocCasePartMovements_DocCasePartTypes] FOREIGN KEY ([DocCasePartTypeId]) REFERENCES [dbo].[DocCasePartTypes] ([DocCasePartTypeId]),
	CONSTRAINT [FK_DocCasePartMovements_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId]),
)
GO 

exec spDescTable  N'DocCasePartMovements', N'Движение на документ в разделите на преписката'
exec spDescColumn N'DocCasePartMovements', N'DocCasePartMovementId', N'Уникален системно генериран идентификатор.'
print 'DocWorkflows'
GO 

CREATE TABLE DocWorkflows
(
    DocWorkflowId	INT            IDENTITY (1, 1) NOT NULL,
	DocId			INT NOT NULL,
	DocWorkflowActionId	INT NOT NULL,
	EventDate		DATETIME NOT NULL,
	YesNo			BIT NULL,
	UserId			INT NOT NULL,
	ToUnitId		INT NULL,
	PrincipalUnitId	INT NULL,
	Note			NVARCHAR(MAX) NULL,
    Version			ROWVERSION     NOT NULL,
    CONSTRAINT PK_DocWorkflows PRIMARY KEY CLUSTERED (DocWorkflowId),
	CONSTRAINT [FK_DocWorkflows_Docs] FOREIGN KEY ([DocId]) REFERENCES [dbo].[Docs] ([DocId]),
	CONSTRAINT [FK_DocWorkflows_DocWorkflowActions] FOREIGN KEY ([DocWorkflowActionId]) REFERENCES [dbo].[DocWorkflowActions] ([DocWorkflowActionId]),
	CONSTRAINT [FK_DocWorkflows_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId]),
	CONSTRAINT [FK_DocWorkflows_Units] FOREIGN KEY ([ToUnitId]) REFERENCES [dbo].[Units] ([UnitId]),
	CONSTRAINT [FK_DocWorkflows_Units2] FOREIGN KEY ([PrincipalUnitId]) REFERENCES [dbo].[Units] ([UnitId]),
)
GO 

exec spDescTable  N'DocWorkflows', N'Работен поток.'
exec spDescColumn N'DocWorkflows', N'DocWorkflowId', N'Уникален системно генериран идентификатор.'

print 'DocWorkflowActions'
GO 

CREATE TABLE DocWorkflowActions
(
    DocWorkflowActionId		INT            IDENTITY (1, 1) NOT NULL,
    Name			NVARCHAR (200) NOT NULL,
	Alias			NVARCHAR (200) NOT NULL,
    IsActive		BIT            NOT NULL,
    Version			ROWVERSION     NOT NULL,
    CONSTRAINT PK_DocWorkflowActions PRIMARY KEY CLUSTERED (DocWorkflowActionId)

)
GO 

exec spDescTable  N'DocWorkflowActions', N'Действия за работен поток.'
exec spDescColumn N'DocWorkflowActions', N'DocWorkflowActionId', N'Уникален системно генериран идентификатор.'

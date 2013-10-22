print 'Docs'
GO 

CREATE TABLE Docs
(
    DocId			INT				IDENTITY (1, 1) NOT NULL,
	DocDirectionId	INT				NOT NULL,
	DocEntryTypeId	INT				NOT NULL,
	DocCasePartTypeId	INT				NULL,
	DocSubject		NVARCHAR (max)	NOT NULL,
	DocBody			NVARCHAR (max)	NULL,
	DocStatusId		INT				NOT NULL,

	DocSourceTypeId INT				NULL,
    DocDestinationTypeId INT		NULL,
--DocEntryType=DOCUMENT 
	DocTypeId		INT				NULL,
	DocFormatTypeId INT				NULL,
	DocRegisterId	INT				NULL,
	RegUri			NVARCHAR (200)	NULL,
	RegIndex		NVARCHAR (200)	NULL, -- регистър
	RegNumber		int				NULL, -- номер в регистър
	RegDate			datetime		NULL, -- дата
    ExternalRegNumber		NVARCHAR (200)	NULL, -- номер от външен рег
	CorrRegNumber	NVARCHAR (200)	NULL,   
	CorrRegDate		datetime		NULL,   
	AccessCode		NVARCHAR (50)	NULL,
--DocEntryType=RESOLUTION 
    AssignmentTypeId INT    NULL,
    AssignmentDate datetime NULL,
    AssignmentDeadline datetime NULL,

-- Flags
    --IsExamined      BIT             NOT NULL,
    IsCase          BIT             NOT NULL,
    IsRegistered    BIT             NOT NULL,
    IsSigned        BIT             NOT NULL,
	LockObjectId    uniqueidentifier    NULL,
    ModifyDate      datetime        NULL,
    ModifyUserId    int             NULL,
/*
	LASTEDITEDBYID
	LOCKEDBYID
	REGISTEREDBYID
	DELOVODNOARHIVIRANBYID
	CreateDate
	LastModify
*/
    IsActive		BIT            NOT NULL,
    Version			ROWVERSION     NOT NULL,
    CONSTRAINT PK_Docs PRIMARY KEY CLUSTERED (DocId),
	CONSTRAINT [FK_Docs_DocDirections] FOREIGN KEY ([DocDirectionId]) REFERENCES [dbo].[DocDirections] ([DocDirectionId]),
	CONSTRAINT [FK_Docs_DocEntryTypes] FOREIGN KEY ([DocEntryTypeId]) REFERENCES [dbo].[DocEntryTypes] ([DocEntryTypeId]),
	CONSTRAINT [FK_Docs_DocStatuses] FOREIGN KEY ([DocStatusId]) REFERENCES [dbo].[DocStatuses] ([DocStatusId]),
    CONSTRAINT [FK_Docs_AssignmentTypes] FOREIGN KEY ([AssignmentTypeId]) REFERENCES [dbo].[AssignmentTypes] ([AssignmentTypeId]),

	CONSTRAINT [FK_Docs_DocSourceTypes] FOREIGN KEY ([DocSourceTypeId]) REFERENCES [dbo].[DocSourceTypes] ([DocSourceTypeId]),
    CONSTRAINT [FK_Docs_DocDestinationTypes] FOREIGN KEY ([DocDestinationTypeId]) REFERENCES [dbo].[DocDestinationTypes] ([DocDestinationTypeId]),

	CONSTRAINT [FK_Docs_DocTypes] FOREIGN KEY ([DocTypeId]) REFERENCES [dbo].[DocTypes] ([DocTypeId]),
	CONSTRAINT [FK_Docs_DocFormatTypes] FOREIGN KEY ([DocFormatTypeId]) REFERENCES [dbo].[DocFormatTypes] ([DocFormatTypeId]),
	CONSTRAINT [FK_Docs_DocRegisters] FOREIGN KEY ([DocRegisterId]) REFERENCES [dbo].[DocRegisters] ([DocRegisterId]),
	CONSTRAINT [FK_Docs_DocCasePartTypes] FOREIGN KEY ([DocCasePartTypeId]) REFERENCES [dbo].[DocCasePartTypes] ([DocCasePartTypeId]),
    CONSTRAINT [FK_Docs_Users] FOREIGN KEY ([ModifyUserId]) REFERENCES [dbo].[Users] ([UserId]),
)
GO 

exec spDescTable  N'Docs', N'Документи.'
exec spDescColumn N'Docs', N'DocId', N'Уникален системно генериран идентификатор.'

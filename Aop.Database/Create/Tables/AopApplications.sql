PRINT 'AopApplications'
GO 

CREATE TABLE [dbo].[AopApplications] (
    [AopApplicationId]    INT  NOT NULL IDENTITY,
    [DocId]               INT  NULL,
    [AopEmployerId] INT NULL,
	[Email] NVARCHAR (100) NULL,
	-- I
	[STAopApplicationTypeId] INT NULL,
	[STObjectId] INT NULL,
	[STSubject] NVARCHAR (MAX) NULL,
	[STCriteriaId] INT NULL,
	[STValue] NVARCHAR (500) NULL,
	[STRemark] NVARCHAR (MAX) NULL,
	[STIsMilitary] BIT NULL,
	[STNoteTypeId] INT NULL, --?
	[STDocId] INT NULL,
	[STChecklistId] INT NULL,
	[STChecklistStatusId] INT NULL, -- нов, документ, резолюция
	[STNoteId] INT NULL,
	-- II
	[NDAopApplicationTypeId] INT NULL,
	[NDObjectId] INT NULL,
	[NDSubject] NVARCHAR (MAX) NULL,
	[NDCriteriaId] INT NULL,
	[NDValue] NVARCHAR (500) NULL,
	[NDIsMilitary] BIT NULL,
	[NDROPIdNum] NVARCHAR (50) NULL,
	[NDROPUnqNum] NVARCHAR (50) NULL,
	[NDROPDate] DATETIME NULL,
	[NDProcedureStatusId] INT NULL,
	[NDRefusalReason] NVARCHAR (MAX) NULL,
	[NDAppeal] NVARCHAR (MAX) NULL,
	[NDRemark] NVARCHAR (MAX) NULL,
	[NDDocId] INT NULL,
	[NDChecklistId] INT NULL,
	[NDChecklistStatusId] INT NULL,
	[NDReportId] INT NULL,

	[Version] ROWVERSION NOT NULL,
    CONSTRAINT [PK_AopApplications]           PRIMARY KEY ([AopApplicationId]),
    
	CONSTRAINT [FK_AopApplications_AopEmployers]      FOREIGN KEY ([AopEmployerId])            REFERENCES [dbo].[AopEmployers] ([AopEmployerId]),

    CONSTRAINT [FK_AopApplications_Docs]      FOREIGN KEY ([DocId])            REFERENCES [dbo].[Docs] ([DocId]),
	CONSTRAINT [FK_AopApplications_Docs2]      FOREIGN KEY ([STDocId])            REFERENCES [dbo].[Docs] ([DocId]),
	CONSTRAINT [FK_AopApplications_Docs3]      FOREIGN KEY ([STChecklistId])            REFERENCES [dbo].[Docs] ([DocId]),
	CONSTRAINT [FK_AopApplications_Docs4]      FOREIGN KEY ([STNoteId])            REFERENCES [dbo].[Docs] ([DocId]),

	CONSTRAINT [FK_AopApplications_Docs5]      FOREIGN KEY ([NDDocId])            REFERENCES [dbo].[Docs] ([DocId]),
	CONSTRAINT [FK_AopApplications_Docs6]      FOREIGN KEY ([NDChecklistId])            REFERENCES [dbo].[Docs] ([DocId]),
	CONSTRAINT [FK_AopApplications_Docs7]      FOREIGN KEY ([NDReportId])            REFERENCES [dbo].[Docs] ([DocId]),

	CONSTRAINT [FK_AopApplications_AopChecklistStatuses]      FOREIGN KEY ([STChecklistStatusId])            REFERENCES [dbo].[AopChecklistStatuses] ([AopChecklistStatusId]),
	CONSTRAINT [FK_AopApplications_AopChecklistStatuses2]      FOREIGN KEY ([NDChecklistStatusId])            REFERENCES [dbo].[AopChecklistStatuses] ([AopChecklistStatusId]),

	CONSTRAINT [FK_AopApplications_AopProcedureStatuses]      FOREIGN KEY ([NDProcedureStatusId])            REFERENCES [dbo].[AopProcedureStatuses] ([AopProcedureStatusId]),

	CONSTRAINT [FK_AopApplications_AopApplicationCriteria]      FOREIGN KEY ([STCriteriaId])            REFERENCES [dbo].[AopApplicationCriteria] ([AopApplicationCriteriaId]),
	CONSTRAINT [FK_AopApplications_AopApplicationCriteria2]      FOREIGN KEY ([NDCriteriaId])            REFERENCES [dbo].[AopApplicationCriteria] ([AopApplicationCriteriaId]),

	CONSTRAINT [FK_AopApplications_AopApplicationObjects]      FOREIGN KEY ([STObjectId])            REFERENCES [dbo].[AopApplicationObjects] ([AopApplicationObjectId]),
	CONSTRAINT [FK_AopApplications_AopApplicationObjects2]      FOREIGN KEY ([NDObjectId])            REFERENCES [dbo].[AopApplicationObjects] ([AopApplicationObjectId])
)
GO

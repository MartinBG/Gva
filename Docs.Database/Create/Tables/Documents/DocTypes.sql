print 'DocTypes'
GO 

CREATE TABLE DocTypes
(
    DocTypeId		INT            IDENTITY (1, 1) NOT NULL,
	DocTypeGroupId	INT            NOT NULL,
    PrimaryRegisterIndexId INT            NULL,
    SecondaryRegisterIndexId INT          NULL,
    Name			NVARCHAR (500) NOT NULL,
	Alias			NVARCHAR (200) NOT NULL,
	IsElectronicService					BIT            NOT NULL,
	ElectronicServiceFileTypeUri		NVARCHAR (200) NULL,
	ElectronicServiceTypeApplication	NVARCHAR (200) NULL,
	ElectronicServiceProvider			NVARCHAR (200) NULL,
	ExecutionDeadline					INT            NULL,
	RemoveIrregularitiesDeadline		INT            NULL,
    IsActive		BIT            NOT NULL,
    Version			ROWVERSION     NOT NULL,
    CONSTRAINT PK_DocTypes PRIMARY KEY CLUSTERED (DocTypeId),
	CONSTRAINT [FK_DocTypes_DocTypeGroups] FOREIGN KEY ([DocTypeGroupId]) REFERENCES [dbo].[DocTypeGroups] ([DocTypeGroupId]),
    CONSTRAINT [FK_DocTypes_RegisterIndexes] FOREIGN KEY ([PrimaryRegisterIndexId]) REFERENCES [dbo].[RegisterIndexes] ([RegisterIndexId]),
    CONSTRAINT [FK_DocTypes_RegisterIndexes_2] FOREIGN KEY ([SecondaryRegisterIndexId]) REFERENCES [dbo].[RegisterIndexes] ([RegisterIndexId]),
)
GO 

exec spDescTable  N'DocTypes', N'Типове документи.'
exec spDescColumn N'DocTypes', N'DocTypeId', N'Уникален системно генериран идентификатор.'

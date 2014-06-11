print 'DocClassifications'
GO 

CREATE TABLE [dbo].[AdministrativeEmails](
	[AdministrativeEmailId] [int] IDENTITY(1,1) NOT NULL,
	[TypeId] [int] NOT NULL,
	[UserId] [int] NULL,
	[CorrespondentId] [int] NULL,
	[CorrespondentContactId] [int] NULL,
	[Param1] [nvarchar](500) NULL,
	[Param2] [nvarchar](500) NULL,
	[Param3] [nvarchar](500) NULL,
	[Param4] [nvarchar](500) NULL,
	[Param5] [nvarchar](500) NULL,
	[StatusId] [int] NOT NULL,
    [Subject] [nvarchar](1000) NULL,
	[Body] [nvarchar](MAX) NULL,
    [SentDate] [datetime] NULL,
	[Version] ROWVERSION NOT NULL,
 CONSTRAINT [PK_AdministrativeEmails] PRIMARY KEY ([AdministrativeEmailId]),
 CONSTRAINT [FK_AdministrativeEmails_AdministrativeEmailStatuses] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[AdministrativeEmailStatuses] ([AdministrativeEmailStatusId]),
 CONSTRAINT [FK_AdministrativeEmails_AdministrativeEmailTypes] FOREIGN KEY ([TypeId]) REFERENCES [dbo].[AdministrativeEmailTypes] ([AdministrativeEmailTypeId]),
)
GO

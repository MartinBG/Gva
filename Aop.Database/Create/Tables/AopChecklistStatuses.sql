PRINT 'AopChecklistStatuses'
GO 

CREATE TABLE [dbo].[AopChecklistStatuses] (
    [AopChecklistStatusId]    INT  NOT NULL IDENTITY,
    [Name] NVARCHAR(200) NOT NULL,
    [Alias] NVARCHAR(200) NOT NULL,
	[IsActive] BIT NOT NULL,
	[Version] ROWVERSION NOT NULL,
    CONSTRAINT [PK_AopChecklistStatuses]           PRIMARY KEY ([AopChecklistStatusId])
)
GO

PRINT 'AopProcedureStatuses'
GO 

CREATE TABLE [dbo].[AopProcedureStatuses] (
    [AopProcedureStatusId]    INT  NOT NULL IDENTITY,
    [Name] NVARCHAR(200) NOT NULL,
    [Alias] NVARCHAR(200) NOT NULL,
	[IsActive] BIT NOT NULL,
	[Version] ROWVERSION NOT NULL,
    CONSTRAINT [PK_AopProcedureStatuses]           PRIMARY KEY ([AopProcedureStatusId])
)
GO

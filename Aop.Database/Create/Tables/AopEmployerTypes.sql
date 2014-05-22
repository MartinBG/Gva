PRINT 'AopEmployerTypes'
GO 

CREATE TABLE [dbo].[AopEmployerTypes] (
    [AopEmployerTypeId]    INT  NOT NULL IDENTITY,
    [Name] NVARCHAR(200) NOT NULL,
    [Alias] NVARCHAR(200) NOT NULL,
	[IsActive] BIT NOT NULL,
	[Version] ROWVERSION NOT NULL,
    CONSTRAINT [PK_AopEmployerTypes]           PRIMARY KEY ([AopEmployerTypeId])
)
GO

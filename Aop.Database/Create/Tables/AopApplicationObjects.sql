PRINT 'AopApplicationObjects'
GO 

CREATE TABLE [dbo].[AopApplicationObjects] (
    [AopApplicationObjectId]    INT  NOT NULL IDENTITY,
    [Name] NVARCHAR(200) NOT NULL,
    [Alias] NVARCHAR(200) NOT NULL,
	[IsActive] BIT NOT NULL,
	[Version] ROWVERSION NOT NULL,
    CONSTRAINT [PK_AopApplicationObjects]           PRIMARY KEY ([AopApplicationObjectId])
)
GO

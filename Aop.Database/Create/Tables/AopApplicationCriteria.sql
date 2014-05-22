PRINT 'AopApplicationCriteria'
GO 

CREATE TABLE [dbo].[AopApplicationCriteria] (
    [AopApplicationCriteriaId]    INT  NOT NULL IDENTITY,
    [Name] NVARCHAR(200) NOT NULL,
    [Alias] NVARCHAR(200) NOT NULL,
	[IsActive] BIT NOT NULL,
	[Version] ROWVERSION NOT NULL,
    CONSTRAINT [PK_AopApplicationCriteria]           PRIMARY KEY ([AopApplicationCriteriaId])
)
GO

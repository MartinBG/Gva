print 'EmailStatuses'
GO 

CREATE TABLE [dbo].[EmailStatuses] (
    [EmailStatusId] INT IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (200) NOT NULL,
    [Alias] NVARCHAR (200) NULL,
    [Version] ROWVERSION NOT NULL,
    CONSTRAINT [PK_EmailStatuses] PRIMARY KEY ([EmailStatusId])
)
GO

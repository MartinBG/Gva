PRINT 'LotSets'
GO 

CREATE TABLE [dbo].[LotSets] (
    [LotSetId]   INT           NOT NULL,
    [Name]       NVARCHAR (50) NOT NULL,
    [Alias]      NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_LotSets] PRIMARY KEY ([LotSetId]),
    CONSTRAINT [UQ_LotSet_Alias] UNIQUE NONCLUSTERED ([Alias])
)
GO

exec spDescTable  N'LotSets', N'Типове партиди.'
exec spDescColumn N'LotSets', N'LotSetId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'LotSets', N'Name'      , N'Наименование.'
exec spDescColumn N'LotSets', N'Alias'     , N'Символен идентификатор.'
GO

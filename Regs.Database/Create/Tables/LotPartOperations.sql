PRINT 'LotPartOperations'
GO 

CREATE TABLE [dbo].[LotPartOperations] (
    [LotPartOperationId]    INT             NOT NULL,
    [Name]                  NVARCHAR (50)   NOT NULL,
    [Alias]                 NVARCHAR (50)   NULL,
    CONSTRAINT [PK_LotPartOperations] PRIMARY KEY ([LotPartOperationId])
)
GO

exec spDescTable  N'LotPartOperations', N'Операции за част на партида.'
exec spDescColumn N'LotPartOperations', N'LotPartOperationId' , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'LotPartOperations', N'Name'               , N'Наименование.'
exec spDescColumn N'LotPartOperations', N'Alias'              , N'Символен идентификатор.'
GO

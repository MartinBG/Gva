PRINT 'LotTypes'
GO 

CREATE TABLE [dbo].[LotTypes] (
    [LotTypeId]  INT           NOT NULL,
    [Name]       NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_LotTypes] PRIMARY KEY ([LotTypeId])
)
GO

exec spDescTable  N'LotTypes', N'Типове партиди.'
exec spDescColumn N'LotTypes', N'LotTypeId' , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'LotTypes', N'Name'      , N'Наименование.'
GO

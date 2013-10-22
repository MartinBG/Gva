PRINT 'GvaLotFileTypes'
GO 

CREATE TABLE [dbo].[GvaLotFileTypes] (
    [GvaLotFileTypeId]   INT             NOT NULL,
    [Code]               NVARCHAR (50)   NULL,
    [Name]               NVARCHAR (50)   NULL,
    CONSTRAINT [PK_GvaLotFileTypes] PRIMARY KEY ([GvaLotFileTypeId])
)
GO

exec spDescTable  N'GvaLotFileTypes', N'Типове на файл от описа.'
exec spDescColumn N'GvaLotFileTypes', N'GvaLotFileTypeId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'GvaLotFileTypes', N'Code'                 , N'Код.'
exec spDescColumn N'GvaLotFileTypes', N'Name'                 , N'Наименование.'
GO

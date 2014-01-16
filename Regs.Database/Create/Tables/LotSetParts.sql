PRINT 'LotSetParts'
GO 

CREATE TABLE [dbo].[LotSetParts] (
    [LotSetPartId]  int            NOT NULL,
    [LotSetId]      int            NOT NULL,
    [PathRegex]     nvarchar (100)  NOT NULL,
    [Schema]        nvarchar (MAX) NOT NULL,
    CONSTRAINT [PK_LotSetParts]         PRIMARY KEY ([LotSetPartId]),
    CONSTRAINT [FK_LotSetParts_LotSets] FOREIGN KEY ([LotSetId]) REFERENCES [dbo].[LotSets] ([LotSetId])
)
GO

exec spDescTable  N'LotSetParts', N'Части на тип партида.'
exec spDescColumn N'LotSetParts', N'LotSetPartId' , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'LotSetParts', N'LotSetId'     , N'Тип партида.'
exec spDescColumn N'LotSetParts', N'PathRegex'    , N'Път.'
exec spDescColumn N'LotSetParts', N'Schema'       , N'Схема.'
GO

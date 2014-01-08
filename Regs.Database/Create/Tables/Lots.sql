PRINT 'Lots'
GO 

CREATE TABLE [dbo].[Lots] (
    [LotId]         INT                 NOT NULL IDENTITY,
    [LotSetId]      INT                 NOT NULL,
    [NextIndex]     INT                 NOT NULL,
    [ModifyDate]    DATETIME2 (7)       NOT NULL,
    [Version]       ROWVERSION          NOT NULL,
    CONSTRAINT [PK_Lots]          PRIMARY KEY ([LotId]),
    CONSTRAINT [FK_Lots_LotSets]  FOREIGN KEY ([LotSetId]) REFERENCES [dbo].[LotSets] ([LotSetId])
)
GO

exec spDescTable  N'Lots', N'Партиди.'
exec spDescColumn N'Lots', N'LotId'      , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Lots', N'LotSetId'   , N'Тип на партидата.'
exec spDescColumn N'Lots', N'NextIndex'  , N'Следващ поред номер на част на партидата.'
exec spDescColumn N'Lots', N'ModifyDate' , N'Дата на последна промяна.'
exec spDescColumn N'Lots', N'Version'    , N'Версия.'
GO

PRINT 'GvaLotCases'
GO 

CREATE TABLE [dbo].[GvaLotCases] (
    [GvaLotCaseId]   INT NOT NULL IDENTITY,
    [GvaCaseTypeId]  INT NOT NULL,
    [LotId]          INT NOT NULL,
    CONSTRAINT [PK_GvaLotCases] PRIMARY KEY ([GvaLotCaseId]),
    CONSTRAINT [FK_GvaLotCases_GvaCaseTypes] FOREIGN KEY([GvaCaseTypeId]) REFERENCES [dbo].[GvaCaseTypes] ([GvaCaseTypeId]),
    CONSTRAINT [FK_GvaLotCases_Lots]         FOREIGN KEY([LotId])         REFERENCES [dbo].[Lots]         ([LotId])
)
GO

exec spDescTable  N'GvaLotCases', N'Дела по партида.'
exec spDescColumn N'GvaLotCases', N'GvaLotCaseId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'GvaLotCases', N'GvaCaseTypeId' , N'Тип дело.'
exec spDescColumn N'GvaLotCases', N'LotId'         , N'Партида.'
GO

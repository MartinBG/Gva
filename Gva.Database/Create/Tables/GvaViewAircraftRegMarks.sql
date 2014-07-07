PRINT 'GvaViewAircraftRegMarks'
GO 

CREATE TABLE [dbo].[GvaViewAircraftRegMarks] (
    [LotId]                  INT           NOT NULL,
    [PartIndex]              INT           NOT NULL,
    [RegMark]                NVARCHAR(50)  NOT NULL,
    CONSTRAINT [PK_GvaViewAircraftRegMarks]                   PRIMARY KEY ([LotId], [PartIndex]),
    CONSTRAINT [FK_GvaViewAircraftRegMarks_GvaViewAircrafts]  FOREIGN KEY ([LotId]) REFERENCES [dbo].[GvaViewAircrafts] ([LotId])
)
GO

exec spDescTable  N'GvaViewAircraftRegMarks', N'Регистрационни знаци на ВС'
exec spDescColumn N'GvaViewAircraftRegMarks', N'LotId'                 , N'Идентификатор на партида на ВС.'
exec spDescColumn N'GvaViewAircraftRegMarks', N'PartIndex'             , N'Идентификатор на регистрация на ВС.'
exec spDescColumn N'GvaViewAircraftRegMarks', N'RegMark'               , N'Рег. знак.'
GO

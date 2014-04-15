PRINT 'GvaViewAircraftRegMarks'
GO 

CREATE TABLE [dbo].[GvaViewAircraftRegMarks] (
    [LotPartId]              INT           NOT NULL,
    [LotId]                  INT           NOT NULL,
    [RegMark]                NVARCHAR(50)  NOT NULL,
    CONSTRAINT [PK_GvaViewAircraftRegMarks]                   PRIMARY KEY ([LotPartId]),
    CONSTRAINT [FK_GvaViewAircraftRegMarks_LotParts]          FOREIGN KEY ([LotPartId]) REFERENCES [dbo].[LotParts] ([LotPartId]),
    CONSTRAINT [FK_GvaViewAircraftRegMarks_GvaViewAircrafts]  FOREIGN KEY ([LotId])     REFERENCES [dbo].[Lots]     ([LotId])
)
GO

exec spDescTable  N'GvaViewAircraftRegMarks', N'Регистрационни знаци на ВС'
exec spDescColumn N'GvaViewAircraftRegMarks', N'LotPartId'             , N'Идентификатор на регистрация на ВС.'
exec spDescColumn N'GvaViewAircraftRegMarks', N'LotId'                 , N'Идентификатор на партида на ВС.'
exec spDescColumn N'GvaViewAircraftRegMarks', N'RegMark'               , N'Рег. знак.'
GO

PRINT 'GvaViewAircraftPrintedCertNoises'
GO 

CREATE TABLE [dbo].[GvaViewAircraftPrintedCertNoises] (
    [LotId]                   INT              NOT NULL,
    [NoisePartIndex]          INT              NOT NULL,
    [PrintedFileId]           INT              NOT NULL,
    CONSTRAINT [PK_GvaViewAircraftPrintedCertNoises]                  PRIMARY KEY ([LotId], [NoisePartIndex]),
    CONSTRAINT [FK_GvaViewAircraftPrintedCertNoises_GvaViewAircrafts] FOREIGN KEY ([LotId]) REFERENCES [dbo].[GvaViewAircrafts] ([LotId]),
    CONSTRAINT [FK_GvaViewAircraftPrintedCertNoises_GvaFiles]         FOREIGN KEY ([PrintedFileId])   REFERENCES [dbo].[GvaFiles] ([GvaFileId])
)
GO

exec spDescTable  N'GvaViewAircraftPrintedCertNoises', N'Данни за принтирани удостоверения за шум.'
exec spDescColumn N'GvaViewAircraftPrintedCertNoises', N'LotId'                  , N'Идентификатор на партидата.'
exec spDescColumn N'GvaViewAircraftPrintedCertNoises', N'NoisePartIndex'         , N'Идентификатор на парта на шума.'
GO

PRINT 'GvaViewAircraftCerts'
GO 

CREATE TABLE [dbo].[GvaViewAircraftCerts] (
    [LotId]                     INT           NOT NULL,
    [PartIndex]                 INT           NOT NULL,
    [DocumentNumber]            NVARCHAR(20)  NOT NULL,
    [ParsedNumberWithoutPrefix] INT           NULL,
    [FormNumberPrefix]          INT           NULL,
    CONSTRAINT [PK_GvaViewAircraftCerts]                   PRIMARY KEY ([LotId], [PartIndex]),
    CONSTRAINT [FK_GvaViewAircraftCerts_GvaViewAircrafts]  FOREIGN KEY ([LotId]) REFERENCES [dbo].[GvaViewAircrafts] ([LotId])
)
GO

exec spDescTable  N'GvaViewAircraftCerts', N'Данни за Форма 24,25 и 45 на ВС'
exec spDescColumn N'GvaViewAircraftCerts', N'LotId'                     , N'Идентификатор на партида на ВС.'
exec spDescColumn N'GvaViewAircraftCerts', N'PartIndex'                 , N'Идентификатор на Форма на ВС.'
exec spDescColumn N'GvaViewAircraftCerts', N'DocumentNumber'            , N'Номер на документа без префикс'
exec spDescColumn N'GvaViewAircraftCerts', N'FormNumberPrefix'          , N'Префикса на формата.'
exec spDescColumn N'GvaViewAircraftCerts', N'ParsedNumberWithoutPrefix' , N'Номер на документа без префикс конвертиран в int'
GO

PRINT 'GvaViewAircraftCertAirworthinesses'
GO 

CREATE TABLE [dbo].[GvaViewAircraftCertAirworthinesses] (
    [LotId]                  INT           NOT NULL,
    [PartIndex]              INT           NOT NULL,
    [CertificateTypeId]      INT           NOT NULL,
    [PrintedFileId]          INT           NULL,
    CONSTRAINT [PK_GvaViewAircraftCertAirworthinesses]                   PRIMARY KEY ([LotId], [PartIndex]),
    CONSTRAINT [FK_GvaViewAircraftCertAirworthinesses_GvaViewAircrafts]  FOREIGN KEY ([LotId])             REFERENCES [dbo].[GvaViewAircrafts] ([LotId]),
    CONSTRAINT [FK_GvaViewAircraftCertAirworthinesses_NomValues]         FOREIGN KEY ([CertificateTypeId]) REFERENCES [dbo].[NomValues] ([NomValueId]),
    CONSTRAINT [FK_GvaViewAircraftCertAirworthinesses_GvaFiles]          FOREIGN KEY ([PrintedFileId])     REFERENCES [dbo].[GvaFiles] ([GvaFileId])
)
GO

exec spDescTable  N'GvaViewAircraftCertAirworthinesses', N'Летателни годности на ВС'
exec spDescColumn N'GvaViewAircraftCertAirworthinesses', N'LotId'                 , N'Идентификатор на партида на ВС.'
exec spDescColumn N'GvaViewAircraftCertAirworthinesses', N'PartIndex'             , N'Идентификатор на регистрация на ВС.'
exec spDescColumn N'GvaViewAircraftCertAirworthinesses', N'CertificateTypeId'     , N'Tим летателна годност.'
exec spDescColumn N'GvaViewAircraftCertAirworthinesses', N'PrintedFileId'         , N'Идентификатор на принтиран документи.'
GO

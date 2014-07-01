PRINT 'GvaViewAircraftRegistrations'
GO 

CREATE TABLE [dbo].[GvaViewAircraftRegistrations] (
    [LotPartId]              INT           NOT NULL,
    [LotId]                  INT           NOT NULL,
    [CertRegisterId]         INT           NOT NULL,
    [CertNumber]             INT           NOT NULL,
    [ActNumber]              INT           NOT NULL,
    [RegMark]                NVARCHAR(50)  NOT NULL,
    CONSTRAINT [PK_GvaAircraftRegistrations]                   PRIMARY KEY ([LotPartId]),
    CONSTRAINT [FK_GvaAircraftRegistrations_LotParts]          FOREIGN KEY ([LotPartId]) REFERENCES [dbo].[LotParts] ([LotPartId]),
    CONSTRAINT [FK_GvaAircraftRegistrations_GvaViewAircrafts]  FOREIGN KEY ([LotId])     REFERENCES [dbo].[Lots]     ([LotId])
)
GO

exec spDescTable  N'GvaViewAircraftRegistrations', N'Регистрации на ВС'
exec spDescColumn N'GvaViewAircraftRegistrations', N'LotPartId'             , N'Идентификатор на регистрация на ВС.'
exec spDescColumn N'GvaViewAircraftRegistrations', N'LotId'                 , N'Идентификатор на партида на ВС.'
exec spDescColumn N'GvaViewAircraftRegistrations', N'CertRegisterId'        , N'Регистър.'
exec spDescColumn N'GvaViewAircraftRegistrations', N'CertNumber'            , N'Рег. номер.'
exec spDescColumn N'GvaViewAircraftRegistrations', N'ActNumber'             , N'Дел. номер.'
exec spDescColumn N'GvaViewAircraftRegistrations', N'RegMark'               , N'Рег. знак.'
GO

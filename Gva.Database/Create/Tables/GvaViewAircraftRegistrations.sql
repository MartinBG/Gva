PRINT 'GvaViewAircraftRegistrations'
GO 

CREATE TABLE [dbo].[GvaViewAircraftRegistrations] (
    [LotPartId] INT           NOT NULL,
    [LotId] INT           NOT NULL,
    [CertNumber]            NVARCHAR(50)  NOT NULL,
    CONSTRAINT [PK_GvaAircraftRegistrations]      PRIMARY KEY ([LotPartId]),
    CONSTRAINT [FK_GvaAircraftRegistrations_LotParts]  FOREIGN KEY ([LotPartId]) REFERENCES [dbo].[LotParts] ([LotPartId]),
    CONSTRAINT [FK_GvaAircraftRegistrations_GvaViewAircrafts]  FOREIGN KEY ([LotId]) REFERENCES [dbo].[Lots] ([LotId])
)
GO

exec spDescTable  N'GvaViewAircraftRegistrations', N'Регистрации на ВС'
exec spDescColumn N'GvaViewAircraftRegistrations', N'LotPartId', N'Идентификатор на регистрация на ВС.'
exec spDescColumn N'GvaViewAircraftRegistrations', N'LotId'           , N'Идентификатор на партида на ВС.'
exec spDescColumn N'GvaViewAircraftRegistrations', N'CertNumber'           , N'Рег. номер.'
GO

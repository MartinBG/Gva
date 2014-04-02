PRINT 'GvaViewPersonLicences'
GO 

CREATE TABLE [dbo].[GvaViewPersonLicences] (
    [LotId]          INT           NOT NULL,
    [LotPartId]      INT           NOT NULL,
    [LicenceType]    NVARCHAR(MAX) NOT NULL,
    CONSTRAINT [PK_GvaViewPersonLicences]                PRIMARY KEY ([LotId], [LotPartId]),
    CONSTRAINT [FK_GvaViewPersonLicences_Lots]           FOREIGN KEY ([LotId])              REFERENCES [dbo].[Lots] ([LotId]),
    CONSTRAINT [FK_GvaViewPersonLicences_LotParts]       FOREIGN KEY ([LotPartId])          REFERENCES [dbo].[LotParts] ([LotPartId])
)
GO

exec spDescTable  N'GvaViewPersonLicences', N'Лицензи на физически лица.'
exec spDescColumn N'GvaViewPersonLicences', N'LotId'                     , N'Идентификатор на партида.'
exec spDescColumn N'GvaViewPersonLicences', N'LotPartId'                 , N'Идентификатор на част от партида.'
exec spDescColumn N'GvaViewPersonLicences', N'LicenceType'               , N'Тип лиценз.'
GO
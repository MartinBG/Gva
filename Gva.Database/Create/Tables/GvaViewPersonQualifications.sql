PRINT 'GvaViewPersonQualifications'
GO 

CREATE TABLE [dbo].[GvaViewPersonQualifications] (
    [LotId]                INT          NOT NULL,
    [QualificationCode]    NVARCHAR(50) NOT NULL,
    [QualificationName]    NVARCHAR(50) NOT NULL,
    [LicenceTypeCode]      NVARCHAR(50) NOT NULL,
    [State]                NVARCHAR(20) NULL,
    [StateMethod]          NVARCHAR(20) NULL,
    CONSTRAINT [PK_GvaViewPersonQualifications]                PRIMARY KEY ([LotId], [QualificationCode]),
    CONSTRAINT [FK_GvaViewPersonQualifications_GvaViewPersons] FOREIGN KEY ([LotId])  REFERENCES [dbo].[GvaViewPersons] ([LotId])
)
GO

exec spDescTable  N'GvaViewPersonQualifications', N'Данни за квалификация.'
exec spDescColumn N'GvaViewPersonQualifications', N'LotId'               , N'Идентификатор на партида.'
exec spDescColumn N'GvaViewPersonQualifications', N'QualificationCode'   , N'Код на квалификация.'
exec spDescColumn N'GvaViewPersonQualifications', N'QualificationName'   , N'Наименование на квалификация.'
exec spDescColumn N'GvaViewPersonQualifications', N'LicenceTypeCode'     , N'Код на типа лиценз.'
exec spDescColumn N'GvaViewPersonQualifications', N'State'               , N'Статус на квалификацията.'
exec spDescColumn N'GvaViewPersonQualifications', N'StateMethod'         , N'Метод на вписване на статуса.'
GO

print 'LicenseInstances'
GO

CREATE TABLE [dbo].[LicenseInstances] (
    [LicenseInstanceId]           INT             NOT NULL IDENTITY(1,1),
    [LicenseId]                   INT             NOT NULL,
    [LicenseInstanceTypeId]       INT             NULL,
    [IssueDate]                   DATE            NULL,
    [ValidDate]                   DATE            NULL,
    [PaperNumber]                 NVARCHAR (50)   NULL,
    [ExaminerId]                  INT             NULL,
    [LimOld]                      NVARCHAR (200)  NULL,
    [LimAtA]                      NVARCHAR (200)  NULL,
    [LimAtB1]                     NVARCHAR (200)  NULL,
    [LimApA]                      NVARCHAR (200)  NULL,
    [LimApB1]                     NVARCHAR (200)  NULL,
    [LimHtA]                      NVARCHAR (200)  NULL,
    [LimHtB1]                     NVARCHAR (200)  NULL,
    [LimHpA]                      NVARCHAR (200)  NULL,
    [LimHpB1]                     NVARCHAR (200)  NULL,
    [LimAvionics]                 NVARCHAR (200)  NULL,
    [LimOther]                    NVARCHAR (200)  NULL,
    [LimMedCert]                  NVARCHAR (200)  NULL,
    [LimPeB3]                     NVARCHAR (200)  NULL,
    [Notes]                       NVARCHAR (500)  NULL,
    [NotesLatin]                  NVARCHAR (500)  NULL,
    [Version]                     ROWVERSION      NOT NULL,
    CONSTRAINT [PK_LicenseInstances]                      PRIMARY KEY ([LicenseInstanceId]),
    CONSTRAINT [FK_LicenseInstances_Licenses]             FOREIGN KEY ([LicenseId])             REFERENCES [dbo].[Licenses]             ([LicenseId]),
    CONSTRAINT [FK_LicenseInstances_LicenseInstanceTypes] FOREIGN KEY ([LicenseInstanceTypeId]) REFERENCES [dbo].[LicenseInstanceTypes] ([LicenseInstanceTypeId]),
    CONSTRAINT [FK_LicenseInstances_Examiners]            FOREIGN KEY ([ExaminerId])            REFERENCES [dbo].[Examiners]            ([ExaminerId])
);
GO

exec spDescTable  N'LicenseInstances'                              , N'Лицензи за правоспособност - история.'
exec spDescColumn N'LicenseInstances', N'LicenseInstanceId'        , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'LicenseInstances', N'LicenseId'                , N'Лиценз.'
exec spDescColumn N'LicenseInstances', N'LicenseInstanceTypeId'    , N'Вид действие относно правоспособност.'
exec spDescColumn N'LicenseInstances', N'IssueDate'                , N'Дата на издаване.'
exec spDescColumn N'LicenseInstances', N'ValidDate'                , N'Дата на валидност.'
exec spDescColumn N'LicenseInstances', N'PaperNumber'              , N'Номер на хартиеният лист на свидетелството за правоспособност.'
exec spDescColumn N'LicenseInstances', N'ExaminerId'               , N'Физическо лице издател, ако е необходимо да се съхранява такава информация.'
exec spDescColumn N'LicenseInstances', N'LimOld'                   , N'Ограничения към лиценза (за AML).'
exec spDescColumn N'LicenseInstances', N'LimAtA'                   , N'Ограничения към лиценза (за AML) - Aeroplanes Turbine A.'
exec spDescColumn N'LicenseInstances', N'LimAtB1'                  , N'Ограничения към лиценза (за AML) - Aeroplanes Turbine B1.'
exec spDescColumn N'LicenseInstances', N'LimApA'                   , N'Ограничения към лиценза (за AML) - Aeroplanes Piston A.'
exec spDescColumn N'LicenseInstances', N'LimApB1'                  , N'Ограничения към лиценза (за AML) - Aeroplanes Piston B1.'
exec spDescColumn N'LicenseInstances', N'LimHtA'                   , N'Ограничения към лиценза (за AML) - Helicopters Turbine A.'
exec spDescColumn N'LicenseInstances', N'LimHtB1'                  , N'Ограничения към лиценза (за AML) - Helicopters Turbine B1.'
exec spDescColumn N'LicenseInstances', N'LimHpA'                   , N'Ограничения към лиценза (за AML) - Helicopters Piston A.'
exec spDescColumn N'LicenseInstances', N'LimHpB1'                  , N'Ограничения към лиценза (за AML) - Helicopters Piston B1.'
exec spDescColumn N'LicenseInstances', N'LimAvionics'              , N'Ограничения към лиценза (за AML) - Avionics B2.'
exec spDescColumn N'LicenseInstances', N'LimOther'                 , N'Ограничения към лиценза (за пилоти) - други.'
exec spDescColumn N'LicenseInstances', N'LimMedCert'               , N'Ограничения към лиценза (за пилоти) - медицински.'
exec spDescColumn N'LicenseInstances', N'LimPeB3'                  , N'Ограничения към лиценза (за AML) - B3 Piston-engine non pressurised aeroplanes of 2 000 Kg MTOM and below.'
exec spDescColumn N'LicenseInstances', N'Notes'                    , N'Забележка.'
exec spDescColumn N'LicenseInstances', N'NotesLatin'               , N'Забележка на поддържащ език.'
GO

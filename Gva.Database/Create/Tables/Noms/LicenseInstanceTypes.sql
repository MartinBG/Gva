print 'LicenseInstanceTypes'
GO

CREATE TABLE [dbo].[LicenseInstanceTypes] (
    [LicenseInstanceTypeId]       INT             NOT NULL IDENTITY(1,1),
    [Code]                        NVARCHAR (50)   NULL,
    [Name]                        NVARCHAR (50)   NULL,
    [Version]                     ROWVERSION      NOT NULL,
    CONSTRAINT [PK_LicenseInstanceTypes] PRIMARY KEY ([LicenseInstanceTypeId])
);
GO

exec spDescTable  N'LicenseInstanceTypes'                            , N'Видове действие относно правоспособност.'
exec spDescColumn N'LicenseInstanceTypes', N'LicenseInstanceTypeId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'LicenseInstanceTypes', N'Code'                   , N'Код.'
exec spDescColumn N'LicenseInstanceTypes', N'Name'                   , N'Наименование.'
GO

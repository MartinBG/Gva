print 'LicenseTypes'
GO

CREATE TABLE [dbo].[LicenseTypes] (
    [LicenseTypeId]       INT             NOT NULL IDENTITY(1,1),
    [Code]                NVARCHAR (50)   NULL,
    [Name]                NVARCHAR (50)   NULL,
    [Version]             ROWVERSION      NOT NULL,
    CONSTRAINT [PK_LicenseTypes] PRIMARY KEY ([LicenseTypeId])
);
GO

exec spDescTable  N'LicenseTypes'                    , N'Видове правоспособност.'
exec spDescColumn N'LicenseTypes', N'LicenseTypeId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'LicenseTypes', N'Code'           , N'Код.'
exec spDescColumn N'LicenseTypes', N'Name'           , N'Наименование.'
GO

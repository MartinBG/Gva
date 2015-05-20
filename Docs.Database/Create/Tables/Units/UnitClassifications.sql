print 'UnitClassifications'
GO 

CREATE TABLE UnitClassifications
(
    UnitClassificationId        INT IDENTITY (1, 1) NOT NULL,
    UnitId                      INT                 NOT NULL,
    ClassificationId            INT                 NOT NULL,
    ClassificationPermissionId  INT                 NOT NULL,
    Version                     ROWVERSION          NOT NULL,
    CONSTRAINT PK_UnitClassifications PRIMARY KEY CLUSTERED (UnitClassificationId),
    CONSTRAINT [FK_UnitClassifications_Units] FOREIGN KEY ([UnitId]) REFERENCES [dbo].[Units] ([UnitId]),
    CONSTRAINT [FK_UnitClassifications_Classifications] FOREIGN KEY ([ClassificationId]) REFERENCES [dbo].[Classifications] ([ClassificationId]),
    CONSTRAINT [FK_UnitClassifications_ClassificationPermissions] FOREIGN KEY ([ClassificationPermissionId]) REFERENCES [dbo].[ClassificationPermissions] ([ClassificationPermissionId]),
)
GO 

exec spDescTable  N'UnitClassifications', N'Класификационни схеми на документ.'
exec spDescColumn N'UnitClassifications', N'UnitClassificationId', N'Уникален системно генериран идентификатор.'

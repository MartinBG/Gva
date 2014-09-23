print 'ClassificationPermissions'
GO 

CREATE TABLE [dbo].[ClassificationPermissions]
(
    ClassificationPermissionId  INT            IDENTITY (1, 1) NOT NULL,
    Name                        NVARCHAR (200) NOT NULL,
    Alias                       NVARCHAR (200) NOT NULL,
    IsActive                    BIT            NOT NULL,
    Version                     ROWVERSION     NOT NULL,
    CONSTRAINT PK_ClassificationPermissions PRIMARY KEY CLUSTERED (ClassificationPermissionId)
)
GO 

exec spDescTable  N'ClassificationPermissions', N'Права върху схема.'
exec spDescColumn N'ClassificationPermissions', N'ClassificationPermissionId', N'Уникален системно генериран идентификатор.'

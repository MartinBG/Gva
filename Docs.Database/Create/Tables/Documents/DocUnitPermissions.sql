print 'DocUnitPermissions'
GO 

CREATE TABLE DocUnitPermissions
(
    DocUnitPermissionId	INT            IDENTITY (1, 1) NOT NULL,
    Name			NVARCHAR (200) NOT NULL,
	Alias			NVARCHAR (200) NOT NULL,
    IsActive		BIT            NOT NULL,
    Version			ROWVERSION     NOT NULL,
    CONSTRAINT PK_DocUnitPermissions PRIMARY KEY CLUSTERED (DocUnitPermissionId)
)
GO 

exec spDescTable  N'DocUnitPermissions', N'Права върху документ.'
exec spDescColumn N'DocUnitPermissions', N'DocUnitPermissionId', N'Уникален системно генериран идентификатор.'

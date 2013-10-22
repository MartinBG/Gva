print 'ClassificationRoles'
GO 

CREATE TABLE ClassificationRoles
(
    ClassificationRoleId	INT            IDENTITY (1, 1) NOT NULL,
    Name			NVARCHAR (200) NOT NULL,
	Alias			NVARCHAR (200) NOT NULL,
    IsActive		BIT            NOT NULL,
    Version			ROWVERSION     NOT NULL,
    CONSTRAINT PK_ClassificationRoles PRIMARY KEY CLUSTERED (ClassificationRoleId)
)
GO 

exec spDescTable  N'ClassificationRoles', N'Права върху класификационна схема.'
exec spDescColumn N'ClassificationRoles', N'ClassificationRoleId', N'Уникален системно генериран идентификатор.'

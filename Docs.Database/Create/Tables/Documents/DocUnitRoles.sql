print 'DocUnitRoles'
GO 

CREATE TABLE DocUnitRoles
(
    DocUnitRoleId	INT            IDENTITY (1, 1) NOT NULL,
    Name			NVARCHAR (200) NOT NULL,
	Alias			NVARCHAR (200) NOT NULL,
    IsActive		BIT            NOT NULL,
    Version			ROWVERSION     NOT NULL,
    CONSTRAINT PK_DocUnitRoles PRIMARY KEY CLUSTERED (DocUnitRoleId)
)
GO 

exec spDescTable  N'DocUnitRoles', N'Роля на звено по документ.'
exec spDescColumn N'DocUnitRoles', N'DocUnitRoleId', N'Уникален системно генериран идентификатор.'

PRINT 'Roles'
GO

CREATE TABLE [dbo].[Roles] (
    [RoleId]        INT              NOT NULL IDENTITY,
    [Name]          NVARCHAR (200)   NOT NULL,
    [Permissions]   NVARCHAR (1000)  NULL,
    [IsActive]      BIT              NOT NULL,
    [Version]       ROWVERSION       NOT NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([RoleId])
);
GO

exec spDescTable  N'Roles', N'Роли'
exec spDescColumn N'Roles', N'RoleId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Roles', N'Name'       , N'Име на ролята.'
exec spDescColumn N'Roles', N'Permissions', N'Права.'
exec spDescColumn N'Roles', N'IsActive'   , N'Маркер за активност.'
exec spDescColumn N'Roles', N'Version'    , N'Версия.'
GO

PRINT 'UserRoles'
GO

CREATE TABLE [dbo].[UserRoles] (
    [UserId]     INT            NOT NULL,
    [RoleId]     INT            NOT NULL,
    CONSTRAINT [PK_UserRoles]       PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_UserRoles_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [FK_UserRoles_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([RoleId])
);
GO

exec spDescTable  N'UserRoles', N'Таблица за връзка м/у потребители и роли.'
exec spDescColumn N'UserRoles', N'UserId', N'Потребител.'
exec spDescColumn N'UserRoles', N'RoleId', N'Роля.'

GO

print 'ExperienceRoles'
GO

CREATE TABLE [dbo].[ExperienceRoles] (
    [ExperienceRoleId]       INT             NOT NULL IDENTITY(1,1),
    [Code]                   NVARCHAR (50)   NULL,
    [Name]                   NVARCHAR (50)   NULL,
    [Version]                ROWVERSION      NOT NULL,
    CONSTRAINT [PK_ExperienceRoles] PRIMARY KEY ([ExperienceRoleId])
);
GO

exec spDescTable  N'ExperienceRoles'                       , N'Роли в натрупан летателен опит.'
exec spDescColumn N'ExperienceRoles', N'ExperienceRoleId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ExperienceRoles', N'Code'              , N'Код.'
exec spDescColumn N'ExperienceRoles', N'Name'              , N'Наименование.'
GO

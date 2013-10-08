print 'Authorizations'
GO

CREATE TABLE [dbo].[Authorizations] (
    [AuthorizationId]       INT             NOT NULL IDENTITY(1,1),
    [Code]                  NVARCHAR (50)   NULL,
    [Name]                  NVARCHAR (50)   NULL,
    [Version]               ROWVERSION      NOT NULL,
    CONSTRAINT [PK_Authorizations] PRIMARY KEY ([AuthorizationId])
);
GO

exec spDescTable  N'Authorizations'                      , N'Разрешения'
exec spDescColumn N'Authorizations', N'AuthorizationId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Authorizations', N'Code'             , N'Код.'
exec spDescColumn N'Authorizations', N'Name'             , N'Наименование.'
GO

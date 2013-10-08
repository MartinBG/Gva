print 'Organizations'
GO

CREATE TABLE [dbo].[Organizations] (
    [OrganizationId] INT             NOT NULL IDENTITY(1,1),
    [Name]           NVARCHAR (50)   NULL,
    [Version]        ROWVERSION      NOT NULL,
    CONSTRAINT [PK_Organizations] PRIMARY KEY ([OrganizationId])
);
GO

exec spDescTable  N'Organizations'                     , N'Организации'
exec spDescColumn N'Organizations', N'OrganizationId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Organizations', N'Name'            , N'Наименование.'
GO

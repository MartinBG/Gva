print 'Schools'
GO

CREATE TABLE [dbo].[Schools] (
    [SchoolId]       INT             NOT NULL IDENTITY(1,1),
    [Code]           NVARCHAR (50)   NULL,
    [Name]           NVARCHAR (50)   NULL,
    [Version]        ROWVERSION      NOT NULL,
    CONSTRAINT [PK_Schools] PRIMARY KEY ([SchoolId])
);
GO

exec spDescTable  N'Schools'               , N'Учебни заведения'
exec spDescColumn N'Schools', N'SchoolId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Schools', N'Code'      , N'Код.'
exec spDescColumn N'Schools', N'Name'      , N'Наименование.'
GO

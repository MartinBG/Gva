print 'Genders'
GO

CREATE TABLE [dbo].[Genders] (
    [GenderId]       INT             NOT NULL IDENTITY(1,1),
    [Code]           NVARCHAR (50)   NULL,
    [Name]           NVARCHAR (50)   NULL,
    [Version]        ROWVERSION      NOT NULL,
    CONSTRAINT [PK_Genders] PRIMARY KEY ([GenderId])
);
GO

exec spDescTable  N'Genders'               , N'Пол на физическо лице'
exec spDescColumn N'Genders', N'GenderId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Genders', N'Code'      , N'Код.'
exec spDescColumn N'Genders', N'Name'      , N'Наименование.'
GO

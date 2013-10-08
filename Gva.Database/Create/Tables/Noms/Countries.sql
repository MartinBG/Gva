print 'Countries'
GO

CREATE TABLE [dbo].[Countries] (
    [CountryId]       INT             NOT NULL IDENTITY(1,1),
    [Code]            NVARCHAR (50)   NULL,
    [Name]            NVARCHAR (50)   NULL,
    [Version]         ROWVERSION      NOT NULL,
    CONSTRAINT [PK_Countries] PRIMARY KEY ([CountryId])
);
GO

exec spDescTable  N'Countries'                , N'Държави'
exec spDescColumn N'Countries', N'CountryId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Countries', N'Code'       , N'Код.'
exec spDescColumn N'Countries', N'Name'       , N'Наименование.'
GO

PRINT 'Countries'
GO 

CREATE TABLE [dbo].[Countries] (
    [CountryId]       INT            NOT NULL IDENTITY,
    [Name]            NVARCHAR (200) NOT NULL,
    [NumericCode]     NVARCHAR (50)  NOT NULL,
    [Alpha3Code]      NVARCHAR (50)  NOT NULL,
    [Alpha2Code]      NVARCHAR (50)  NOT NULL,
    [Alias]           NVARCHAR (200) NULL,
    [Description]     NVARCHAR (MAX) NULL,
    [IsActive]        BIT            NOT NULL,
    [Version]         ROWVERSION     NOT NULL,
    CONSTRAINT [PK_Countries] PRIMARY KEY ([CountryId]),
);
GO

exec spDescTable  N'Countries', N'Държави.'
exec spDescColumn N'Countries', N'CountryId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Countries', N'Name'       , N'Наименование.'
exec spDescColumn N'Countries', N'NumericCode', N'ISO 3166-1 числов код.'
exec spDescColumn N'Countries', N'Alpha3Code' , N'ISO 3166-1 alpha-3 код.'
exec spDescColumn N'Countries', N'Alpha2Code' , N'ISO 3166-1 alpha-2 код.'
exec spDescColumn N'Countries', N'Alias'      , N'Символен идентификатор.'
exec spDescColumn N'Countries', N'Description', N'Описание.'
exec spDescColumn N'Countries', N'IsActive'   , N'Маркер за активност'
exec spDescColumn N'Countries', N'Version'    , N'Версия.'
GO

PRINT 'NomValues'
GO 

CREATE TABLE [dbo].[NomValues](
    [NomValueId]   INT            NOT NULL IDENTITY,
    [NomId]        INT            NOT NULL,
    [Code]         NVARCHAR (50)  NOT NULL,
    [Name]         NVARCHAR (50)  NOT NULL,
    [Alias]        NVARCHAR (50)  NULL,
    [TextContent]  NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_NomValues]      PRIMARY KEY ([NomValueId]),
    CONSTRAINT [FK_NomValues_Noms] FOREIGN KEY ([NomId]) REFERENCES [dbo].[Noms] ([NomId])
)
GO

exec spDescTable  N'NomValues', N'Стойности на номенклатури.'
exec spDescColumn N'NomValues', N'NomValueId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'NomValues', N'NomId'        , N'Номенклатура.'
exec spDescColumn N'NomValues', N'Code'         , N'Код.'
exec spDescColumn N'NomValues', N'Name'         , N'Наименование.'
exec spDescColumn N'NomValues', N'Alias'        , N'Символен идентификатор.'
exec spDescColumn N'NomValues', N'TextContent'  , N'Съдържание.'
GO

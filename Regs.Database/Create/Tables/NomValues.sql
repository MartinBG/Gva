PRINT 'NomValues'
GO 

CREATE TABLE [dbo].[NomValues](
    [NomValueId]      INT            NOT NULL IDENTITY,
    [NomId]           INT            NOT NULL,
    [Code]            NVARCHAR (500) NULL,
    [Name]            NVARCHAR (500) NOT NULL,
    [NameAlt]         NVARCHAR (500) NULL,
    [ParentValueId]   INT            NULL,
    [Alias]           NVARCHAR (50)  NULL,
    [TextContent]     NVARCHAR (MAX) NULL,
    [IsActive]        BIT            NULL,
    CONSTRAINT [PK_NomValues]           PRIMARY KEY ([NomValueId]),
    CONSTRAINT [FK_NomValues_Noms]      FOREIGN KEY ([NomId])         REFERENCES [dbo].[Noms] ([NomId]),
    CONSTRAINT [FK_NomValues_NomValues] FOREIGN KEY ([ParentValueId]) REFERENCES [dbo].[NomValues] ([NomValueId])
)
GO

exec spDescTable  N'NomValues', N'Стойности на номенклатури.'
exec spDescColumn N'NomValues', N'NomValueId'    , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'NomValues', N'NomId'         , N'Номенклатура.'
exec spDescColumn N'NomValues', N'Code'          , N'Код.'
exec spDescColumn N'NomValues', N'Name'          , N'Наименование.'
exec spDescColumn N'NomValues', N'NameAlt'       , N'Наименование на поддържащ език.'
exec spDescColumn N'NomValues', N'ParentValueId' , N'Идентификатор на базовата номенклатура.'
exec spDescColumn N'NomValues', N'Alias'         , N'Символен идентификатор.'
exec spDescColumn N'NomValues', N'TextContent'   , N'Съдържание.'
exec spDescColumn N'NomValues', N'IsActive'      , N'Маркер за валидност.'
GO

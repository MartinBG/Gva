PRINT 'Noms'
GO 

CREATE TABLE [dbo].[Noms] (
    [NomId]   INT            NOT NULL,
    [Name]    NVARCHAR (500) NOT NULL,
    [Alias]   NVARCHAR (50)  NOT NULL,
 CONSTRAINT [PK_Noms] PRIMARY KEY ([NomId])
)
GO

exec spDescTable  N'Noms', N'Номенклатури.'
exec spDescColumn N'Noms', N'NomId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Noms', N'Name'   , N'Наименование.'
exec spDescColumn N'Noms', N'Alias'  , N'Символен идентификатор.'
GO

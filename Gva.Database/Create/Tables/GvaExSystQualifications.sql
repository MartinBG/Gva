PRINT 'GvaExSystQualifications'
GO 

CREATE TABLE [dbo].[GvaExSystQualifications] (
    [Name]    NVARCHAR(200)  NOT NULL,
    [Code]    NVARCHAR(200)  NOT NULL UNIQUE,
    CONSTRAINT [PK_GvaExSystQualifications] PRIMARY KEY ([Code])
)
GO

exec spDescTable  N'GvaExSystQualifications', N'Квалификации от изпитната система.'
exec spDescColumn N'GvaExSystQualifications', N'Name'  , N'Наименование.'
exec spDescColumn N'GvaExSystQualifications', N'Code'  , N'Код.'
GO

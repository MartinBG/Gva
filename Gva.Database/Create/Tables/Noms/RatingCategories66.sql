print 'RatingCategories66'
GO

CREATE TABLE [dbo].[RatingCategories66] (
    [RatingCategory66Id]       INT             NOT NULL IDENTITY(1,1),
    [Code]                     NVARCHAR (50)   NULL,
    [Name]                     NVARCHAR (50)   NULL,
    [Version]                  ROWVERSION      NOT NULL,
    CONSTRAINT [PK_RatingCategories66] PRIMARY KEY ([RatingCategory66Id])
);
GO

exec spDescTable  N'RatingCategories66'                         , N'Категория за AML (Part 66).'
exec spDescColumn N'RatingCategories66', N'RatingCategory66Id'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'RatingCategories66', N'Code'                , N'Код.'
exec spDescColumn N'RatingCategories66', N'Name'                , N'Наименование.'
GO

print 'RatingModels'
GO

CREATE TABLE [dbo].[RatingModels] (
    [RatingModelId]      INT             NOT NULL IDENTITY(1,1),
    [Code]               NVARCHAR (50)   NULL,
    [Name]               NVARCHAR (50)   NULL,
    [Version]            ROWVERSION      NOT NULL,
    CONSTRAINT [PK_RatingModels] PRIMARY KEY ([RatingModelId])
);
GO

exec spDescTable  N'RatingModels'                    , N'Пол на физическо лице'
exec spDescColumn N'RatingModels', N'RatingModelId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'RatingModels', N'Code'           , N'Код.'
exec spDescColumn N'RatingModels', N'Name'           , N'Наименование.'
GO

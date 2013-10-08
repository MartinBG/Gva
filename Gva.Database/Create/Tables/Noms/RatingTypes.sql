print 'RatingTypes'
GO

CREATE TABLE [dbo].[RatingTypes] (
    [RatingTypeId]       INT             NOT NULL IDENTITY(1,1),
    [Code]               NVARCHAR (50)   NULL,
    [Name]               NVARCHAR (50)   NULL,
    [Version]            ROWVERSION      NOT NULL,
    CONSTRAINT [PK_RatingTypes] PRIMARY KEY ([RatingTypeId])
);
GO

exec spDescTable  N'RatingTypes'                   , N'Типове ВС'
exec spDescColumn N'RatingTypes', N'RatingTypeId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'RatingTypes', N'Code'          , N'Код.'
exec spDescColumn N'RatingTypes', N'Name'          , N'Наименование.'
GO

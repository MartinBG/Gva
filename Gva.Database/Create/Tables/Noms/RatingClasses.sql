print 'RatingClasses'
GO

CREATE TABLE [dbo].[RatingClasses] (
    [RatingClassId]    INT             NOT NULL IDENTITY(1,1),
    [Code]              NVARCHAR (50)   NULL,
    [Name]              NVARCHAR (50)   NULL,
    [Version]           ROWVERSION      NOT NULL,
    CONSTRAINT [PK_RatingClasses] PRIMARY KEY ([RatingClassId])
);
GO

exec spDescTable  N'RatingClasses'                    , N'Класове ВС'
exec spDescColumn N'RatingClasses', N'RatingClassId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'RatingClasses', N'Code'           , N'Код.'
exec spDescColumn N'RatingClasses', N'Name'           , N'Наименование.'
GO

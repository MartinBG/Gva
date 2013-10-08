print 'RatingGroups66'
GO

CREATE TABLE [dbo].[RatingGroups66] (
    [RatingGroup66Id]       INT             NOT NULL IDENTITY(1,1),
    [Code]                  NVARCHAR (50)   NULL,
    [Name]                  NVARCHAR (50)   NULL,
    [Version]               ROWVERSION      NOT NULL,
    CONSTRAINT [PK_RatingGroups66] PRIMARY KEY ([RatingGroup66Id])
);
GO

exec spDescTable  N'RatingGroups66'                      , N'Пол на физическо лице'
exec spDescColumn N'RatingGroups66', N'RatingGroup66Id'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'RatingGroups66', N'Code'             , N'Код.'
exec spDescColumn N'RatingGroups66', N'Name'             , N'Наименование.'
GO

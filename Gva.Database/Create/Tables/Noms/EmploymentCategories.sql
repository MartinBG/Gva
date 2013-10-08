print 'EmploymentCategories'
GO

CREATE TABLE [dbo].[EmploymentCategories] (
    [EmploymentCategoryId]   INT             NOT NULL IDENTITY(1,1),
    [Code]                   NVARCHAR (50)   NULL,
    [Name]                   NVARCHAR (50)   NULL,
    [Version]                ROWVERSION      NOT NULL,
    CONSTRAINT [PK_EmploymentCategories] PRIMARY KEY ([EmploymentCategoryId])
);
GO

exec spDescTable  N'EmploymentCategories'                           , N'Пол на физическо лице'
exec spDescColumn N'EmploymentCategories', N'EmploymentCategoryId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'EmploymentCategories', N'Code'                  , N'Код.'
exec spDescColumn N'EmploymentCategories', N'Name'                  , N'Наименование.'
GO

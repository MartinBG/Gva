print 'Employments'
GO

CREATE TABLE [dbo].[Employments] (
    [EmploymentId]          INT             NOT NULL IDENTITY(1,1),
    [PersonId]              INT             NOT NULL,
    [Hiredate]              DATE            NULL,
    [OrganizationId]        INT             NULL,
    [EmploymentCategoryId]  INT             NULL,
    [CountryId]             INT             NULL,
    [Notes]                 NVARCHAR (500)  NULL,
    [IsValid]               BIT             NOT NULL,
    [Version]               ROWVERSION      NOT NULL,
    CONSTRAINT [PK_Employments]                      PRIMARY KEY ([EmploymentId]),
    CONSTRAINT [FK_Employments_Persons]              FOREIGN KEY ([PersonId])             REFERENCES [dbo].[Persons]              ([PersonId]),
    CONSTRAINT [FK_Employments_Organizations]        FOREIGN KEY ([OrganizationId])       REFERENCES [dbo].[Organizations]        ([OrganizationId]),
    CONSTRAINT [FK_Employments_EmploymentCategories] FOREIGN KEY ([EmploymentCategoryId]) REFERENCES [dbo].[EmploymentCategories] ([EmploymentCategoryId]),
    CONSTRAINT [FK_Employments_Countries]            FOREIGN KEY ([CountryId])            REFERENCES [dbo].[Countries]            ([CountryId])
);
GO

exec spDescTable  N'Employments'                         , N'Месторабота.'
exec spDescColumn N'Employments', N'EmploymentId'        , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Employments', N'PersonId'            , N'Физическо лице.'
exec spDescColumn N'Employments', N'Hiredate'            , N'Дата на назначаване.'
exec spDescColumn N'Employments', N'OrganizationId'      , N'Организация.'
exec spDescColumn N'Employments', N'EmploymentCategoryId', N'Категория персонал.'
exec spDescColumn N'Employments', N'CountryId'           , N'Държава в която се намира местоработата.'
exec spDescColumn N'Employments', N'Notes'               , N'Бележки.'
exec spDescColumn N'Employments', N'IsValid'             , N'Валидност.'
GO

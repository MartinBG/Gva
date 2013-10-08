print 'EducationDegrees'
GO

CREATE TABLE [dbo].[EducationDegrees] (
    [EducationDegreeId] INT             NOT NULL IDENTITY(1,1),
    [Code]              NVARCHAR (50)   NULL,
    [Name]              NVARCHAR (50)   NULL,
    [Version]           ROWVERSION      NOT NULL,
    CONSTRAINT [PK_EducationDegrees] PRIMARY KEY ([EducationDegreeId])
);
GO

exec spDescTable  N'EducationDegrees' , N'Образователни степени'
exec spDescColumn N'EducationDegrees' , N'EducationDegreeId' , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'EducationDegrees' , N'Code'              , N'Код.'
exec spDescColumn N'EducationDegrees' , N'Name'              , N'Наименование.'
GO

print 'Educations'
GO

CREATE TABLE [dbo].[Educations] (
    [EducationId]           INT             NOT NULL IDENTITY(1,1),
    [PersonId]              INT             NOT NULL,
    [DegreeNumber]          NVARCHAR (50)   NULL,
    [CompletionDate]        DATE            NULL,
    [Speciality]            NVARCHAR (50)   NULL,
    [SchoolId]              INT             NULL,
    [EducationDegreeId]     INT             NULL,
    [Notes]                 NVARCHAR (500)  NULL,
    [Version]               ROWVERSION      NOT NULL,
    CONSTRAINT [PK_Educations]                  PRIMARY KEY ([EducationId]),
    CONSTRAINT [FK_Educations_Persons]          FOREIGN KEY ([PersonId])          REFERENCES [dbo].[Persons]          ([PersonId]),
    CONSTRAINT [FK_Educations_Schools]          FOREIGN KEY ([SchoolId])          REFERENCES [dbo].[Schools]          ([SchoolId]),
    CONSTRAINT [FK_Educations_EducationDegrees] FOREIGN KEY ([EducationDegreeId]) REFERENCES [dbo].[EducationDegrees] ([EducationDegreeId])
);
GO

exec spDescTable  N'Educations'                       , N'Образование.'
exec spDescColumn N'Educations', N'EducationId'       , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Educations', N'PersonId'          , N'Физическо лице.'
exec spDescColumn N'Educations', N'DegreeNumber'      , N'Идентификация на диплома.'
exec spDescColumn N'Educations', N'CompletionDate'    , N'Дата на завършване.'
exec spDescColumn N'Educations', N'Speciality'        , N'Специалност.'
exec spDescColumn N'Educations', N'SchoolId'          , N'Учебно заведение.'
exec spDescColumn N'Educations', N'EducationDegreeId' , N'Степен на образование.'
exec spDescColumn N'Educations', N'Notes'             , N'Бележки.'
GO

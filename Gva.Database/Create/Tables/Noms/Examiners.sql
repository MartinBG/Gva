print 'Examiners'
GO

CREATE TABLE [dbo].[Examiners] (
    [ExaminerId]       INT             NOT NULL IDENTITY(1,1),
    [Code]             NVARCHAR (50)   NULL,
    [Name]             NVARCHAR (50)   NULL,
    [Version]          ROWVERSION      NOT NULL,
    CONSTRAINT [PK_Examiners] PRIMARY KEY ([ExaminerId])
);
GO

exec spDescTable  N'Examiners'                 , N'Пол на физическо лице'
exec spDescColumn N'Examiners', N'ExaminerId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Examiners', N'Code'        , N'Код.'
exec spDescColumn N'Examiners', N'Name'        , N'Наименование.'
GO

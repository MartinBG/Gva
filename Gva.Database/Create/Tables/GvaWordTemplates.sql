PRINT 'GvaWordTemplates'
GO 

CREATE TABLE [dbo].[GvaWordTemplates] (
    [GvaWordTemplateId] INT            NOT NULL IDENTITY,
    [Name]              NVARCHAR (200) NOT NULL,
    [Template]          VARBINARY(MAX) NOT NULL,
    CONSTRAINT [PK_GvaWordTemplates]         PRIMARY KEY ([GvaWordTemplateId]),
)
GO

exec spDescTable  N'GvaWordTemplates', N'Шаблони за принтиране на документи.'
exec spDescColumn N'GvaWordTemplates', N'GvaWordTemplateId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'GvaWordTemplates', N'Name'             , N'Име на шаблона.'
exec spDescColumn N'GvaWordTemplates', N'Template'         , N'Шаблон.'
GO

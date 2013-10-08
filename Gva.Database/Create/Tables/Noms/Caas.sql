print 'Caas'
GO

CREATE TABLE [dbo].[Caas] (
    [CaaId]       INT             NOT NULL IDENTITY(1,1),
    [Code]        NVARCHAR (50)   NULL,
    [Name]        NVARCHAR (50)   NULL,
    [Version]     ROWVERSION      NOT NULL,
    CONSTRAINT [PK_Caas] PRIMARY KEY ([CaaId])
);
GO

exec spDescTable  N'Caas'            , N'Граждански въздухоплавателни администрации'
exec spDescColumn N'Caas', N'CaaId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Caas', N'Code'   , N'Код.'
exec spDescColumn N'Caas', N'Name'   , N'Наименование.'
GO

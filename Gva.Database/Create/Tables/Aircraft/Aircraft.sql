print 'Aircraft'
GO

CREATE TABLE [dbo].[Aircraft] (
    [AircraftId]     INT             NOT NULL IDENTITY(1,1),
    [Code]           NVARCHAR (50)   NULL,
    [Name]           NVARCHAR (50)   NULL,
    [Version]        ROWVERSION      NOT NULL,
    CONSTRAINT [PK_Aircraft] PRIMARY KEY ([AircraftId])
);
GO

exec spDescTable  N'Aircraft'                , N'Въздухоплавателни средства'
exec spDescColumn N'Aircraft', N'AircraftId' , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Aircraft', N'Code'       , N'Код.'
exec spDescColumn N'Aircraft', N'Name'       , N'Наименование.'
GO

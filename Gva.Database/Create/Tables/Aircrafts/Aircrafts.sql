print 'Aircrafts'
GO

CREATE TABLE [dbo].[Aircrafts] (
    [AircraftId]     INT             NOT NULL IDENTITY(1,1),
    [Code]           NVARCHAR (50)   NULL,
    [Name]           NVARCHAR (50)   NULL,
    [Version]        ROWVERSION      NOT NULL,
    CONSTRAINT [PK_Aircrafts] PRIMARY KEY ([AircraftId])
);
GO

exec spDescTable  N'Aircrafts'                , N'Въздухоплавателни средства'
exec spDescColumn N'Aircrafts', N'AircraftId' , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Aircrafts', N'Code'       , N'Код.'
exec spDescColumn N'Aircrafts', N'Name'       , N'Наименование.'
GO

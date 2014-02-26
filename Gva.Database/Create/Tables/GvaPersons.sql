PRINT 'GvaPersons'
GO 

CREATE TABLE [dbo].[GvaPersons] (
    [GvaPersonLotId] INT           NOT NULL,
    [Lin]            NVARCHAR(50)  NOT NULL,
    [Uin]            NVARCHAR(50)  NULL,
    [Names]          NVARCHAR(MAX) NOT NULL,
    [Age]            INT           NOT NULL,
    [Licences]       NVARCHAR(MAX) NULL,
    [Ratings]        NVARCHAR(MAX) NULL,
    [Organization]   NVARCHAR(50)  NULL,
    CONSTRAINT [PK_GvaPersons]      PRIMARY KEY ([GvaPersonLotId]),
    CONSTRAINT [FK_GvaPersons_Lots]  FOREIGN KEY ([GvaPersonLotId]) REFERENCES [dbo].[Lots] ([LotId])
)
GO

exec spDescTable  N'GvaPersons', N'Физически лица.'
exec spDescColumn N'GvaPersons', N'GvaPersonLotId', N'Идентификатор на партида на физическо лице.'
exec spDescColumn N'GvaPersons', N'Lin'           , N'Личен идентификационен номер.'
exec spDescColumn N'GvaPersons', N'Uin'           , N'Единен граждански номер.'
exec spDescColumn N'GvaPersons', N'Names'         , N'Имена.'
exec spDescColumn N'GvaPersons', N'Age'           , N'Възраст.'
exec spDescColumn N'GvaPersons', N'Licences'      , N'Лицензи.'
exec spDescColumn N'GvaPersons', N'Ratings'       , N'Квалификации.'
exec spDescColumn N'GvaPersons', N'Organization'  , N'Фирма.'
GO

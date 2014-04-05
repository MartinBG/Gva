PRINT 'GvaViewAircrafts'
GO 

CREATE TABLE [dbo].[GvaViewAircrafts] (
    [LotId]            INT       NOT NULL,
    [ManSN]            NVARCHAR(50)  NULL,
    [Model]            NVARCHAR(50)  NULL,
    [ModelAlt]         NVARCHAR(50)  NULL,
    [OutputDate]       DATETIME2     NULL,
    [ICAO]             NVARCHAR(50)  NULL,
    [AircraftCategory] NVARCHAR(MAX) NULL,
    [AircraftProducer] NVARCHAR(MAX) NULL,
    [Engine]           NVARCHAR(50)  NULL,
    [Propeller]        NVARCHAR(50)  NULL,
    [ModifOrWingColor] NVARCHAR(50)  NULL,
    [Mark]             NVARCHAR(50)  NULL,
    CONSTRAINT [PK_GvaViewAircrafts]      PRIMARY KEY ([LotId]),
    CONSTRAINT [FK_GvaViewAircrafts_Lots]  FOREIGN KEY ([LotId]) REFERENCES [dbo].[Lots] ([LotId])
)
GO

exec spDescTable  N'GvaViewAircrafts', N'Физически лица.'
exec spDescColumn N'GvaViewAircrafts', N'LotId', N'Идентификатор на партида на ВС.'
exec spDescColumn N'GvaViewAircrafts', N'ManSN'           , N'Личен идентификационен номер.'
exec spDescColumn N'GvaViewAircrafts', N'Model'           , N'Единен граждански номер.'
exec spDescColumn N'GvaViewAircrafts', N'ModelAlt'           , N'Единен граждански номер.'
exec spDescColumn N'GvaViewAircrafts', N'OutputDate'         , N'Имена.'
exec spDescColumn N'GvaViewAircrafts', N'ICAO'           , N'Възраст.'
exec spDescColumn N'GvaViewAircrafts', N'AircraftCategory'      , N'Лицензи.'
exec spDescColumn N'GvaViewAircrafts', N'AircraftProducer'       , N'Квалификации.'
exec spDescColumn N'GvaViewAircrafts', N'Engine'  , N'Фирма.'
exec spDescColumn N'GvaViewAircrafts', N'Propeller'    , N'Длъжност.'
exec spDescColumn N'GvaViewAircrafts', N'ModifOrWingColor'    , N'Длъжност2.'
exec spDescColumn N'GvaViewAircrafts', N'Mark'    , N'Длъжност3.'
GO

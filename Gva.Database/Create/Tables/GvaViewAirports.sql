PRINT 'GvaViewAirports'
GO 

CREATE TABLE [dbo].[GvaViewAirports] (
    [LotId]           INT            NOT NULL,
    [Name]            NVARCHAR(50)   NOT NULL,
    [NameAlt]         NVARCHAR(50)   NOT NULL,
    [AirportTypeId]   INT            NOT NULL,
    [Place]           NVARCHAR(50)   NULL,
    [ICAO]            NVARCHAR(50)   NULL,
    [Runway]          NVARCHAR(50)   NULL,
    [Course]          NVARCHAR(50)   NULL,
    [Excess]          NVARCHAR(50)   NULL,
    [Concrete]        NVARCHAR(50)   NULL,
    CONSTRAINT [PK_GvaViewAirports]             PRIMARY KEY ([LotId]),
    CONSTRAINT [FK_GvaViewAirports_Lots]        FOREIGN KEY ([LotId])         REFERENCES [dbo].[Lots] ([LotId]),
    CONSTRAINT [FK__GvaViewAirports_NomValues]  FOREIGN KEY ([AirportTypeId]) REFERENCES [dbo].[NomValues] ([NomValueId]),
)
GO

exec spDescTable  N'GvaViewAirports', N'Летища.'
exec spDescColumn N'GvaViewAirports', N'LotId'        , N'Идентификатор на партида на ВС.'
exec spDescColumn N'GvaViewAirports', N'Name'         , N'Наименование.'
exec spDescColumn N'GvaViewAirports', N'NameAlt'      , N'Наименование.(англ.)'
exec spDescColumn N'GvaViewAirports', N'AirportTypeId', N'Вид летище.'
exec spDescColumn N'GvaViewAirports', N'Place'        , N'Местоположение.'
exec spDescColumn N'GvaViewAirports', N'ICAO'         , N'ICAO код.'
exec spDescColumn N'GvaViewAirports', N'Runway'       , N'Лицензи.'
exec spDescColumn N'GvaViewAirports', N'Course'       , N'Квалификации.'
exec spDescColumn N'GvaViewAirports', N'Excess'       , N'Фирма.'
exec spDescColumn N'GvaViewAirports', N'Concrete'     , N'Длъжност.'
GO

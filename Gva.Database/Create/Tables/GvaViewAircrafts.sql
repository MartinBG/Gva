PRINT 'GvaViewAircrafts'
GO 

CREATE TABLE [dbo].[GvaViewAircrafts] (
    [LotId]               INT           NOT NULL,
    [ManSN]               NVARCHAR(50)  NULL,
    [Model]               NVARCHAR(50)  NULL,
    [ModelAlt]            NVARCHAR(50)  NULL,
    [OutputDate]          DATETIME2     NULL,
    [ICAO]                NVARCHAR(50)  NULL,
    [AirCategoryId]       INT           NULL,
    [AircraftProducerId]  INT           NULL,
    [Engine]              NVARCHAR(50)  NULL,
    [Propeller]           NVARCHAR(50)  NULL,
    [ModifOrWingColor]    NVARCHAR(50)  NULL,
    [EngineAlt]           NVARCHAR(50)  NULL,
    [PropellerAlt]        NVARCHAR(50)  NULL,
    [ModifOrWingColorAlt] NVARCHAR(50)  NULL,
    [Mark]                NVARCHAR(50)  NULL,
    [CertNumber]          INT           NULL,
    [ActNumber]           INT           NULL,
    CONSTRAINT [PK_GvaViewAircrafts]             PRIMARY KEY ([LotId]),
    CONSTRAINT [FK_GvaViewAircrafts_Lots]        FOREIGN KEY ([LotId])              REFERENCES [dbo].[Lots] ([LotId]),
    CONSTRAINT [FK_GvaViewAircrafts_NomValues]   FOREIGN KEY ([AirCategoryId])      REFERENCES [dbo].[NomValues] ([NomValueId]),
    CONSTRAINT [FK_GvaViewAircrafts_NomValues2]  FOREIGN KEY ([AircraftProducerId]) REFERENCES [dbo].[NomValues] ([NomValueId])
)
GO

exec spDescTable  N'GvaViewAircrafts', N'Въздухоплавателни средства.'
exec spDescColumn N'GvaViewAircrafts', N'LotId'               , N'Идентификатор на партида на ВС.'
exec spDescColumn N'GvaViewAircrafts', N'ManSN'               , N'Сериен номер.'
exec spDescColumn N'GvaViewAircrafts', N'Model'               , N'Модел.'
exec spDescColumn N'GvaViewAircrafts', N'ModelAlt'            , N'Модел (английски).'
exec spDescColumn N'GvaViewAircrafts', N'OutputDate'          , N'Дата на произв.'
exec spDescColumn N'GvaViewAircrafts', N'ICAO'                , N'ICAO код.'
exec spDescColumn N'GvaViewAircrafts', N'AirCategoryId'       , N'AIR Category.'
exec spDescColumn N'GvaViewAircrafts', N'AircraftProducerId'  , N'Производител.'
exec spDescColumn N'GvaViewAircrafts', N'Engine'              , N'Двигател.'
exec spDescColumn N'GvaViewAircrafts', N'Propeller'           , N'Витло.'
exec spDescColumn N'GvaViewAircrafts', N'ModifOrWingColor'    , N'Модификация.'
exec spDescColumn N'GvaViewAircrafts', N'EngineAlt'           , N'Двигател (английски).'
exec spDescColumn N'GvaViewAircrafts', N'PropellerAlt'        , N'Витло (английски).'
exec spDescColumn N'GvaViewAircrafts', N'ModifOrWingColorAlt' , N'Модификация (английски).'
exec spDescColumn N'GvaViewAircrafts', N'Mark'                , N'Регистрационен знак.'
exec spDescColumn N'GvaViewAircrafts', N'CertNumber'          , N'Регистрационен номер.'
exec spDescColumn N'GvaViewAircrafts', N'ActNumber'           , N'Деловоден номер.'
GO

print 'FlyingExperiences'
GO

CREATE TABLE [dbo].[FlyingExperiences] (
    [FlyingExperienceId]          INT             NOT NULL IDENTITY(1,1),
    [PersonId]                    INT             NOT NULL,
    [StaffTypeId]                 INT             NULL,
    [OrganizationId]              INT             NULL,
    [AircraftId]                  INT             NULL,
    [RatingTypeId]                INT             NULL,
    [RatingClassId]               INT             NULL,
    [AuthorizationId]             INT             NULL,
    [LicenseTypeId]               INT             NULL,
    [LocationIndicatorId]         INT             NULL,
    [Sector]                      NVARCHAR (200)  NULL,
    [ExperienceRoleId]            INT             NULL,
    [ExperienceMeasureId]         INT             NULL,
    [Notes]                       NVARCHAR (500)  NULL,
    [PeriodMonth]                 NVARCHAR (500)  NULL,
    [PeriodYear]                  NVARCHAR (500)  NULL,
    [DayIfrHours]                 INT             NULL,
    [DayIfrMinutes]               INT             NULL,
    [DayVfrHours]                 INT             NULL,
    [DayVfrMinutes]               INT             NULL,
    [DayLandings]                 INT             NULL,
    [NightIfrHours]               INT             NULL,
    [NightIfrMinutes]             INT             NULL,
    [NightVfrHours]               INT             NULL,
    [NightVfrMinutes]             INT             NULL,
    [NightLandings]               INT             NULL,
    [TotalAmountHours]            INT             NULL,
    [TotalAmountMinutes]          INT             NULL,
    [TotalDocumentAmountHours]    INT             NULL,
    [TotalDocumentAmountMinutes]  INT             NULL,
    [Last12MonthsAmountHours]     INT             NULL,
    [Last12MonthsAmountMinutes]   INT             NULL,
    [Version]                     ROWVERSION      NOT NULL,
    CONSTRAINT [PK_FlyingExperience]                    PRIMARY KEY ([FlyingExperienceId]),
    CONSTRAINT [FK_FlyingExperience_Persons]            FOREIGN KEY ([PersonId])            REFERENCES [dbo].[Persons]            ([PersonId]),
    CONSTRAINT [FK_FlyingExperience_StaffTypes]         FOREIGN KEY ([StaffTypeId])         REFERENCES [dbo].[StaffTypes]         ([StaffTypeId]),
    CONSTRAINT [FK_FlyingExperience_Organizations]      FOREIGN KEY ([OrganizationId])      REFERENCES [dbo].[Organizations]      ([OrganizationId]),
    CONSTRAINT [FK_FlyingExperience_Aircrafts]          FOREIGN KEY ([AircraftId])          REFERENCES [dbo].[Aircrafts]          ([AircraftId]),
    CONSTRAINT [FK_FlyingExperience_RatingClasses]      FOREIGN KEY ([RatingClassId])       REFERENCES [dbo].[RatingClasses]      ([RatingClassId]),
    CONSTRAINT [FK_FlyingExperience_RatingTypes]        FOREIGN KEY ([RatingTypeId])        REFERENCES [dbo].[RatingTypes]        ([RatingTypeId]),
    CONSTRAINT [FK_FlyingExperience_Authorizations]     FOREIGN KEY ([AuthorizationId])     REFERENCES [dbo].[Authorizations]     ([AuthorizationId]),
    CONSTRAINT [FK_FlyingExperience_LicenseTypes]       FOREIGN KEY ([LicenseTypeId])       REFERENCES [dbo].[LicenseTypes]       ([LicenseTypeId]),
    CONSTRAINT [FK_FlyingExperience_LocationIndicators] FOREIGN KEY ([LocationIndicatorId]) REFERENCES [dbo].[LocationIndicators] ([LocationIndicatorId]),
    CONSTRAINT [FK_FlyingExperience_ExperienceRoles]    FOREIGN KEY ([ExperienceRoleId])    REFERENCES [dbo].[ExperienceRoles]    ([ExperienceRoleId]),
    CONSTRAINT [FK_FlyingExperience_ExperienceMeasures] FOREIGN KEY ([ExperienceMeasureId]) REFERENCES [dbo].[ExperienceMeasures] ([ExperienceMeasureId])
);
GO

exec spDescTable  N'FlyingExperiences'                                , N'Летателен опит.'
exec spDescColumn N'FlyingExperiences', N'FlyingExperienceId'         , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'FlyingExperiences', N'PersonId'                   , N'Физическо лице.'
exec spDescColumn N'FlyingExperiences', N'StaffTypeId'                , N'Тип персонал (направление на квалификацията).'
exec spDescColumn N'FlyingExperiences', N'OrganizationId'             , N'Организация.'
exec spDescColumn N'FlyingExperiences', N'AircraftId'                 , N'ВС.'
exec spDescColumn N'FlyingExperiences', N'RatingTypeId'               , N'Тип ВС.'
exec spDescColumn N'FlyingExperiences', N'RatingClassId'              , N'Клас ВС.'
exec spDescColumn N'FlyingExperiences', N'AuthorizationId'            , N'Разрешение.'
exec spDescColumn N'FlyingExperiences', N'LicenseTypeId'              , N'Вид правоспособност.'
exec spDescColumn N'FlyingExperiences', N'LocationIndicatorId'        , N'Местоположение.'
exec spDescColumn N'FlyingExperiences', N'Sector'                     , N'Сектор/работно място.'
exec spDescColumn N'FlyingExperiences', N'ExperienceRoleId'           , N'Роля в натрупан летателен опит.'
exec spDescColumn N'FlyingExperiences', N'ExperienceMeasureId'        , N'Вид летателен опит.'
exec spDescColumn N'FlyingExperiences', N'Notes'                      , N'Забележки.'
exec spDescColumn N'FlyingExperiences', N'PeriodMonth'                , N'За месец.'
exec spDescColumn N'FlyingExperiences', N'PeriodYear'                 , N'За година.'
exec spDescColumn N'FlyingExperiences', N'DayIfrHours'                , N'Нальот, дневен, IFR, часове.'
exec spDescColumn N'FlyingExperiences', N'DayIfrMinutes'              , N'Нальот, дневен, IFR, минути.'
exec spDescColumn N'FlyingExperiences', N'DayVfrHours'                , N'Нальот, дневен, VFR, часове.'
exec spDescColumn N'FlyingExperiences', N'DayVfrMinutes'              , N'Нальот, дневен, VFR, минути.'
exec spDescColumn N'FlyingExperiences', N'DayLandings'                , N'Кацания, ден.'
exec spDescColumn N'FlyingExperiences', N'NightIfrHours'              , N'Нальот, нощен, IFR, часове.'
exec spDescColumn N'FlyingExperiences', N'NightIfrMinutes'            , N'Нальот, нощен, IFR, минути.'
exec spDescColumn N'FlyingExperiences', N'NightVfrHours'              , N'Нальот, нощен, VFR, часове.'
exec spDescColumn N'FlyingExperiences', N'NightVfrMinutes'            , N'Нальот, нощен, VFR, минути.'
exec spDescColumn N'FlyingExperiences', N'NightLandings'              , N'Кацания, нощ.'
exec spDescColumn N'FlyingExperiences', N'TotalAmountHours'           , N'Общо количество (с натрупване), часове.'
exec spDescColumn N'FlyingExperiences', N'TotalAmountMinutes'         , N'Общо количество (с натрупване), минути.'
exec spDescColumn N'FlyingExperiences', N'TotalDocumentAmountHours'   , N'Общо количество (по документа), часове.'
exec spDescColumn N'FlyingExperiences', N'TotalDocumentAmountMinutes' , N'Общо количество (по документа), минути.'
exec spDescColumn N'FlyingExperiences', N'Last12MonthsAmountHours'    , N'Общ нальот за посл. 12 месеца, часове.'
exec spDescColumn N'FlyingExperiences', N'Last12MonthsAmountMinutes'  , N'Общ нальот за посл. 12 месеца, минути.'
GO

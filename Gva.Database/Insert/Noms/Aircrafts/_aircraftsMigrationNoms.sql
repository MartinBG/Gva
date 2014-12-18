GO
INSERT INTO [Noms]
    ([Name]                                  , [Alias]                        , [Category])
VALUES
    (N'Държатели на ТС за ВС'                , N'aircraftTCHolders'           ,N'aircraft'), --TODO NOT USED ANYWHERE?
    (N'Типове ВС'                            , N'aircraftTypes'               ,N'aircraft'),
    (N'Категория за АМЛ (Part-66 Category)'  , N'aircraftGroup66'             ,N'rating'),
    (N'Класове ограничения по част 147'      , N'lim147classes'               ,N'org147'  ),
    (N'Категория оганичения по част 147'     , N'lim147ratings'               ,N'org147'  ),
    (N'Категории ВС'                         , N'aircraftCategories'          ,N'aircraft'),
    (N'Производители на ВС'                  , N'aircraftProducers'           ,N'aircraft'),
    (N'Типове ВС - за генериране на S-Code'  , N'aircraftSCodeTypes'          ,N'aircraft'),
    (N'Типове връзки с ВС'                   , N'aircraftRelations'           ,N'aircraft'),
    (N'Състояния на оборудване на ВС'        , N'aircraftPartStatuses'        ,N'aircraft'),
    (N'Типове тежести върху ВС'              , N'aircraftDebtTypes'           ,N'aircraft'),
    (N'Класове инциденти с ВС'               , N'aircraftOccurrenceClasses'   ,N'aircraft'),
    (N'Типове удостоверения за ВС'           , N'aircraftCertificateTypes'    ,N'aircraft'),
    (N'Типове опериране на ВС'               , N'aircraftOperTypes'           ,N'aircraft'),
    (N'Типове типов сертификат за ВС'        , N'aircraftTypeCertificateTypes',N'aircraft'),
    (N'Причини за отписване на ВС'           , N'aircraftRemovalReasons'      ,N'aircraft'),
    (N'Типове радиооборудване на ВС'         , N'aircraftRadiotypes'          ,N'aircraft'),
    (N'Производители на ВС(Fm)'              , N'aircraftProducersFm'         ,N'aircraft'),
    (N'Ограничения при регистрация на ВС(Fm)', N'aircraftLimitationsFm'       ,N'aircraft'),
    (N'Състяние на регистрация на ВС(Fm)'    , N'aircraftRegStatsesFm'        ,N'aircraft'),
    (N'Държави(Fm)'                          , N'countriesFm'                 ,N'aircraft'),
    (N'Тип летателна годност на ВС(Fm)'      , N'CofATypesFm'                 ,N'aircraft'),
    (N'Тип EASA на ВС(Fm)'                   , N'EASATypesFm'                 ,N'aircraft'),
    (N'Категория EASA на ВС(Fm)'             , N'EASACategoriesFm'            ,N'aircraft'),
    (N'Тип EU регистър на ВС(Fm)'            , N'EURegTypesFm'                ,N'aircraft'),
    (N'Типове тежести върху ВС(Fm)'          , N'aircraftDebtTypesFm'         ,N'aircraft'),
    (N'Кредиторите на ВС(Fm)'                , N'aircraftCreditorsFm'         ,N'aircraft'),
    (N'Категории ВС(Fm)'                     , N'aircraftCatAWsFm'            ,N'aircraft')
GO

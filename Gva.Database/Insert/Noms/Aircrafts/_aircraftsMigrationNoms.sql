GO
INSERT INTO [Noms]
    ([Name]                                  , [Alias]                        )
VALUES
    (N'Държатели на ТС за ВС'                , N'aircraftTCHolders'           ), --TODO NOT USED ANYWHERE?
    (N'Типове ВС'                            , N'aircraftTypes'               ),
    (N'Категория за АМЛ (Part-66 Category)'  , N'aircraftGroup66'             ), --TODO NOT USED ANYWHERE?
    (N'Класове ограничения по част 147'      , N'lim147classes'               ), --TODO NOT USED ANYWHERE?
    (N'Категория оганичения по част 147'     , N'lim147ratings'               ), --TODO NOT USED ANYWHERE?
    (N'Категории ВС'                         , N'aircraftCategories'          ),
    (N'Производители на ВС'                  , N'aircraftProducers'           ),
    (N'Типове ВС - за генериране на S-Code'  , N'aircraftSCodeTypes'          ),
    (N'Типове връзки с ВС'                   , N'aircraftRelations'           ),
    (N'Типове компоненти на ВС'              , N'aircraftParts'               ),
    (N'Състояния на оборудване на ВС'        , N'aircraftPartStatuses'        ),
    (N'Типове тежести върху ВС'              , N'aircraftDebtTypes'           ),
    (N'Класове инциденти с ВС'               , N'aircraftOccurrenceClasses'   ),
    (N'Типове удостоверения за ВС'           , N'aircraftCertificateTypes'    ),
    (N'Типове опериране на ВС'               , N'aircraftOperTypes'           ), --TODO NOT USED ANYWHERE?
    (N'Типове типов сертификат за ВС'        , N'aircraftTypeCertificateTypes'),
    (N'Причини за отписване на ВС'           , N'aircraftRemovalReasons'      ),
    (N'Типове радиооборудване на ВС'         , N'aircraftRadiotypes'          ),
    (N'Производители на ВС(Fm)'              , N'aircraftProducersFm'         ),
    (N'Ограничения при регистрация на ВС(Fm)', N'aircraftLimitationsFm'       ),
    (N'Състяние на регистрация на ВС(Fm)'    , N'aircraftRegStatsesFm'        ),
    (N'Държави(Fm)'                          , N'countriesFm'                 ),
    (N'Тип летателна годност на ВС(Fm)'      , N'CofATypesFm'                 ),
    (N'Тип EASA на ВС(Fm)'                   , N'EASATypesFm'                 ),
    (N'Категория EASA на ВС(Fm)'             , N'EASACategoriesFm'            ),
    (N'Тип EU регистър на ВС(Fm)'            , N'EURegTypesFm'                ),
    (N'Типове тежести върху ВС(Fm)'          , N'aircraftDebtTypesFm'         ),
    (N'Кредиторите на ВС(Fm)'                , N'aircraftCreditorsFm'         ),
    (N'Категории ВС(Fm)'                     , N'aircraftCategoriesFm'        )
GO

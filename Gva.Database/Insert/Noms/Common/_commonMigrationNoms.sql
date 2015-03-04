GO
INSERT INTO [Noms]
    ([Name]                                                          , [Alias]                   , [Category])
VALUES
    (N'Резултати от одит'                                            , N'auditResults'           , 'system'    ),
    (N'Състояния на одит'                                            , N'auditStatuses'          , 'system'    ),
    (N'Видове одит'                                                  , N'auditTypes'             , 'system'    ),
    (N'Причини за одит'                                              , N'auditReasons'           , 'system'    ),
    (N'Изисквания към раздел (част) в част 3 на доклад в подкрепа на', N'auditPartSectionDetails', 'orgReport' ),
    (N'Раздел (част) в част 3 на доклад в подкрепа на'               , N'auditPartSections'      , 'orgReport' ),
    (N'Изисквания към раздел'                                        , N'auditPartRequirements'  , 'system'    ),
    (N'Раздел'                                                       , N'auditParts'             , 'system'    ),
    (N'Ограничения по част М/Ф и част 145'                           , N'lim145limitations'      , 'org145mf'  ),
    (N'Класове ограничения по част М/Ф и част 145'                   , N'lim145classes'          , 'org145mf'  ), --TODO NOT USED ANYWHERE??
    (N'Парични единици'                                              , N'currencies'             , 'system'    ),
    (N'Видове плащания по заявления'                                 , N'applicationpaymentTypes', 'system'    ),
    (N'Видове заявления'                                             , N'applicationTypes'       , 'system'    ),
    (N'Въздухоплавателни администрации'                              , N'caa'                    , 'aircraft'  ),
    (N'Групи ВС'                                                     , N'aircraftTypeGroups'     , 'aircraft'  ),
    (N'Типове документи'                                             , N'documentTypes'          , 'system'    ),
    (N'Роли документи'                                               , N'documentRoles'          , 'system'    ),
    (N'Типове адреси'                                                , N'addressTypes'           , 'system'    ),
    (N'Държави'                                                      , N'countries'              , 'system'    ),
    (N'Населени места'                                               , N'cities'                 , 'system'    )
GO

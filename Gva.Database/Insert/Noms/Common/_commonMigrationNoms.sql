GO
INSERT INTO [Noms]
    ([Name]                                                          , [Alias]                   , [Category])
VALUES
    (N'Резултати от одит'                                            , N'auditResults'           , 'inspection'),
    (N'Състояния на одит'                                            , N'auditStatuses'          , 'inspection'),
    (N'Видове одит'                                                  , N'auditTypes'             , 'inspection'),
    (N'Причини за одит'                                              , N'auditReasons'           , 'inspection'),
    (N'Изисквания към раздел (част) в част 3 на доклад в подкрепа на', N'auditPartSectionDetails', 'orgReport'),
    (N'Раздел (част) в част 3 на доклад в подкрепа на'               , N'auditPartSections'      , 'orgReport'),
    (N'Изисквания към раздел'                                        , N'auditPartRequirements'  , 'inspection'),
    (N'Раздел'                                                       , N'auditParts'             , 'inspection'),
    (N'Ограничения по част М/Ф и част 145'                           , N'lim145limitations'      , 'org145mf'),
    (N'Класове ограничения по част М/Ф и част 145'                   , N'lim145classes'          , 'org145mf'), --TODO NOT USED ANYWHERE??
    (N'Парични единици'                                              , N'currencies'             , 'system'),
    (N'Видове плащания по заявления'                                 , N'applicationpaymentTypes', 'application'),
    (N'Видове заявления'                                             , N'applicationTypes'       , 'application'),
    (N'Въздухоплавателни администрации'                              , N'caa'                    , 'aircraft'),
    (N'Групи ВС'                                                     , N'aircraftTypeGroups'     , 'aircraft'),
    (N'Типове документи'                                             , N'documentTypes'          , 'document'),
    (N'Роли документи'                                               , N'documentRoles'          , 'document'),
    (N'Типове адреси'                                                , N'addressTypes'           , 'system'),
    (N'Държави'                                                      , N'countries'              , 'system'),
    (N'Населени места'                                               , N'cities'                 , 'system')
GO

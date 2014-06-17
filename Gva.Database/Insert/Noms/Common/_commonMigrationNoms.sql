GO
INSERT INTO [Noms]
    ([Name]                                                          , [Alias]                   )
VALUES
    (N'Резултати от одит'                                            , N'auditResults'           ),
    (N'Състояния на одит'                                            , N'auditStatuses'          ),
    (N'Видове одит'                                                  , N'auditTypes'             ),
    (N'Причини за одит'                                              , N'auditReasons'           ),
    (N'Изисквания към раздел (част) в част 3 на доклад в подкрепа на', N'auditPartSectionDetails'),
    (N'Раздел (част) в част 3 на доклад в подкрепа на'               , N'auditPartSections'      ),
    (N'Изисквания към раздел'                                        , N'auditPartRequirements'  ),
    (N'Раздел'                                                       , N'auditParts'             ),
    (N'Ограничения по част М/Ф и част 145'                           , N'lim145limitations'      ),
    (N'Класове ограничения по част М/Ф и част 145'                   , N'lim145classes'          ), --TODO NOT USED ANYWHERE??
    (N'Парични единици'                                              , N'currencies'             ),
    (N'Видове плащания по заявления'                                 , N'applicationpaymentTypes'),
    (N'Видове заявления'                                             , N'applicationTypes'       ),
    (N'Въздухоплавателни администрации'                              , N'caa'                    ),
    (N'Групи ВС'                                                     , N'aircraftTypeGroups'     ),
    (N'Типове документи'                                             , N'documentTypes'          ),
    (N'Роли документи'                                               , N'documentRoles'          ),
    (N'Типове адреси'                                                , N'addressTypes'           ),
    (N'Държави'                                                      , N'countries'              ),
    (N'Населени места'                                               , N'cities'                 )
GO

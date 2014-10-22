GO
INSERT INTO [Noms]
    ([Name]                                              , [Alias]                   )
VALUES
    (N'Типове персонал за обучение'                      , N'trainingStaffTypes'     ),
    (N'Нива на владеене на език'                         , N'langLevels'             ),
    (N'Модел на квалификация на Физическо лице'          , N'personRatingModels'     ),
    (N'Оценки при проверка на Физическо лице'            , N'personCheckRatingValues'),
    (N'Видове действия относно правоспособност'          , N'licenceActions'         ),
    (N'Степени на квалификационен клас на Физическо лице', N'personRatingLevels'     ),
    (N'Видове летателен опит'                            , N'experienceMeasures'     ),
    (N'Роли в натрупан летателният опит'                 , N'experienceRoles'        ),
    (N'Ограничения за медицинска годност'                , N'medLimitation'          ),
    (N'Класове за медицинска годност'                    , N'medClasses'             ),
    (N'Ограничения (Part-66)'                            , N'limitations66'          ),
    (N'Клас (Part-66 Category)'                          , N'aircraftClases66'       ),
    (N'Индикатори на местоположение'                     , N'locationIndicators'     ),
    (N'Видове(типове) правоспособност'                   , N'licenceTypes'           ),
    (N'Легенда към видове(типове) правоспособност'       , N'licenceTypeDictionary'  ), --TODO NOT USED ANYWHERE?
    (N'Разрешения към квалификация'                      , N'authorizations'         ),
    (N'Групи Разрешения към квалификация'                , N'authorizationGroups'    ), --TODO NOT USED ANYWHERE?
    (N'Типове състояния на Физичеко лице'                , N'personStatusTypes'      ),
    (N'Издатели на документи - Други'                    , N'otherDocPublishers'     ),
    (N'Издатели на документи - Медицински'               , N'medDocPublishers'       ),
    (N'Типове ВС за екипажи'                             , N'ratingTypes'            ),
    (N'Групи Класове ВС за екипажи'                      , N'ratingClassGroups'      ), --TODO NOT USED ANYWHERE?
    (N'Класове ВС за екипажи'                            , N'ratingClasses'          ),
    (N'Подкласове ВС за екипажи'                         , N'ratingSubClasses'       ),
    (N'Направления'                                      , N'directions'             ), --TODO NOT USED ANYWHERE?
    (N'Учебни заведения'                                 , N'schools'                ),
    (N'Степени на образование'                           , N'graduations'            ),
    (N'Категории персонал'                               , N'employmentCategories'   ),
    (N'Типове персонал'                                  , N'staffTypes'             ),
    (N'Полове'                                           , N'gender'                 ),
    (N'Причини за промяна на статус на правоспособност'  , N'licenceChangeReasons'   )
GO

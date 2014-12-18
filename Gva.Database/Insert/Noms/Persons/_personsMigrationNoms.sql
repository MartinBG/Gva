GO
INSERT INTO [Noms]
    ([Name]                                              , [Alias]                   , [Category])
VALUES
    (N'Типове персонал за обучение'                      , N'trainingStaffTypes'     , N'person'),
    (N'Нива на владеене на език'                         , N'langLevels'             , N'person'),
    (N'Модел на квалификация на Физическо лице'          , N'personRatingModels'     , N'person'),
    (N'Оценки при проверка на Физическо лице'            , N'personCheckRatingValues', N'person'),
    (N'Видове действия относно правоспособност'          , N'licenceActions'         , N'licence'),
    (N'Степени на квалификационен клас на Физическо лице', N'personRatingLevels'     , N'person'),
    (N'Видове летателен опит'                            , N'experienceMeasures'     , N'person'),
    (N'Роли в натрупан летателният опит'                 , N'experienceRoles'        , N'person'),
    (N'Ограничения за медицинска годност'                , N'medLimitation'          , N'person'),
    (N'Класове за медицинска годност'                    , N'medClasses'             , N'person'),
    (N'Ограничения (Part-66)'                            , N'limitations66'          , N'licence'),
    (N'Клас (Part-66 Category)'                          , N'aircraftClases66'       , N'rating'),
    (N'Индикатори на местоположение'                     , N'locationIndicators'     , N'system'),
    (N'Видове(типове) правоспособност'                   , N'licenceTypes'           , N'licence'),
    (N'Легенда към видове(типове) правоспособност'       , N'licenceTypeDictionary'  , N'licence'), --TODO NOT USED ANYWHERE?
    (N'Разрешения към квалификация'                      , N'authorizations'         , N'rating'),
    (N'Групи Разрешения към квалификация'                , N'authorizationGroups'    , N'rating'), --TODO NOT USED ANYWHERE?
    (N'Типове състояния на Физичеко лице'                , N'personStatusTypes'      , N'person'),
    (N'Издатели на документи - Други'                    , N'otherDocPublishers'     , N'system'),
    (N'Издатели на документи - Медицински'               , N'medDocPublishers'       , N'system'),
    (N'Типове ВС за екипажи'                             , N'ratingTypes'            , N'rating'),
    (N'Групи Класове ВС за екипажи'                      , N'ratingClassGroups'      , N'rating'), --TODO NOT USED ANYWHERE?
    (N'Класове ВС за екипажи'                            , N'ratingClasses'          , N'rating'),
    (N'Подкласове ВС за екипажи'                         , N'ratingSubClasses'       , N'rating'),
    (N'Направления'                                      , N'directions'             , N'system'), --TODO NOT USED ANYWHERE?
    (N'Учебни заведения'                                 , N'schools'                , N'person'),
    (N'Степени на образование'                           , N'graduations'            , N'person'),
    (N'Категории персонал'                               , N'employmentCategories'   , N'person'),
    (N'Типове персонал'                                  , N'staffTypes'             , N'person'),
    (N'Полове'                                           , N'gender'                 , N'system'),
    (N'Причини за промяна на статус на правоспособност'  , N'licenceChangeReasons'   , N'person')
GO

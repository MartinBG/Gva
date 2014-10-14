GO
INSERT INTO [LotSets] ([Name], [Alias]) VALUES (N'Персонал', N'Person')

DECLARE @setId INT = @@IDENTITY

INSERT INTO [LotSetParts]
    ([LotSetId], [Name]                        , [Alias]                 , [PathRegex]                            , [LotSchemaId])
VALUES                                                                                                            
    (@setId    , 'Адрес'                       , 'personAddress'         , N'^personAddresses/\d+$'               , NULL        ),
    (@setId    , 'Лични данни'                 , 'personData'            , N'^personData$'                        , NULL        ),
    (@setId    , 'Документ за самоличност'     , 'personDocumentId'      , N'^personDocumentIds/\d+$'             , NULL        ),
    (@setId    , 'Проверка'                    , 'personCheck'           , N'^personDocumentChecks/\d+$'          , NULL        ),
    (@setId    , 'Образование'                 , 'personEducation'       , N'^personDocumentEducations/\d+$'      , NULL        ),
    (@setId    , 'Месторабота'                 , 'personEmployment'      , N'^personDocumentEmployments/\d+$'     , NULL        ),
    (@setId    , 'Медицинско свидетелство'     , 'personMedical'         , N'^personDocumentMedicals/\d+$'        , NULL        ),
    (@setId    , ''                            , 'personExams'           , N'^personDocumentExams/\d+$'           , NULL        ),
    (@setId    , 'Обучение'                    , 'personTraining'        , N'^personDocumentTrainings/\d+$'       , NULL        ),
    (@setId    , 'Свидетелство за език'        , 'personLangCert'        , N'^personDocumentLangCertificates/\d+$', NULL        ),
    (@setId    , 'Летателен / практически опит', 'personFlyingExperience', N'^personFlyingExperiences/\d+$'       , NULL        ),
    (@setId    , 'Състояния'                   , 'personStatus'          , N'^personStatuses/\d+$'                , NULL        ),
    (@setId    , 'Лиценз'                      , 'personLicence'         , N'^licences/\d+$'                      , NULL        ),
    (@setId    , 'Вписване към лиценз'         , 'personLicenceEdition'  , N'^licenceEditions/\d+$'               , NULL        ),
    (@setId    , 'Класове'                     , 'personRating'          , N'^ratings/\d+$'                       , NULL        ),
    (@setId    , 'Вписване към клас'           , 'personRatingEdition'   , N'^ratingEditions/\d+$'                , NULL        ),
    (@setId    , 'Друг документ'               , 'personOther'           , N'^personDocumentOthers/\d+$'          , NULL        ),
    (@setId    , 'Заявление'                   , 'personApplication'     , N'^personDocumentApplications/\d+$'    , NULL        ),
    (@setId    , ''                            , 'exams'                 , N'^personExams/\d+$'                   , NULL        ),
    (@setId    , 'Данни за инспектор'          , 'inspectorData'         , N'^inspectorData$'                     , NULL        )
GO


INSERT INTO [LotSets]
    ([LotSetId], [Name]      , [Alias]   )
VALUES
    (1         , N'Персонал' , N'Person' )
GO


INSERT INTO [LotSetParts]
    ([LotSetPartId], [LotSetId], [Name]                       , [Alias]           , [PathRegex]                            , [Schema])
VALUES
    (1             , 1         ,'Адрес'                       , 'address'         , N'^personAddresses/\d+$'               , N'{}'   ),
    (2             , 1         ,'Лични данни'                 , 'data'            , N'^personData$'                        , N'{}'   ),
    (3             , 1         ,'Документ за самоличност'     , 'documentId'      , N'^personDocumentIds/\d+$'             , N'{}'   ),
    (4             , 1         ,'Проверка'                    , 'check'           , N'^personDocumentChecks/\d+$'          , N'{}'   ),
    (5             , 1         ,'Образования'                 , 'education'       , N'^personDocumentEducations/\d+$'      , N'{}'   ),
    (6             , 1         ,'Месторабота'                 , 'employment'      , N'^personDocumentEmployments/\d+$'     , N'{}'   ),
    (7             , 1         ,'Медицински'                  , 'medical'         , N'^personDocumentMedicals/\d+$'        , N'{}'   ),
    (8             , 1         ,''                            , 'theoreticalexams', N'^personDocumentTheoreticalexams/\d+$', N'{}'   ),
    (9             , 1         ,'Обучение'                    , 'training'        , N'^personDocumentTrainings/\d+$'       , N'{}'   ),
    (10            , 1         ,'Летателен / практически опит', 'flyingExperience', N'^personFlyingExperiences/\d+$'       , N'{}'   ),
    (11            , 1         ,'Състояния'                   , 'personStatus'    , N'^personStatuses/\d+$'                , N'{}'   ),
    (12            , 1         ,''                            , 'licence'         , N'^licences/\d+$'                      , N'{}'   ),
    (13            , 1         ,''                            , 'licenceEdition'  , N'^licences/\d+/editions/\d+$'         , N'{}'   ),
    (14            , 1         ,'Класове'                     , 'rating'          , N'^ratings/\d+$'                       , N'{}'   ),
    (15            , 1         ,''                            , 'ratingEdition'   , N'^ratings/\d+/editions/\d+$'          , N'{}'   ),
    (16            , 1         ,'Друг документ'               , 'other'           , N'^personDocumentOthers/\d+$'          , N'{}'   ),
    (17            , 1         ,'Заявление'                   , 'application'     , N'^personDocumentApplications/\d+$'    , N'{}'   )
GO

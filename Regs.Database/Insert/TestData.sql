INSERT INTO [LotSets]
    ([LotSetId], [Name]         , [Alias]         )
VALUES
    (1         , N'Персонал'    , N'Person'       ),
    (2         , N'Организация' , N'Organization' )
GO


INSERT INTO [LotSetParts]
    ([LotSetPartId], [LotSetId], [Name]                                                                                  , [Alias]                                           , [PathRegex]                                              , [Schema])
VALUES                                                                                                                                                                                                                                  
    (1             , 1         ,'Адрес'                                                                                  , 'address'                                         , N'^personAddresses/\d+$'                                 , N'{}'   ),
    (2             , 1         ,'Лични данни'                                                                            , 'data'                                            , N'^personData$'                                          , N'{}'   ),
    (3             , 1         ,'Документ за самоличност'                                                                , 'documentId'                                      , N'^personDocumentIds/\d+$'                               , N'{}'   ),
    (4             , 1         ,'Проверка'                                                                               , 'check'                                           , N'^personDocumentChecks/\d+$'                            , N'{}'   ),
    (5             , 1         ,'Образование'                                                                            , 'education'                                       , N'^personDocumentEducations/\d+$'                        , N'{}'   ),
    (6             , 1         ,'Месторабота'                                                                            , 'employment'                                      , N'^personDocumentEmployments/\d+$'                       , N'{}'   ),
    (7             , 1         ,'Медицинско свидетелство'                                                                , 'medical'                                         , N'^personDocumentMedicals/\d+$'                          , N'{}'   ),
    (8             , 1         ,''                                                                                       , 'theoreticalexams'                                , N'^personDocumentTheoreticalexams/\d+$'                  , N'{}'   ),
    (9             , 1         ,'Обучение'                                                                               , 'training'                                        , N'^personDocumentTrainings/\d+$'                         , N'{}'   ),
    (10            , 1         ,'Летателен / практически опит'                                                           , 'flyingExperience'                                , N'^personFlyingExperiences/\d+$'                         , N'{}'   ),
    (11            , 1         ,'Състояния'                                                                              , 'personStatus'                                    , N'^personStatuses/\d+$'                                  , N'{}'   ),
    (12            , 1         ,''                                                                                       , 'licence'                                         , N'^licences/\d+$'                                        , N'{}'   ),
    (13            , 1         ,''                                                                                       , 'licenceEdition'                                  , N'^licences/\d+/editions/\d+$'                           , N'{}'   ),
    (14            , 1         ,'Класове'                                                                                , 'rating'                                          , N'^ratings/\d+$'                                         , N'{}'   ),
    (15            , 1         ,''                                                                                       , 'ratingEdition'                                   , N'^ratings/\d+/editions/\d+$'                            , N'{}'   ),
    (16            , 1         ,'Друг документ'                                                                          , 'other'                                           , N'^personDocumentOthers/\d+$'                            , N'{}'   ),
    (17            , 1         ,'Заявление'                                                                              , 'application'                                     , N'^personDocumentApplications/\d+$'                      , N'{}'   ),
    (18            , 2         ,'Адрес'                                                                                  , 'address'                                         , N'^organizationAddresses/\d+$'                           , N'{}'   ),
    (19            , 2         ,'План за одит'                                                                           , 'auditplan'                                       , N'^organizationAuditplans/\d+$'                          , N'{}'   ),
    (20            , 2         ,'Лиценз на летищен оператор'                                                             , 'certAirportOperator'                             , N'^organizationCertAirportOperators/\d+$'                , N'{}'   ),
    (21            , 2         ,'Лиценз на оператор по наземно обслужване или самообслужване'                            , 'certGroundServiceOperator'                       , N'^organizationCertGroundServiceOperators/\d+$'          , N'{}'   ),
    (22            , 2         ,'Удостоверение за експлоатационна годност на системи и съоръжения за наземно обслужване' , 'organizationGroundServiceOperatorSnoOperational' , N'^organizationGroundServiceOperatorsSnoOperational/\d+$', N'{}'   ),
    (23            , 2         ,'Одит'                                                                                   , 'inspection'                                      , N'^organizationInspections/\d+$'                         , N'{}'   ),
    (24            , 2         ,'Удостоверение за одобрение'                                                             , 'approval'                                        , N'^organizationApprovals/\d+$'                           , N'{}'   ),
    (25            , 2         ,'Изменение на достоверение за одобрение'                                                 , 'amendment'                                       , N'^organizationApprovals/\d+/amendments/\d+$'            , N'{}'   ),
    (26            , 2         ,'Проверяващи'                                                                            , 'staffExaminer'                                   , N'^organizationStaffExaminers/\d+$'                      , N'{}'   ),
    (27            , 2         ,'Регистър за издадени лицензи за летищен оператор'                                       , 'regAirportOperator'                              , N'^organizationRegAirportOperators/\d+$'                 , N'{}'   ),
    (28            , 2         ,'Регистър за издадени лицензи за оператор по наземно обслужване или самообслужване'      , 'regGroundServiceOperator'                        , N'^organizationRegGroundServiceOperators/\d+$'           , N'{}'   ),
    (29            , 2         ,'Доклад от препоръки'                                                                    , 'recommendation'                                  , N'^organizationRecommendations/\d+$'                     , N'{}'   ),
    (30            , 2         ,'Друг документ'                                                                          , 'other'                                           , N'^organizationDocumentOthers/\d+$'                      , N'{}'   ),
    (31            , 2         ,'Ръководен персонал'                                                                     , 'staffManagement'                                 , N'^organizationStaffManagement/\d+$'                     , N'{}'   ),
    (32            , 2         ,'Данни за организация'                                                                   , 'data'                                            , N'^organizationData$'                                    , N'{}'   ),
    (33            , 2         ,'Заявление'                                                                              , 'application'                                     , N'^organizationDocumentApplications/\d+$'                , N'{}'   )
GO

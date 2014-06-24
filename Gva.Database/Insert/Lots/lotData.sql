﻿INSERT INTO [LotSets]
    ([LotSetId], [Name]         , [Alias]        )
VALUES
    (1         , N'Персонал'    , N'Person'      ),
    (2         , N'Организация' , N'Organization'),
    (3         , N'ВС'          , N'Aircraft'    ),
    (4         , N'Летище'      , N'Airport'     ),
    (5         , N'Съоръжение'  , N'Equipment'   )
GO


INSERT INTO [LotSetParts]
    ([LotSetPartId], [LotSetId], [Name]                                                                                  , [Alias]                                                       , [PathRegex]                                              , [Schema])
VALUES                                                                                                                                                                                                                                                        
    (1             , 1         ,'Адрес'                                                                                  , 'personAddress'                                               , N'^personAddresses/\d+$'                                 , N'{}'   ),
    (2             , 1         ,'Лични данни'                                                                            , 'personData'                                                  , N'^personData$'                                          , N'{}'   ),
    (3             , 1         ,'Документ за самоличност'                                                                , 'personDocumentId'                                            , N'^personDocumentIds/\d+$'                               , N'{}'   ),
    (4             , 1         ,'Проверка'                                                                               , 'personCheck'                                                 , N'^personDocumentChecks/\d+$'                            , N'{}'   ),
    (5             , 1         ,'Образование'                                                                            , 'personEducation'                                             , N'^personDocumentEducations/\d+$'                        , N'{}'   ),
    (6             , 1         ,'Месторабота'                                                                            , 'personEmployment'                                            , N'^personDocumentEmployments/\d+$'                       , N'{}'   ),
    (7             , 1         ,'Медицинско свидетелство'                                                                , 'personMedical'                                               , N'^personDocumentMedicals/\d+$'                          , N'{}'   ),
    (8             , 1         ,''                                                                                       , 'personExams'                                                 , N'^personDocumentExams/\d+$'                             , N'{}'   ),
    (9             , 1         ,'Обучение'                                                                               , 'personTraining'                                              , N'^personDocumentTrainings/\d+$'                         , N'{}'   ),
    (10            , 1         ,'Летателен / практически опит'                                                           , 'personFlyingExperience'                                      , N'^personFlyingExperiences/\d+$'                         , N'{}'   ),
    (11            , 1         ,'Състояния'                                                                              , 'personStatus'                                                , N'^personStatuses/\d+$'                                  , N'{}'   ),
    (12            , 1         ,''                                                                                       , 'personLicence'                                               , N'^licences/\d+$'                                        , N'{}'   ),
    (14            , 1         ,'Класове'                                                                                , 'personRating'                                                , N'^ratings/\d+$'                                         , N'{}'   ),
    (16            , 1         ,'Друг документ'                                                                          , 'personOther'                                                 , N'^personDocumentOthers/\d+$'                            , N'{}'   ),
    (17            , 1         ,'Заявление'                                                                              , 'personApplication'                                           , N'^personDocumentApplications/\d+$'                      , N'{}'   ),
    (18            , 1         ,''                                                                                       , 'exams'                                                       , N'^personExams/\d+$'                                     , N'{}'   ),
    (19            , 1         ,'Данни за инспектор'                                                                     , 'inspectorData'                                               , N'^inspectorData$'                                       , N'{}'   ),
    (20            , 2         ,'Адрес'                                                                                  , 'organizationAddress'                                         , N'^organizationAddresses/\d+$'                           , N'{}'   ),
    (21            , 2         ,'План за одит'                                                                           , 'organizationAuditplan'                                       , N'^organizationAuditplans/\d+$'                          , N'{}'   ),
    (22            , 2         ,'Лиценз на летищен оператор'                                                             , 'organizationCertAirportOperator'                             , N'^organizationCertAirportOperators/\d+$'                , N'{}'   ),
    (23            , 2         ,'Лиценз на оператор по наземно обслужване или самообслужване'                            , 'organizationCertGroundServiceOperator'                       , N'^organizationCertGroundServiceOperators/\d+$'          , N'{}'   ),
    (24            , 2         ,'Удостоверение за експлоатационна годност на системи и съоръжения за наземно обслужване' , 'organizationOrganizationGroundServiceOperatorSnoOperational' , N'^organizationGroundServiceOperatorsSnoOperational/\d+$', N'{}'   ),
    (25            , 2         ,'Свидетелство за авиационен оператор'                                                    , 'organizationCertAirOperator'                                 , N'^organizationCertAirOperators/\d+$'                    , N'{}'   ),
    (26            , 2         ,'Свидетелство за извършване на аеронавигационно обслужване.'                             , 'organizationCertAirNavigationServiceDeliverer'               , N'^organizationCertAirNavigationServiceDeliverers/\d+$'  , N'{}'   ),
    (27            , 2         ,'Оперативен лиценз на въздушен превозвач.'                                               , 'organizationCertAirCarrier'                                  , N'^organizationCertAirCarriers/\d+$'                     , N'{}'   ),
    (28            , 2         ,'Одит'                                                                                   , 'organizationInspection'                                      , N'^organizationInspections/\d+$'                         , N'{}'   ),
    (29            , 2         ,'Удостоверение за одобрение'                                                             , 'organizationApproval'                                        , N'^organizationApprovals/\d+$'                           , N'{}'   ),
    (30            , 2         ,'Изменение на достоверение за одобрение'                                                 , 'organizationAmendment'                                       , N'^organizationApprovals/\d+/amendments/\d+$'            , N'{}'   ),
    (31            , 2         ,'Проверяващи'                                                                            , 'organizationStaffExaminer'                                   , N'^organizationStaffExaminers/\d+$'                      , N'{}'   ),
    (32            , 2         ,'Регистър за издадени лицензи за летищен оператор'                                       , 'organizationRegAirportOperator'                              , N'^organizationRegAirportOperators/\d+$'                 , N'{}'   ),
    (33            , 2         ,'Регистър за издадени лицензи за оператор по наземно обслужване или самообслужване'      , 'organizationRegGroundServiceOperator'                        , N'^organizationRegGroundServiceOperators/\d+$'           , N'{}'   ),
    (34            , 2         ,'Доклад от препоръки'                                                                    , 'organizationRecommendation'                                  , N'^organizationRecommendations/\d+$'                     , N'{}'   ),
    (35            , 2         ,'Друг документ'                                                                          , 'organizationOther'                                           , N'^organizationDocumentOthers/\d+$'                      , N'{}'   ),
    (36            , 2         ,'Ръководен персонал'                                                                     , 'organizationStaffManagement'                                 , N'^organizationStaffManagement/\d+$'                     , N'{}'   ),
    (37            , 2         ,'Данни за организация'                                                                   , 'organizationData'                                            , N'^organizationData$'                                    , N'{}'   ),
    (38            , 2         ,'Заявление'                                                                              , 'organizationApplication'                                     , N'^organizationDocumentApplications/\d+$'                , N'{}'   ),
    (39            , 3         ,'Данни за ВС'                                                                            , 'aircraftData'                                                , N'^aircraftData$'                                        , N'{}'   ),
    (40            , 3         ,'Данни за ВС'                                                                            , 'aircraftDataApex'                                            , N'^aircraftDataApex$'                                    , N'{}'   ),
    (41            , 3         ,'Свързано лице'                                                                          , 'aircraftOwner'                                               , N'^aircraftDocumentOwners/\d+$'                          , N'{}'   ),
    (42            , 3         ,'Оборудване'                                                                             , 'aircraftPart'                                                , N'^aircraftParts/\d+$'                                   , N'{}'   ),
    (43            , 3         ,'Залог/запор'                                                                            , 'aircraftDebtFM'                                              , N'^aircraftDocumentDebtsFM/\d+$'                         , N'{}'   ),
    (44            , 3         ,'Залог/запор'                                                                            , 'aircraftDebt'                                                , N'^aircraftDocumentDebts/\d+$'                           , N'{}'   ),
    (45            , 3         ,'Подръжка'                                                                               , 'aircraftMaintenance'                                         , N'^maintenances/\d+$'                                    , N'{}'   ),
    (46            , 3         ,'Инцидент'                                                                               , 'aircraftOccurrence'                                          , N'^documentOccurrences/\d+$'                             , N'{}'   ),
    (47            , 3         ,'Инспекция'                                                                              , 'aircraftInspection'                                          , N'^inspections/\d+$'                                     , N'{}'   ),
    (48            , 3         ,'Заявление'                                                                              , 'aircraftApplication'                                         , N'^aircraftDocumentApplications/\d+$'                    , N'{}'   ),
    (49            , 3         ,'Друг документ'                                                                          , 'aircraftOther'                                               , N'^aircraftDocumentOthers/\d+$'                          , N'{}'   ),
    (50            , 3         ,'Регистация'                                                                             , 'aircraftRegistration'                                        , N'^aircraftCertRegistrations/\d+$'                       , N'{}'   ),
    (51            , 3         ,'Регистация'                                                                             , 'aircraftRegistrationFM'                                      , N'^aircraftCertRegistrationsFM/\d+$'                     , N'{}'   ),
    (52            , 3         ,'Летателна годност'                                                                      , 'aircraftAirworthiness'                                       , N'^aircraftCertAirworthinesses/\d+$'                     , N'{}'   ),
    (53            , 3         ,'Летателна годност'                                                                      , 'aircraftAirworthinessFM'                                     , N'^aircraftCertAirworthinessesFM/\d+$'                   , N'{}'   ),
    (54            , 3         ,'Регистрационен знак'                                                                    , 'aircraftMark'                                                , N'^aircraftCertMarks/\d+$'                               , N'{}'   ),
    (55            , 3         ,'S-mode код'                                                                             , 'aircraftSmod'                                                , N'^aircraftCertSmods/\d+$'                               , N'{}'   ),
    (57            , 3         ,'Разрешително за полет'                                                                  , 'aircraftPermit'                                              , N'^aircraftCertPermitsToFly/\d+$'                        , N'{}'   ),
    (58            , 3         ,'Удосотоверение за шум'                                                                  , 'aircraftNoise'                                               , N'^aircraftCertNoises/\d+$'                              , N'{}'   ),
    (59            , 3         ,'Разрешително за радиостанция'                                                           , 'aircraftRadio'                                               , N'^aircraftCertRadios/\d+$'                              , N'{}'   ),
    (60            , 4         ,'Данни за летище'                                                                        , 'airportData'                                                 , N'^airportData$'                                         , N'{}'   ),
    (61            , 4         ,'Свързано лице'                                                                          , 'airportOwner'                                                , N'^airportDocumentOwners/\d+$'                           , N'{}'   ),
    (62            , 4         ,'Друг документ'                                                                          , 'airportOther'                                                , N'^airportDocumentOthers/\d+$'                           , N'{}'   ),
    (63            , 4         ,'Заявление'                                                                              , 'airportApplication'                                          , N'^airportDocumentApplications/\d+$'                     , N'{}'   ),
    (64            , 4         ,'Eксплоатационна годност'                                                                , 'airportOperational'                                          , N'^airportCertOperationals/\d+$'                         , N'{}'   ),
    (65            , 4         ,'Инспекция'                                                                              , 'airportInspection'                                           , N'^inspections/\d+$'                                     , N'{}'   ),
    (66            , 5         ,'Данни за съoръжение'                                                                    , 'equipmentData'                                               , N'^equipmentData$'                                       , N'{}'   ),
    (67            , 5         ,'Свързано лице'                                                                          , 'equipmentOwner'                                              , N'^equipmentDocumentOwners/\d+$'                         , N'{}'   ),
    (68            , 5         ,'Друг документ'                                                                          , 'equipmentOther'                                              , N'^equipmentDocumentOthers/\d+$'                         , N'{}'   ),
    (69            , 5         ,'Заявление'                                                                              , 'equipmentApplication'                                        , N'^equipmentDocumentApplications/\d+$'                   , N'{}'   ),
    (70            , 5         ,'Eксплоатационна годност'                                                                , 'equipmentOperational'                                        , N'^equipmentCertOperationals/\d+$'                       , N'{}'   ),
    (71            , 5         ,'Инспекция'                                                                              , 'equipmentInspection'                                         , N'^inspections/\d+$'                                     , N'{}'   )

GO
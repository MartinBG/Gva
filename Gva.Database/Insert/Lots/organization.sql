GO
INSERT INTO [LotSets] ([Name], [Alias]) VALUES (N'Организация', N'Organization')

DECLARE @setId INT = @@IDENTITY

INSERT INTO [LotSetParts]
    ([LotSetId], [Name]                                                                                  , [Alias]                                                      , [PathRegex]                                              , [LotSchemaId])
VALUES
    (@setId    , 'Адрес'                                                                                 , 'organizationAddress'                                        , N'^organizationAddresses/\d+$'                           , NULL        ),
    (@setId    , 'План за одит'                                                                          , 'organizationAuditplan'                                      , N'^organizationAuditplans/\d+$'                          , NULL        ),
    (@setId    , 'Лиценз на летищен оператор'                                                            , 'organizationCertAirportOperator'                            , N'^organizationCertAirportOperators/\d+$'                , NULL        ),
    (@setId    , 'Лиценз на оператор по наземно обслужване или самообслужване'                           , 'organizationCertGroundServiceOperator'                      , N'^organizationCertGroundServiceOperators/\d+$'          , NULL        ),
    (@setId    , 'Удостоверение за експлоатационна годност на системи и съоръжения за наземно обслужване', 'organizationOrganizationGroundServiceOperatorSnoOperational', N'^organizationGroundServiceOperatorsSnoOperational/\d+$', NULL        ),
    (@setId    , 'Свидетелство за авиационен оператор'                                                   , 'organizationCertAirOperator'                                , N'^organizationCertAirOperators/\d+$'                    , NULL        ),
    (@setId    , 'Свидетелство за извършване на аеронавигационно обслужване.'                            , 'organizationCertAirNavigationServiceDeliverer'              , N'^organizationCertAirNavigationServiceDeliverers/\d+$'  , NULL        ),
    (@setId    , 'Оперативен лиценз на въздушен превозвач.'                                              , 'organizationCertAirCarrier'                                 , N'^organizationCertAirCarriers/\d+$'                     , NULL        ),
    (@setId    , 'Одит'                                                                                  , 'organizationInspection'                                     , N'^organizationInspections/\d+$'                         , NULL        ),
    (@setId    , 'Удостоверение за одобрение'                                                            , 'organizationApproval'                                       , N'^organizationApprovals/\d+$'                           , NULL        ),
    (@setId    , 'Изменение на достоверение за одобрение'                                                , 'organizationAmendment'                                      , N'^organizationApprovals/\d+/amendments/\d+$'            , NULL        ),
    (@setId    , 'Проверяващи'                                                                           , 'organizationStaffExaminer'                                  , N'^organizationStaffExaminers/\d+$'                      , NULL        ),
    (@setId    , 'Регистър за издадени лицензи за летищен оператор'                                      , 'organizationRegAirportOperator'                             , N'^organizationRegAirportOperators/\d+$'                 , NULL        ),
    (@setId    , 'Регистър за издадени лицензи за оператор по наземно обслужване или самообслужване'     , 'organizationRegGroundServiceOperator'                       , N'^organizationRegGroundServiceOperators/\d+$'           , NULL        ),
    (@setId    , 'Доклад от препоръки'                                                                   , 'organizationRecommendation'                                 , N'^organizationRecommendations/\d+$'                     , NULL        ),
    (@setId    , 'Друг документ'                                                                         , 'organizationOther'                                          , N'^organizationDocumentOthers/\d+$'                      , NULL        ),
    (@setId    , 'Ръководен персонал'                                                                    , 'organizationStaffManagement'                                , N'^organizationStaffManagement/\d+$'                     , NULL        ),
    (@setId    , 'Данни за организация'                                                                  , 'organizationData'                                           , N'^organizationData$'                                    , NULL        ),
    (@setId    , 'Заявление'                                                                             , 'organizationApplication'                                    , N'^organizationDocumentApplications/\d+$'                , NULL        )
GO

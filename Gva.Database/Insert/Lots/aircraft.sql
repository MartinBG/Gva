GO
INSERT INTO [LotSets] ([Name], [Alias]) VALUES (N'ВС', N'Aircraft')

DECLARE @setId INT = @@IDENTITY

INSERT INTO [LotSetParts]
    ([LotSetId], [Name]                        , [Alias]                  , [PathRegex]                           , [LotSchemaId])
VALUES
    (@setId    , 'Данни за ВС'                 , 'aircraftData'           , N'^aircraftData$'                     , NULL        ),
    (@setId    , 'Право на собственост'        , 'aircraftOwner'          , N'^aircraftDocumentOwners/\d+$'       , NULL        ),
    (@setId    , 'Оборудване'                  , 'aircraftPart'           , N'^aircraftParts/\d+$'                , NULL        ),
    (@setId    , 'Задължение'                  , 'aircraftDebtFM'         , N'^aircraftDocumentDebtsFM/\d+$'      , NULL        ),
    (@setId    , 'Подръжка'                    , 'aircraftMaintenance'    , N'^maintenances/\d+$'                 , NULL        ),
    (@setId    , 'Инцидент'                    , 'aircraftOccurrence'     , N'^documentOccurrences/\d+$'          , NULL        ),
    (@setId    , 'Инспекция'                   , 'aircraftInspection'     , N'^inspections/\d+$'                  , NULL        ),
    (@setId    , 'Заявление'                   , 'aircraftApplication'    , N'^aircraftDocumentApplications/\d+$' , NULL        ),
    (@setId    , 'Друг документ'               , 'aircraftOther'          , N'^aircraftDocumentOthers/\d+$'       , NULL        ),
    (@setId    , 'Регистация'                  , 'aircraftRegistration'   , N'^aircraftCertRegistrations/\d+$'    , NULL        ),
    (@setId    , 'Регистация'                  , 'aircraftRegistrationFM' , N'^aircraftCertRegistrationsFM/\d+$'  , NULL        ),
    (@setId    , 'Летателна годност'           , 'aircraftAirworthinessFM', N'^aircraftCertAirworthinessesFM/\d+$', NULL        ),
    (@setId    , 'Регистрационен знак'         , 'aircraftMark'           , N'^aircraftCertMarks/\d+$'            , NULL        ),
    (@setId    , 'S-mode код'                  , 'aircraftSmod'           , N'^aircraftCertSmods/\d+$'            , NULL        ),
    (@setId    , 'Разрешително за полет'       , 'aircraftPermit'         , N'^aircraftCertPermitsToFly/\d+$'     , NULL        ),
    (@setId    , 'Удосотоверение за шум'       , 'aircraftNoise'          , N'^aircraftCertNoises/\d+$'           , NULL        ),
    (@setId    , 'Разрешително за радиостанция', 'aircraftRadio'          , N'^aircraftCertRadios/\d+$'           , NULL        )
GO

GO
INSERT [dbo].[Noms] ([Name], [Alias], [Category]) VALUES (N'Видове документи', N'documentParts', N'system')

DECLARE @nomId INT = @@IDENTITY

INSERT INTO [NomValues]
    ([NomId], [Code]                    , [Name]                    , [NameAlt], [ParentValueId], [Alias]                   , [IsActive], [TextContent])
VALUES
    (@nomId , N'personDocumentId'       , N'Документ за самоличност', NULL     , NULL           , N'personDocumentId'       , 1         , NULL         ),
    (@nomId , N'personEducation'        , N'Образование'            , NULL     , NULL           , N'personEducation'        , 1         , NULL         ),
    (@nomId , N'personEmployment'       , N'Месторабота'            , NULL     , NULL           , N'personEmployment'       , 1         , NULL         ),
    (@nomId , N'personMedical'          , N'Медицинско'             , NULL     , NULL           , N'personMedical'          , 1         , NULL         ),
    (@nomId , N'personCheck'            , N'Проверка'               , NULL     , NULL           , N'personCheck'            , 1         , NULL         ),
    (@nomId , N'personTraining'         , N'Обучение'               , NULL     , NULL           , N'personTraining'         , 1         , NULL         ),
    (@nomId , N'personLangCert'         , N'Свидетелство за език'   , NULL     , NULL           , N'personLangCert'         , 1         , NULL         ),
    (@nomId , N'personOther'            , N'Друг документ'          , NULL     , NULL           , N'personOther'            , 1         , NULL         ),
    (@nomId , N'personApplication'      , N'Заявление'              , NULL     , NULL           , N'personApplication'      , 1         , NULL         ),
    (@nomId , N'personReport'           , N'Отчет'                  , NULL     , NULL           , N'personReport'           , 1         , NULL         ),

    (@nomId , N'organizationOther'      , N'Друг документ'          , NULL     , NULL           , N'organizationOther'      , 1         , NULL         ),
    (@nomId , N'organizationApplication', N'Заявление'              , NULL     , NULL           , N'organizationApplication', 1         , NULL         ),

    (@nomId , N'aircraftOwner'          , N'Свързано лице'          , NULL     , NULL           , N'aircraftOwner'          , 1         , NULL         ),
    (@nomId , N'aircraftDebtFM'         , N'Тежест'                 , NULL     , NULL           , N'aircraftDebtFM'         , 1         , NULL         ),
    (@nomId , N'aircraftOccurrence'     , N'Инцидент'               , NULL     , NULL           , N'aircraftOccurrence'     , 1         , NULL         ),
    (@nomId , N'aircraftApplication'    , N'Заявление'              , NULL     , NULL           , N'aircraftApplication'    , 1         , NULL         ),
    (@nomId , N'aircraftOther'          , N'Друг документ'          , NULL     , NULL           , N'aircraftOther'          , 1         , NULL         ),

    (@nomId , N'airportOwner'           , N'Свързано лице'          , NULL     , NULL           , N'airportOwner'           , 1         , NULL         ),
    (@nomId , N'airportOther'           , N'Друг документ'          , NULL     , NULL           , N'airportOther'           , 1         , NULL         ),
    (@nomId , N'airportApplication'     , N'Заявление'              , NULL     , NULL           , N'airportApplication'     , 1         , NULL         ),

    (@nomId , N'equipmentOwner'         , N'Свързано лице'          , NULL     , NULL           , N'equipmentOwner'         , 1         , NULL         ),
    (@nomId , N'equipmentOther'         , N'Друг документ'          , NULL     , NULL           , N'equipmentOther'         , 1         , NULL         ),
    (@nomId , N'equipmentApplication'   , N'Заявление'              , NULL     , NULL           , N'equipmentApplication'   , 1         , NULL         )
GO
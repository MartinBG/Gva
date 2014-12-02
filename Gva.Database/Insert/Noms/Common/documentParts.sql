GO
INSERT [dbo].[Noms] ([Name], [Alias]) VALUES (N'Типове документи', N'documentParts')

DECLARE @nomId INT = @@IDENTITY

INSERT INTO [NomValues]
    ([NomId], [Code]                    , [Name]                    , [NameAlt], [ParentValueId], [Alias]                   , [IsActive], [TextContent])
VALUES
    (@nomId , N'personDocumentId'       , N'Документ за самоличност', NULL     , 1              , N'personDocumentId'       , 1         , NULL         ),
    (@nomId , N'personEducation'        , N'Образование'            , NULL     , 1              , N'personEducation'        , 1         , NULL         ),
    (@nomId , N'personEmployment'       , N'Месторабота'            , NULL     , 1              , N'personEmployment'       , 1         , NULL         ),
    (@nomId , N'personMedical'          , N'Медицинско'             , NULL     , 1              , N'personMedical'          , 1         , NULL         ),
    (@nomId , N'personCheck'            , N'Проверка'               , NULL     , 1              , N'personCheck'            , 1         , NULL         ),
    (@nomId , N'personTraining'         , N'Обучение'               , NULL     , 1              , N'personTraining'         , 1         , NULL         ),
    (@nomId , N'personLangCert'         , N'Свидетелство за език'   , NULL     , 1              , N'personLangCert'         , 1         , NULL         ),
    (@nomId , N'personOther'            , N'Друг документ'          , NULL     , 1              , N'personOther'            , 1         , NULL         ),
    (@nomId , N'personReport'           , N'Отчет'                  , NULL     , 1              , N'personReport'           , 1         , NULL         ),

    (@nomId , N'organizationOther'      , N'Друг документ'          , NULL     , 2              , N'organizationOther'      , 1         , NULL         ),

    (@nomId , N'aircraftOwner'          , N'Свързано лице'          , NULL     , 3              , N'aircraftOwner'          , 1         , NULL         ),
    (@nomId , N'aircraftDebtFM'         , N'Тежест'                 , NULL     , 3              , N'aircraftDebtFM'         , 1         , NULL         ),
    (@nomId , N'aircraftOccurrence'     , N'Инцидент'               , NULL     , 3              , N'aircraftOccurrence'     , 1         , NULL         ),
    (@nomId , N'aircraftOther'          , N'Друг документ'          , NULL     , 3              , N'aircraftOther'          , 1         , NULL         ),

    (@nomId , N'airportOwner'           , N'Свързано лице'          , NULL     , 4              , N'airportOwner'           , 1         , NULL         ),
    (@nomId , N'airportOther'           , N'Друг документ'          , NULL     , 4              , N'airportOther'           , 1         , NULL         ),

    (@nomId , N'equipmentOwner'         , N'Свързано лице'          , NULL     , 5              , N'equipmentOwner'         , 1         , NULL         ),
    (@nomId , N'equipmentOther'         , N'Друг документ'          , NULL     , 5              , N'equipmentOther'         , 1         , NULL         )
GO

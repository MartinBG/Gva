GO
INSERT INTO [LotSets] ([Name], [Alias]) VALUES (N'Летище', N'Airport')

DECLARE @setId INT = @@IDENTITY

INSERT INTO [LotSetParts]
    ([LotSetId], [Name]                   , [Alias]             , [PathRegex]                         , [LotSchemaId])
VALUES
    (@setId    , 'Данни за летище'        , 'airportData'       , N'^airportData$'                    , NULL        ),
    (@setId    , 'Свързано лице'          , 'airportOwner'      , N'^airportDocumentOwners/\d+$'      , NULL        ),
    (@setId    , 'Друг документ'          , 'airportOther'      , N'^airportDocumentOthers/\d+$'      , NULL        ),
    (@setId    , 'Заявление'              , 'airportApplication', N'^airportDocumentApplications/\d+$', NULL        ),
    (@setId    , 'Eксплоатационна годност', 'airportOperational', N'^airportCertOperationals/\d+$'    , NULL        ),
    (@setId    , 'Инспекция'              , 'airportInspection' , N'^inspections/\d+$'                , NULL        )
GO

GO
INSERT INTO [LotSets] ([Name], [Alias]) VALUES (N'Съоръжение', N'Equipment')

DECLARE @setId INT = @@IDENTITY

INSERT INTO [LotSetParts]
    ([LotSetId], [Name]                   , [Alias]               , [PathRegex]                           , [LotSchemaId])
VALUES
    (@setId    , 'Данни за съoръжение'    , 'equipmentData'       , N'^equipmentData$'                    , NULL        ),
    (@setId    , 'Право на собственост'   , 'equipmentOwner'      , N'^equipmentDocumentOwners/\d+$'      , NULL        ),
    (@setId    , 'Друг документ'          , 'equipmentOther'      , N'^equipmentDocumentOthers/\d+$'      , NULL        ),
    (@setId    , 'Заявление'              , 'equipmentApplication', N'^equipmentDocumentApplications/\d+$', NULL        ),
    (@setId    , 'Eксплоатационна годност', 'equipmentOperational', N'^equipmentCertOperationals/\d+$'    , NULL        ),
    (@setId    , 'Инспекция'              , 'equipmentInspection' , N'^inspections/\d+$'                  , NULL        )
GO

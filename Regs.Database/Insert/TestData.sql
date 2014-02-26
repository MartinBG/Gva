INSERT INTO [LotSets]
    ([LotSetId], [Name]      , [Alias]   )
VALUES
    (1         , N'Персонал' , N'Person' )
GO


INSERT INTO [LotSetParts]
    ([LotSetPartId], [LotSetId]    , [Alias]            , [PathRegex]                     , [Schema])
VALUES
    (1             , 1             , 'address'         , N'personAddresses/\d+'          , N'{}'   ),
    (2             , 1             , 'data'            , N'personData'                   , N'{}'   ),
    (3             , 1             , 'documentId'      , N'personDocumentIds/\d+'        , N'{}'   ),
    (4             , 1             , 'check'           , N'personDocumentChecks/\d+'     , N'{}'   ),
    (5             , 1             , 'education'       , N'personDocumentEducations/\d+' , N'{}'   ),
    (6             , 1             , 'employment'      , N'personDocumentEmployments/\d+', N'{}'   ),
    (7             , 1             , 'medical'         , N'personDocumentMedicals/\d+'   , N'{}'   ),
    (8             , 1             , 'training'        , N'personDocumentTrainings/\d+'  , N'{}'   ),
    (9             , 1             , 'edition'         , N'ratings/\d+/editions/\d+'     , N'{}'   ),
    (10            , 1             , 'flyingExperience', N'personFlyingExperiences/\d+'  , N'{}'   ),
    (11            , 1             , 'personStatus'    , N'personStatuses/\d+'           , N'{}'   ),
    (12            , 1             , 'licence'         , N''                             , N'{}'   ),
    (13            , 1             , 'rating'          , N''                             , N'{}'   ),
GO

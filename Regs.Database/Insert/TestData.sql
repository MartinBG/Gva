INSERT INTO [LotSets]
    ([LotSetId], [Name]      , [Alias]   )
VALUES
    (1         , N'Персонал' , N'Person' )
GO


INSERT INTO [LotSetParts]
    ([LotSetPartId], [LotSetId], [Alias]      , [PathRegex]                    , [Schema])
VALUES
    (1             , 1         , 'info'       , N'/generalInfo'                , N'{}'   ),
    (2             , 1         , 'addresses'  , N'/addresses/\d+'              , N'{}'   ),
    (3             , 1         , 'ratings'    , N'/ratings/\d+'                , N'{}'   ),
    (4             , 1         , 'ratingDates', N'/ratings/\d+/ratingDates/\d+', N'{}'   )
GO

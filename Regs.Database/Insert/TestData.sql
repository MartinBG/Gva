INSERT INTO [LotSets]
    ([LotSetId], [Name]      , [Alias]   )
VALUES
    (1         , N'Персонал' , N'Person' )
GO


INSERT INTO [LotSetParts]
    ([LotSetPartId], [LotSetId], [PathRegex]                    , [Schema])
VALUES                                                          
    (1             , 1         , N'/generalInfo'                , N'{}'   ),
    (2             , 1         , N'/addresses/\d+'              , N'{}'   ),
    (3             , 1         , N'/ratings/\d+'                , N'{}'   ),
    (4             , 1         , N'/ratings/\d+/ratingDates/\d+', N'{}'   )
GO
INSERT INTO [LotSets]
    ([LotSetId], [Name]      , [Alias]   )
VALUES
    (1         , N'Персонал' , N'Person' )
GO


INSERT INTO [LotSetParts]
    ([LotSetPartId], [LotSetId], [Path]                     , [Schema])
VALUES
    (1             , 1         , N'/generalInfo'            , N'{}'   ),
    (2             , 1         , N'/addresses/*'            , N'{}'   ),
    (3             , 1         , N'/ratings/*'              , N'{}'   ),
    (4             , 1         , N'/ratings/*/ratingDates/*', N'{}'   )
GO
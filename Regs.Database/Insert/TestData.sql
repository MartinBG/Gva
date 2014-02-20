INSERT INTO [LotSets]
    ([LotSetId], [Name]      , [Alias]   )
VALUES
    (1         , N'Персонал' , N'Person' )
GO


INSERT INTO [LotSetParts]
    ([LotSetPartId], [LotSetId]    , [Alias]                     , [PathRegex]                    , [Schema])
VALUES
    (1                     , 1                   , 'addresses'           , N'personAddresses/\d+'        , N'{}'   ),
    (2                     , 1                   , 'data'                      , N'personData'                 , N'{}'   ),
    (3                     , 1                   , 'documentIds'        , N'personDocumentIds/\d+'      , N'{}'   ),
	(4                     , 1                   , 'checks'                  , N'personDocumentChecks/\d+', N'{}'),
	(5                     , 1                   , 'educations'   , N'personDocumentEducations/\d+', N'{}'),
	(6                     , 1                   , 'employments', N'personDocumentEmployments/\d+', N'{}'),
	(7                     , 1                   , 'medicals'        , N'personDocumentMedicals/\d+', N'{}'),
	(8                     , 1                   , 'trainings'        , N'personDocumentTrainings/\d+', N'{}'),
	(9                     , 1                   , 'editions', N'ratings/\d+/editions/\d+', N'{}'),
	(10                   , 1                   , 'flyingExperiences', N'personFlyingExperiences/\d+', N'{}'),
	(11                   , 1                   , 'personStatuses', N'personStatuses/\d+', N'{}')
GO

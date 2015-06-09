GO
INSERT INTO [LotSets] ([Name], [Alias]) VALUES (N'S Mode кодове', N'SModeCode')

DECLARE @setId INT = @@IDENTITY

INSERT INTO [LotSetParts]
    ([LotSetId], [Name]                 , [Alias]         , [PathRegex]        , [LotSchemaId])
VALUES
    (@setId    , 'Данни за S Mode код'  , 'sModeCodeData' , N'^sModeCodeData$' , NULL        )
GO

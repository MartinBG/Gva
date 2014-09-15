GO
INSERT [dbo].[Noms] ([Name], [Alias]) VALUES (N'Тип възложител', N'AopEmployerType')

DECLARE @nomId INT = @@IDENTITY

INSERT INTO [NomValues]
    ([NomId], [Code]     , [Name]                  , [NameAlt], [ParentValueId], [Alias]    , [IsActive], [TextContent])
VALUES
    (@nomId , N'M7.1-4'  , N'чл. 7, т. 1-4 ЗОП'    , NULL     , NULL           , N'M7.1-4'  , 1         , NULL         ),
    (@nomId , N'Central' , N'централен'            , NULL     , NULL           , N'Central' , 1         , NULL         ),
    (@nomId , N'Regional', N'регионален'           , NULL     , NULL           , N'Regional', 1         , NULL         ),
    (@nomId , N'M7.5-6'  , N'чл. 7, т. 5 или 6 ЗОП', NULL     , NULL           , N'M7.5-6'  , 1         , NULL         ),
    (@nomId , N'Unknown' , N'неопределен'          , NULL     , NULL           , N'Unknown' , 1         , NULL         )
GO

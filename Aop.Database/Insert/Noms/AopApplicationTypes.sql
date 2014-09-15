GO
INSERT [dbo].[Noms] ([Name], [Alias]) VALUES (N'Вид на процедурата', N'AopApplicationType')

DECLARE @nomId INT = @@IDENTITY

INSERT INTO [NomValues]
    ([NomId], [Code]               , [Name]                               , [NameAlt], [ParentValueId], [Alias]              , [IsActive], [TextContent])
VALUES
    (@nomId , N'Open'              , N'Открита'                           , NULL     , NULL           , N'Open'              , 1         , NULL         ),
    (@nomId , N'Limited'           , N'Ограничена'                        , NULL     , NULL           , N'Limited'           , 1         , NULL         ),
    (@nomId , N'AcceleratedLimited', N'Ускорена ограничена'               , NULL     , NULL           , N'AcceleratedLimited', 1         , NULL         ),
    (@nomId , N'Dealed'            , N'Договаряне с обявление'            , NULL     , NULL           , N'Dealed'            , 1         , NULL         ),
    (@nomId , N'AcceleratedDealed' , N'Ускорена на договаряне с обявление', NULL     , NULL           , N'AcceleratedDealed' , 1         , NULL         ),
    (@nomId , N'CompetitiveDialog' , N'Състезателен диалог'               , NULL     , NULL           , N'CompetitiveDialog' , 1         , NULL         )
GO

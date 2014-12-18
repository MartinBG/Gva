GO
INSERT [dbo].[Noms] ([Name], [Alias], [Category]) VALUES (N'Причина за дерегистрация на ВС', N'aircraftRemovalReasonsFm', 'aircraft')

DECLARE @nomId INT = @@IDENTITY

INSERT INTO [NomValues]
    ([NomId], [Code], [Name]                          , [NameAlt]               , [ParentValueId], [Alias]    , [IsActive], [TextContent])
VALUES                                                                                                        
    (@nomId , N'7'  , N'Изтичане на срока на договора', N'Contract date expired', NULL           , NULL       , 1         , NULL         ),
    (@nomId , N'8'  , N'Смяна собственост'            , N'Ownership change'     , NULL           , NULL       , 1         , NULL         ),
    (@nomId , N'9'  , N'Брак'                         , N'Totaled'              , NULL           , NULL       , 1         , NULL         ),
    (@nomId , N'36' , N'Заповед'                      , N'Order'                , NULL           , NULL       , 1         , NULL         ),
    (@nomId , N'0' ,  N'Миграция'                     , N'Migration'            , NULL           , 'migration', 1         , NULL         )
GO

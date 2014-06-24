﻿GO
INSERT [dbo].[Noms] ([Name], [Alias]) VALUES (N'Номенклатура типове лин', N'linTypes')

DECLARE @nomId INT = @@IDENTITY

INSERT INTO [NomValues]
    ([NomId], [Code]             , [Name]             , [NameAlt]          , [ParentValueId], [Alias], [IsActive], [TextContent])
VALUES
    (@nomId , N'none'            , N'Няма'            , N'Няма'            , NULL           , NULL   , 1         , NULL         ),
    (@nomId , N'pilots'          , N'Пилоти'          , N'Пилоти'          , NULL           , NULL   , 1         , NULL         ),
    (@nomId , N'flyingCrew'      , N'Летателен състав', N'Летателен състав', NULL           , NULL   , 1         , NULL         ),
    (@nomId , N'crewStaff'       , N'Кабинен състав'  , N'Кабинен състав'  , NULL           , NULL   , 1         , NULL         ),
    (@nomId , N'headFlights'     , N'Рък. на полети'  , N'Рък. на полети'  , NULL           , NULL   , 1         , NULL         ),
    (@nomId , N'airlineEngineers', N'Авио-инженери'   , N'Авио-инженери'   , NULL           , NULL   , 1         , NULL         ),
    (@nomId , N'dispatchers'     , N'Диспечери'       , N'Диспечери'       , NULL           , NULL   , 1         , NULL         ),
    (@nomId , N'paratroopers'    , N'Парашутисти'     , N'Парашутисти'     , NULL           , NULL   , 1         , NULL         ),
    (@nomId , N'engineersRVD'    , N'Инженери РВД'    , N'Инженери РВД'    , NULL           , NULL   , 1         , NULL         ),
    (@nomId , N'deltaplaner'     , N'Делтапланери'    , N'Делтапланери'    , NULL           , NULL   , 1         , NULL         )
GO
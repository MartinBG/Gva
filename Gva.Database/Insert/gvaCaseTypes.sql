INSERT INTO [GvaCaseTypes]
    ([GvaCaseTypeId], [Name]       , [Alias]            , [LotSetId])
VALUES
    (1              , N'Общи'      , 'none'             , 1         ),
    (2              , N'Екипажи'   , 'flightCrew'       , 1         ),
    (3              , N'ОВД'       , 'ovd'              , 1         ),
    (4              , N'ТО(AML)'   , 'to_vs'            , 1         ),
    (5              , N'ТО(СУВД)'  , 'to_suvd'          , 1         ),
    (6              , N'ОО'        , 'approvedOrg'      , 2         ),
    (7              , N'ЛО'        , 'airportOperator'  , 2         ),
    (8              , N'ОНО'       , 'groundSvcOperator', 2         ),
    (9              , N'ВП'        , 'airCarrier'       , 2         ),
    (10             , N'АО'        , 'airOperator'      , 2         ),
    (11             , N'АУЦ'       , 'educationOrg'     , 2         ),
    (12             , N'ДАО'       , 'airNavSvcProvider', 2         ),
    (13             , N'Инспектор' , 'inspector'        , 1         ),
    (14             , N'Проверяващ', 'examiner'         , 1         ),
    (15             , N'ВС'        , 'aircraft'         , 3         ),
    (16             , N'Летище'    , 'airport'          , 4         ),
    (17             , N'Съоръжение', 'equipment'        , 5         )
GO

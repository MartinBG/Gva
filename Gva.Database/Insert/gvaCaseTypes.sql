INSERT INTO [GvaCaseTypes]
    ([GvaCaseTypeId], [Name]       , [Alias]            , [LotSetId])
VALUES
    (1              , N'Екипажи'   , 'flightCrew'       , 1         ),
    (2              , N'ОВД'       , 'ovd'              , 1         ),
    (3              , N'ТО(AML)'   , 'to_vs'            , 1         ),
    (4              , N'ТО(СУВД)'  , 'to_suvd'          , 1         ),
    (5              , N'ОО'        , 'approvedOrg'      , 2         ),
    (6              , N'ЛО'        , 'airportOperator'  , 2         ),
    (7              , N'ОНО'       , 'groundSvcOperator', 2         ),
    (8              , N'ВП'        , 'airCarrier'       , 2         ),
    (9              , N'АО'        , 'airOperator'      , 2         ),
    (10             , N'АУЦ'       , 'educationOrg'     , 2         ),
    (11             , N'ДАО'       , 'airNavSvcProvider', 2         ),
    (12             , N'Инспектор' , 'inspector'        , 1         ),
    (13             , N'Проверяващ', 'examiner'         , 1         ),
    (14             , N'ВС'        , 'aircraft'         , 3         ),
    (15             , N'Летище'    , 'airport'          , 4         ),
    (16             , N'Съоръжение', 'equipment'        , 5         )
GO

INSERT INTO [GvaCaseTypes]
    ([GvaCaseTypeId], [Name]       , [Alias]    , [LotSetId])
VALUES
    (1              , N'Пилот'     , 'pilot'            , 1         ),
    (2              , N'РВД'       , 'RVD'              , 1         ),
    (3              , N'ОО'        , 'approvedOrg'      , 2         ),
    (4              , N'ЛО'        , 'airportOperator'  , 2         ),
    (5              , N'ОНО'       , 'groundSvcOperator', 2         ),
    (6              , N'ВП'        , 'airCarrier'       , 2         ),
    (7              , N'АО'        , 'airOperator'      , 2         ),
    (8              , N'АУЦ'       , 'educationOrg'     , 2         ),
    (9              , N'ДАО'       , 'airNavSvcProvider', 2         ),
    (10             , N'Инспектор' , 'inspector'        , 1         ),
    (11             , N'Проверяващ', 'examiner'         , 1         ),
    (12             , N'ВС'        , 'aircraft'         , 3         ),
    (13             , N'Летище'    , 'airport'          , 4         ),
    (14             , N'Съоръжение', 'equipment'        , 5         )
GO
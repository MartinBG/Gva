print 'Insert gvaCaseTypes'
GO

INSERT INTO [GvaCaseTypes]
    ([GvaCaseTypeId], [Name]       , [Alias]             , [IsDefault], [IsActive],[LotSetId]                                                  , [ClassificationId])
VALUES
    (1              , N'Общи'           , N'person'           ,           1,          0,(select LotSetId from LotSets where Alias = N'Person'      ), (select ClassificationId from Classifications where Alias = N'person'           )),
    (2              , N'Екипажи'        , N'flightCrew'       ,           0,          1,(select LotSetId from LotSets where Alias = N'Person'      ), (select ClassificationId from Classifications where Alias = N'flightCrew'       )),
    (3              , N'ОВД'            , N'ovd'              ,           0,          1,(select LotSetId from LotSets where Alias = N'Person'      ), (select ClassificationId from Classifications where Alias = N'ovd'              )),
    (4              , N'ТО(AML)'        , N'to_vs'            ,           0,          1,(select LotSetId from LotSets where Alias = N'Person'      ), (select ClassificationId from Classifications where Alias = N'to_vs'            )),
    (5              , N'ТО(СУВД)'       , N'to_suvd'          ,           0,          1,(select LotSetId from LotSets where Alias = N'Person'      ), (select ClassificationId from Classifications where Alias = N'to_suvd'          )),
    (6              , N'Сигурност'      , N'security'         ,           0,          1,(select LotSetId from LotSets where Alias = N'Person'      ), (select ClassificationId from Classifications where Alias = N'security'         )),
    (7              , N'Инспектор'      , N'inspector'        ,           0,          1,(select LotSetId from LotSets where Alias = N'Person'      ), NULL                                                                            ),
    (8              , N'Проверяващ ЛГ'  , N'awExaminer'       ,           0,          1,(select LotSetId from LotSets where Alias = N'Person'      ), NULL                                                                            ),
    (9              , N'Проверяващ лица', N'staffExaminer'    ,           0,          1,(select LotSetId from LotSets where Alias = N'Person'      ), (select ClassificationId from Classifications where Alias = N'staffExaminer'    )),
    (10             , N'Други'          , N'others'           ,           1,          1,(select LotSetId from LotSets where Alias = N'Organization'), (select ClassificationId from Classifications where Alias = N'org'              )),
    (11             , N'ОО'             , N'approvedOrg'      ,           0,          1,(select LotSetId from LotSets where Alias = N'Organization'), (select ClassificationId from Classifications where Alias = N'approvedOrg'      )),
    (12             , N'ЛО'             , N'airportOperator'  ,           0,          1,(select LotSetId from LotSets where Alias = N'Organization'), (select ClassificationId from Classifications where Alias = N'airportOperator'  )),
    (13             , N'ОНО'            , N'groundSvcOperator',           0,          1,(select LotSetId from LotSets where Alias = N'Organization'), (select ClassificationId from Classifications where Alias = N'groundSvcOperator')),
    (14             , N'ВП'             , N'airCarrier'       ,           0,          1,(select LotSetId from LotSets where Alias = N'Organization'), (select ClassificationId from Classifications where Alias = N'airCarrier'       )),
    (15             , N'АО'             , N'airOperator'      ,           0,          1,(select LotSetId from LotSets where Alias = N'Organization'), (select ClassificationId from Classifications where Alias = N'airOperator'      )),
    (16             , N'АУЦ'            , N'educationOrg'     ,           0,          1,(select LotSetId from LotSets where Alias = N'Organization'), (select ClassificationId from Classifications where Alias = N'educationOrg'     )),
    (17             , N'ДАО'            , N'airNavSvcProvider',           0,          1,(select LotSetId from LotSets where Alias = N'Organization'), (select ClassificationId from Classifications where Alias = N'airNavSvcProvider')),
    (18             , N'ВС'             , N'aircraft'         ,           1,          1,(select LotSetId from LotSets where Alias = N'Aircraft'    ), (select ClassificationId from Classifications where Alias = N'aircraft'         )),
    (19             , N'Летище'         , N'airport'          ,           1,          1,(select LotSetId from LotSets where Alias = N'Airport'     ), (select ClassificationId from Classifications where Alias = N'airport'          )),
    (20             , N'Съоръжение'     , N'equipment'        ,           1,          1,(select LotSetId from LotSets where Alias = N'Equipment'   ), (select ClassificationId from Classifications where Alias = N'equipment'        ))
GO

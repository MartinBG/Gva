﻿print 'Insert gvaCaseTypes'
GO

INSERT INTO [GvaCaseTypes]
    ([GvaCaseTypeId], [Name]       , [Alias]             , [IsDefault], [LotSetId]                                                  , [ClassificationId])
VALUES
    (1              , N'Общи'      , N'person'           ,           1, (select LotSetId from LotSets where Alias = N'Person'      ), (select ClassificationId from Classifications where Alias = N'person'           )),
    (2              , N'Екипажи'   , N'flightCrew'       ,           0, (select LotSetId from LotSets where Alias = N'Person'      ), (select ClassificationId from Classifications where Alias = N'flightCrew'       )),
    (3              , N'ОВД'       , N'ovd'              ,           0, (select LotSetId from LotSets where Alias = N'Person'      ), (select ClassificationId from Classifications where Alias = N'ovd'              )),
    (4              , N'ТО(AML)'   , N'to_vs'            ,           0, (select LotSetId from LotSets where Alias = N'Person'      ), (select ClassificationId from Classifications where Alias = N'to_vs'            )),
    (5              , N'ТО(СУВД)'  , N'to_suvd'          ,           0, (select LotSetId from LotSets where Alias = N'Person'      ), (select ClassificationId from Classifications where Alias = N'to_suvd'          )),
    (6              , N'Сигурност' , N'security'         ,           0, (select LotSetId from LotSets where Alias = N'Person'      ), (select ClassificationId from Classifications where Alias = N'security'         )),
    (7              , N'Инспектор' , N'inspector'        ,           0, (select LotSetId from LotSets where Alias = N'Person'      ), NULL                                                                            ),
    (8              , N'Проверяващ', N'examiner'         ,           0, (select LotSetId from LotSets where Alias = N'Person'      ), NULL                                                                            ),
    (9              , N'Други'     , N'others'           ,           1, (select LotSetId from LotSets where Alias = N'Organization'), (select ClassificationId from Classifications where Alias = N'org'              )),
    (10             , N'ОО'        , N'approvedOrg'      ,           0, (select LotSetId from LotSets where Alias = N'Organization'), (select ClassificationId from Classifications where Alias = N'approvedOrg'      )),
    (11             , N'ЛО'        , N'airportOperator'  ,           0, (select LotSetId from LotSets where Alias = N'Organization'), (select ClassificationId from Classifications where Alias = N'airportOperator'  )),
    (12             , N'ОНО'       , N'groundSvcOperator',           0, (select LotSetId from LotSets where Alias = N'Organization'), (select ClassificationId from Classifications where Alias = N'groundSvcOperator')),
    (13             , N'ВП'        , N'airCarrier'       ,           0, (select LotSetId from LotSets where Alias = N'Organization'), (select ClassificationId from Classifications where Alias = N'airCarrier'       )),
    (14             , N'АО'        , N'airOperator'      ,           0, (select LotSetId from LotSets where Alias = N'Organization'), (select ClassificationId from Classifications where Alias = N'airOperator'      )),
    (15             , N'АУЦ'       , N'educationOrg'     ,           0, (select LotSetId from LotSets where Alias = N'Organization'), (select ClassificationId from Classifications where Alias = N'educationOrg'     )),
    (16             , N'ДАО'       , N'airNavSvcProvider',           0, (select LotSetId from LotSets where Alias = N'Organization'), (select ClassificationId from Classifications where Alias = N'airNavSvcProvider')),
    (17             , N'ВС'        , N'aircraft'         ,           1, (select LotSetId from LotSets where Alias = N'Aircraft'    ), (select ClassificationId from Classifications where Alias = N'aircraft'         )),
    (18             , N'Летище'    , N'airport'          ,           1, (select LotSetId from LotSets where Alias = N'Airport'     ), (select ClassificationId from Classifications where Alias = N'airport'          )),
    (19             , N'Съоръжение', N'equipment'        ,           1, (select LotSetId from LotSets where Alias = N'Equipment'   ), (select ClassificationId from Classifications where Alias = N'equipment'        ))
GO

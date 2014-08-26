DECLARE @personSetId INT;
SELECT @personSetId = LotSetId FROM LotSets WHERE Alias = 'Person'

DECLARE @organizationSetId INT;
SELECT @organizationSetId = LotSetId FROM LotSets WHERE Alias = 'Organization'

DECLARE @aircraftSetId INT;
SELECT @aircraftSetId = LotSetId FROM LotSets WHERE Alias = 'Aircraft'

DECLARE @airportSetId INT;
SELECT @airportSetId = LotSetId FROM LotSets WHERE Alias = 'Airport'

DECLARE @equipmentSetId INT;
SELECT @equipmentSetId = LotSetId FROM LotSets WHERE Alias = 'Equipment'

INSERT INTO [GvaCaseTypes]
    ([GvaCaseTypeId], [Name]       , [Alias]            , [LotSetId]        )
VALUES
    (1              , N'Общи'      , 'person'           , @personSetId      ),
    (2              , N'Екипажи'   , 'flightCrew'       , @personSetId      ),
    (3              , N'ОВД'       , 'ovd'              , @personSetId      ),
    (4              , N'ТО(AML)'   , 'to_vs'            , @personSetId      ),
    (5              , N'ТО(СУВД)'  , 'to_suvd'          , @personSetId      ),
    (6              , N'Инспектор' , 'inspector'        , @personSetId      ),
    (7              , N'Проверяващ', 'examiner'         , @personSetId      ),
    (8              , N'Общи'      , 'org'              , @organizationSetId),
    (9              , N'ОО'        , 'approvedOrg'      , @organizationSetId),
    (10             , N'ЛО'        , 'airportOperator'  , @organizationSetId),
    (11             , N'ОНО'       , 'groundSvcOperator', @organizationSetId),
    (12             , N'ВП'        , 'airCarrier'       , @organizationSetId),
    (13             , N'АО'        , 'airOperator'      , @organizationSetId),
    (14             , N'АУЦ'       , 'educationOrg'     , @organizationSetId),
    (15             , N'ДАО'       , 'airNavSvcProvider', @organizationSetId),
    (16             , N'ВС'        , 'aircraft'         , @aircraftSetId    ),
    (17             , N'Летище'    , 'airport'          , @airportSetId     ),
    (18             , N'Съоръжение', 'equipment'        , @equipmentSetId   )
GO

select *
into #tNewElectronicServiceStages
from
(VALUES
    (1      ,N'Новопостъпило'        ,NULL         ,N'AcceptApplication',NULL      ,0                ,1                 ,0            ,1         ),
    (2      ,N'В обработка'          ,NULL         ,NULL                ,NULL      ,0                ,0                 ,0            ,1         ),
    (3      ,N'Нови документи'       ,NULL         ,NULL                ,NULL      ,0                ,0                 ,0            ,1         ),
    (4      ,N'Отказано'             ,NULL         ,NULL                ,NULL      ,0                ,0                 ,0            ,1         ),
    (5      ,N'Одобрено'             ,NULL         ,NULL                ,NULL      ,0                ,0                 ,0            ,1         ),
    (6      ,N'Готов лиценз'         ,NULL         ,NULL                ,NULL      ,0                ,0                 ,0            ,1         ),
    (7      ,N'Предаден на заявителя',NULL         ,NULL                ,NULL      ,0                ,0                 ,0            ,1         ),
    (8      ,N'Приключено'           ,NULL         ,NULL                ,NULL      ,0                ,0                 ,0            ,1         )
) as
   t([OldId],[Name]                  ,[Description],[Alias]             ,[Duration],[IsDurationReset],[IsFirstByDefault],[IsLastStage],[IsActive])

select * from #tNewElectronicServiceStages

select [DocTypeId]
into #tDocTypes
from [DocTypes]
where PrimaryRegisterIndexId = 4 and DocTypeId not in (69,70)

select * from #tDocTypes

select row_number() over (order by dt.DocTypeId, es.OldId) + 24 as [ElectronicServiceStageId], dt.DocTypeId, es.*
into #tElectronicServiceStages
from #tDocTypes dt
    inner join #tNewElectronicServiceStages es on 1 = 1
order by dt.DocTypeId, es.OldId

select * from #tElectronicServiceStages

select row_number() over (order by es.[ElectronicServiceStageId]) + 24 as [ElectronicServiceStageExecutorId], es.[ElectronicServiceStageId], 79 as [UnitId], 1 as [IsActive]
into #tElectronicServiceStageExecutors
from #tElectronicServiceStages es
order by es.[ElectronicServiceStageId]

select * from #tElectronicServiceStageExecutors

delete ess
--select *
from ElectronicServiceStageExecutors ess
    inner join ElectronicServiceStages es on ess.ElectronicServiceStageId = es.ElectronicServiceStageId
where es.DocTypeId in (select * from #tDocTypes)

delete es
--select *
from ElectronicServiceStages es
where es.DocTypeId in (select * from #tDocTypes)

SET IDENTITY_INSERT [ElectronicServiceStages] ON

insert into ElectronicServiceStages
    ([ElectronicServiceStageId],[DocTypeId],[Name],[Description],[Alias],[Duration],[IsDurationReset],[IsFirstByDefault],[IsLastStage],[IsActive])
select
    [ElectronicServiceStageId],[DocTypeId],[Name],[Description],[Alias],[Duration],[IsDurationReset],[IsFirstByDefault],[IsLastStage],[IsActive]
from #tElectronicServiceStages

SET IDENTITY_INSERT [ElectronicServiceStages] OFF

SET IDENTITY_INSERT [ElectronicServiceStageExecutors] ON

insert into [ElectronicServiceStageExecutors]
    ([ElectronicServiceStageExecutorId],[ElectronicServiceStageId],[UnitId],[IsActive])
select
    [ElectronicServiceStageExecutorId],[ElectronicServiceStageId],[UnitId],[IsActive]
from #tElectronicServiceStageExecutors

SET IDENTITY_INSERT [ElectronicServiceStageExecutors] OFF

select
    row_number() over (order by d.DocId, st.Ordinal) as [DocElectronicServiceStageId],
    d.DocId,
    ess.[ElectronicServiceStageId],
    st.StartingDate,
    NULL as [ExpectedEndingDate],
    nextStage.StartingDate as [EndingDate],
    case when row_number() over (partition by d.DocId order by st.Ordinal desc) = 1 then 1 else 0 end as [IsCurrentStage],
    st.GvaAppStageId
into #tDocElectronicServiceStages
from Docs d
    inner join GvaApplications a on d.DocId = a.DocId
    inner join GvaAppStages st on st.GvaApplicationId = a.GvaApplicationId
    left join GvaAppStages nextStage on nextStage.GvaApplicationId = a.GvaApplicationId and nextStage.Ordinal = st.Ordinal + 1
    inner join #tElectronicServiceStages ess on st.GvaStageId = ess.OldId and d.DocTypeId = ess.DocTypeId
order by d.DocId, st.Ordinal

select * from #tDocElectronicServiceStages
order by [DocElectronicServiceStageId]

SET IDENTITY_INSERT DocElectronicServiceStages ON

insert into DocElectronicServiceStages
    ([DocElectronicServiceStageId],[DocId],[ElectronicServiceStageId],[StartingDate],[ExpectedEndingDate],[EndingDate],[IsCurrentStage])
select
    [DocElectronicServiceStageId],[DocId],[ElectronicServiceStageId],[StartingDate],[ExpectedEndingDate],[EndingDate],[IsCurrentStage]
from #tDocElectronicServiceStages

SET IDENTITY_INSERT DocElectronicServiceStages OFF

drop table #tNewElectronicServiceStages
drop table #tDocTypes
drop table #tElectronicServiceStages
drop table #tElectronicServiceStageExecutors
drop table #tDocElectronicServiceStages

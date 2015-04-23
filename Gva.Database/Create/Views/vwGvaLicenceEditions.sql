print 'vwGvaLicenceEditions'
GO

CREATE VIEW [dbo].[vwGvaLicenceEditions] WITH SCHEMABINDING
AS
    select l.LotId, e.PartId as EditionPartId, l.PartId as LicencePartId, e.[Index] as EditionIndex, l.LicenceTypeId, e.StampNumber, e.FirstDocDateValidFrom, e.DateValidFrom, e.DateValidTo,
    e.LicenceActionId,l.LicenceNumber, e.LicencePartIndex, e.PartIndex, e.IsLastEdition,
    l.Valid, l.LicenceTypeCaCode, l.PublisherCode, l.ForeignLicenceNumber, l.ForeignPublisher, e.Notes, e.Inspector, l.StatusChange, e.Limitations,
    lf.GvaLotFileId, ga.GvaApplicationId, ga.GvaAppLotPartId as ApplicationPartId, s.GvaStageId
    from [dbo].[GvaViewPersonLicenceEditions] e
    join [dbo].[GvaViewPersonLicences] l on e.LicencePartIndex = l.PartIndex and e.LotId = l.LotId
        left join (select GvaLotFileId, LotPartId
                    from (select lf.GvaLotFileId, lf.LotPartId, ROW_NUMBER() over (partition by lf.LotPartId order by lf.LotPartId) as num
                    from [dbo].[GvaLotFiles] lf) x
                    where x.num = 1) lf on e.PartId = lf.LotPartId
        left join (select GvaLotFileId, GvaApplicationId
                    from (select alf.GvaLotFileId, alf.GvaApplicationId, ROW_NUMBER() over (partition by alf.GvaLotFileId, alf.GvaApplicationId order by alf.GvaLotFileId, alf.GvaApplicationId) as num
                    from [dbo].[GvaAppLotFiles] alf) x
                    where x.num = 1) alf on lf.GvaLotFileId = alf.GvaLotFileId
        left join [dbo].[GvaApplications] ga on alf.GvaApplicationId = ga.GvaApplicationId
        left join (select apps.GvaApplicationId, max(apps.GvaStageId) as GvaStageId
                    from [dbo].[GvaAppStages] apps
                    group by apps.GvaApplicationId) s on ga.GvaApplicationId = s.GvaApplicationId
GO


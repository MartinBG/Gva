print 'vwGvaLicenceEditions'
GO

CREATE VIEW [dbo].[vwGvaLicenceEditions] WITH SCHEMABINDING
AS
    select e.LotId, e.LicencePartId, e.EditionPartId, e.EditionIndex, e.LicenceTypeId, e.StampNumber, e.DateValidFrom, e.DateValidTo,
    e.LicenceActionId, e.LicenceNumber, e.IsLastEdition, e.LicencePartIndex, e.EditionPartIndex, e.FirstDocDateValidFrom,
    e.Valid, e.LicenceTypeCode, e.LicenceTypeCaCode, e.PublisherCode,
    lf.GvaLotFileId, ga.GvaApplicationId, ga.GvaAppLotPartId as ApplicationPartId, s.GvaStageId
    from [dbo].[GvaViewPersonLicenceEditions] e
        left join (select GvaLotFileId, LotPartId
                    from (select lf.GvaLotFileId, lf.LotPartId, ROW_NUMBER() over (partition by lf.LotPartId order by lf.LotPartId) as num
                    from [dbo].[GvaLotFiles] lf) x
                    where x.num = 1) lf on e.EditionPartId = lf.LotPartId
        left join (select GvaLotFileId, GvaApplicationId
                    from (select alf.GvaLotFileId, alf.GvaApplicationId, ROW_NUMBER() over (partition by alf.GvaLotFileId, alf.GvaApplicationId order by alf.GvaLotFileId, alf.GvaApplicationId) as num
                    from [dbo].[GvaAppLotFiles] alf) x
                    where x.num = 1) alf on lf.GvaLotFileId = alf.GvaLotFileId
        left join [dbo].[GvaApplications] ga on alf.GvaApplicationId = ga.GvaApplicationId
        left join (select apps.GvaApplicationId, max(apps.GvaStageId) as GvaStageId
                    from [dbo].[GvaAppStages] apps
                    group by apps.GvaApplicationId) s on ga.GvaApplicationId = s.GvaApplicationId
GO

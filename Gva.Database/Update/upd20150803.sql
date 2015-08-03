alter table GvaViewPersonLicenceEditions add PaperId         INT NULL
alter table GvaViewPersonLicenceEditions add HasNoNumber     BIT NULL
GO

PRINT 'GvaPapers'
GO 

CREATE TABLE [dbo].[GvaPapers] (
    [GvaPaperId]        INT            NOT NULL IDENTITY,
    [Name]              NVARCHAR (50)  NOT NULL,
    [FromDate]          DATETIME2      NOT NULL,
	[ToDate]            DATETIME2      NOT NULL,
    [IsActive]          BIT            NOT NULL,
    [FirstNumber]       INT            NOT NULL,
    CONSTRAINT [PK_GvaPapers]         PRIMARY KEY ([GvaPaperId])
)
GO

CREATE UNIQUE NONCLUSTERED INDEX [UQ_GvaPapers_Name]
ON [dbo].[GvaPapers](Name)
GO

exec spDescTable  N'GvaPapers', N'Информация за хартия за принтиране на документи.'
exec spDescColumn N'GvaPapers', N'GvaPaperId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'GvaPapers', N'Name'             , N'Наименование на хартита.'
exec spDescColumn N'GvaPapers', N'FromDate'         , N'От дата.'
exec spDescColumn N'GvaPapers', N'ToDate'           , N'До дата.'
exec spDescColumn N'GvaPapers', N'IsActive'         , N'Флаг дали е активен записа.'
exec spDescColumn N'GvaPapers', N'FirstNumber'      , N'Първи № на тази хартия.'
GO

SET IDENTITY_INSERT [dbo].[GvaPapers] ON 
GO

INSERT [dbo].[GvaPapers] ([GvaPaperId], [Name], [FromDate], [ToDate], [IsActive], [FirstNumber]) VALUES (1 , N'Стара хартия' , N'1900-01-01 00:00:00.0000000', N'2020-01-01 00:00:00.0000000', 1, 1 )

SET IDENTITY_INSERT [dbo].[GvaPapers] OFF 
GO

DROP VIEW [dbo].[vwGvaLicenceEditions]
GO

CREATE VIEW [dbo].[vwGvaLicenceEditions] WITH SCHEMABINDING
AS
    select l.LotId, e.PartId as EditionPartId, l.PartId as LicencePartId, e.[Index] as EditionIndex, l.LicenceTypeId, e.StampNumber, e.FirstDocDateValidFrom, e.DateValidFrom, e.DateValidTo,
    e.LicenceActionId,l.LicenceNumber, e.LicencePartIndex, e.PartIndex, e.IsLastEdition, e.HasNoNumber,
    l.Valid, l.LicenceTypeCaCode, l.PublisherCode, l.ForeignLicenceNumber, l.ForeignPublisher, e.Notes, e.Inspector, l.StatusChange, e.Limitations, e.OfficiallyReissuedStageId,
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

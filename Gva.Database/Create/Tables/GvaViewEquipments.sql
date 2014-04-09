PRINT 'GvaViewEquipments'
GO 

CREATE TABLE [dbo].[GvaViewEquipments] (
    [LotId]            INT       NOT NULL,
    [Name]            NVARCHAR(50)  NOT NULL,
    [EquipmentProducer]            NVARCHAR(50)  NOT NULL,
    [EquipmentType]         NVARCHAR(50)  NOT NULL,
    [ManPlace]       NVARCHAR(50)     NOT NULL,
    [ManDate]             DATETIME2  NOT NULL,
    [Place]         NVARCHAR(50) NULL,
    [OperationalDate]         NVARCHAR(50) NULL,
    [Note]           NVARCHAR(50)  NULL,
    CONSTRAINT [PK_GvaViewEquipments]      PRIMARY KEY ([LotId]),
    CONSTRAINT [FK_GvaViewEquipments_Lots]  FOREIGN KEY ([LotId]) REFERENCES [dbo].[Lots] ([LotId])
)
GO

exec spDescTable  N'GvaViewEquipments', N'Летища.'
exec spDescColumn N'GvaViewEquipments', N'LotId', N'Идентификатор на партида на ВС.'
exec spDescColumn N'GvaViewEquipments', N'Name'           , N'Наименование.'
exec spDescColumn N'GvaViewEquipments', N'EquipmentProducer'           , N'Наименование.(англ.)'
exec spDescColumn N'GvaViewEquipments', N'EquipmentType'           , N'Вид летище.'
exec spDescColumn N'GvaViewEquipments', N'ManPlace'           , N'Местоположение.'
exec spDescColumn N'GvaViewEquipments', N'Place'           , N'ICAO код.'
exec spDescColumn N'GvaViewEquipments', N'OperationalDate'      , N'Лицензи.'
exec spDescColumn N'GvaViewEquipments', N'Note'       , N'Квалификации.'
GO

PRINT 'Settlements'
GO 

CREATE TABLE [dbo].[Settlements] (
    [SettlementId]        INT            NOT NULL IDENTITY,
    [MunicipalityId]      INT            NOT NULL,
    [DistrictId]          INT            NOT NULL,
    [Code]                NVARCHAR (10)  NOT NULL,
    [MunicipalityCode]    NVARCHAR (10)  NULL,
    [DistrictCode]        NVARCHAR (10)  NULL,
    [MunicipalityCode2]   NVARCHAR (10)  NULL,
    [DistrictCode2]       NVARCHAR (10)  NULL,
    [Name]                NVARCHAR (200) NOT NULL,
    [TypeName]            NVARCHAR (20)  NOT NULL,
    [SettlementName]      NVARCHAR (200) NOT NULL,
    [TypeCode]            NVARCHAR (50)  NULL,
    [MayoraltyCode]       NVARCHAR (50)  NULL,
    [Category]            NVARCHAR (50)  NULL,
    [Altitude]            NVARCHAR (50)  NULL,
    [Alias]               NVARCHAR (200) NULL,
    [Description]         NVARCHAR (MAX) NULL,
    [IsDistrict]          BIT            NOT NULL,
    [IsActive]            BIT            NOT NULL,
    [Version]             ROWVERSION     NOT NULL,
    CONSTRAINT [PK_Settlements]                PRIMARY KEY ([SettlementId]),
    CONSTRAINT [FK_Settlements_Municipalities] FOREIGN KEY ([MunicipalityId]) REFERENCES [dbo].Municipalities (MunicipalityId),
    CONSTRAINT [FK_Settlements_Districts]      FOREIGN KEY ([DistrictId])     REFERENCES [dbo].Districts      (DistrictId),
)
GO 

exec spDescTable  N'Settlements', N'Териториални единици.'
exec spDescColumn N'Settlements', N'SettlementId'         , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Settlements', N'MunicipalityId'       , N'Община.'
exec spDescColumn N'Settlements', N'DistrictId'           , N'Община.'
exec spDescColumn N'Settlements', N'Code'                 , N'Код.'
exec spDescColumn N'Settlements', N'MunicipalityCode'     , N'Код община.'
exec spDescColumn N'Settlements', N'DistrictCode'         , N'Код област.'
exec spDescColumn N'Settlements', N'MunicipalityCode2'    , N'Код община ГРАО.'
exec spDescColumn N'Settlements', N'DistrictCode2'        , N'Код област ГРАО.'
exec spDescColumn N'Settlements', N'Name'                 , N'Наименование.'
exec spDescColumn N'Settlements', N'TypeName'             , N'Наименование типа на териториалната единица.'
exec spDescColumn N'Settlements', N'TypeCode'             , N'Код на типа на териториалната единица.'
exec spDescColumn N'Settlements', N'MayoraltyCode'        , N'Код кметство.'
exec spDescColumn N'Settlements', N'Category'             , N'Код на категорията на териториалната единица.'
exec spDescColumn N'Settlements', N'Altitude'             , N'Надморска височина.'
exec spDescColumn N'Settlements', N'Alias'                , N'Символен идентификатор.'
exec spDescColumn N'Settlements', N'Description'          , N'Описание.'
exec spDescColumn N'Settlements', N'IsActive'             , N'Маркер за активност'
exec spDescColumn N'Settlements', N'Version'              , N'Версия.'
GO

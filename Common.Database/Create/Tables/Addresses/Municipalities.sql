PRINT 'Municipalities'
GO 

CREATE TABLE [dbo].[Municipalities] (
    [MunicipalityId]      INT            NOT NULL IDENTITY,
    [DistrictId]          INT            NOT NULL,
    [Code]                NVARCHAR (10)  NOT NULL,
    [Code2]               NVARCHAR (10)  NOT NULL,
    [MainSettlementCode]  NVARCHAR (10)  NULL,
    [Category]            NVARCHAR (50)  NULL,
    [Name]                NVARCHAR (200) NOT NULL,
    [Alias]               NVARCHAR (200) NULL,
    [Description]         NVARCHAR (MAX) NULL,
    [IsActive]            BIT            NOT NULL,
    [Version]             ROWVERSION     NOT NULL,
    CONSTRAINT [PK_Municipalities]           PRIMARY KEY ([MunicipalityId]),
    CONSTRAINT [FK_Municipalities_Districts] FOREIGN KEY (DistrictId) REFERENCES [dbo].[Districts] ([DistrictId]),
)
GO 

exec spDescTable  N'Municipalities', N'Номенклатура общини.'
exec spDescColumn N'Municipalities', N'MunicipalityId'       , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Municipalities', N'DistrictId'           , N'Област.'
exec spDescColumn N'Municipalities', N'Code'                 , N'Код.'
exec spDescColumn N'Municipalities', N'Code2'                , N'Код ГРАО.'
exec spDescColumn N'Municipalities', N'MainSettlementCode'   , N'Код на центъра.'
exec spDescColumn N'Municipalities', N'Category'             , N'Категория.'
exec spDescColumn N'Municipalities', N'Name'                 , N'Наименование.'
exec spDescColumn N'Municipalities', N'Alias'                , N'Символен идентификатор.'
exec spDescColumn N'Municipalities', N'Description'          , N'Описание.'
exec spDescColumn N'Municipalities', N'IsActive'             , N'Маркер за активност.'
exec spDescColumn N'Municipalities', N'Version'              , N'Версия.'
GO

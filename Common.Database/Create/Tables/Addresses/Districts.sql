PRINT 'Districts'
GO 

CREATE TABLE [dbo].[Districts] (
    [DistrictId]            INT            NOT NULL IDENTITY,
    [Code]                  NVARCHAR (10)  NOT NULL,
    [Code2]                 NVARCHAR (10)  NOT NULL,
    [SecondLevelRegionCode] NVARCHAR (10)  NULL,
    [Name]                  NVARCHAR (200) NOT NULL,
    [MainSettlementCode]    NVARCHAR (10)  NULL,
    [Alias]                 NVARCHAR (200) NULL,
    [Description]           NVARCHAR (MAX) NULL,
    [IsActive]              BIT            NOT NULL,
    [Version]               ROWVERSION     NOT NULL,
    CONSTRAINT [PK_Districts] PRIMARY KEY ([DistrictId]),
)
GO 

exec spDescTable  N'Districts', N'Номенклатура области'
exec spDescColumn N'Districts', N'DistrictId'           , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Districts', N'Code'                 , N'Код.'
exec spDescColumn N'Districts', N'Code2'                , N'Код ГРАО.'
exec spDescColumn N'Districts', N'SecondLevelRegionCode', N'Код на региона за планиране.'
exec spDescColumn N'Districts', N'Name'                 , N'Наименование.'
exec spDescColumn N'Districts', N'MainSettlementCode'   , N'Код на центъра.'
exec spDescColumn N'Districts', N'Alias'                , N'Символен идентификатор.'
exec spDescColumn N'Districts', N'Description'          , N'Описание.'
exec spDescColumn N'Districts', N'IsActive'             , N'Маркер за активност.'
exec spDescColumn N'Districts', N'Version'              , N'Версия.'
GO

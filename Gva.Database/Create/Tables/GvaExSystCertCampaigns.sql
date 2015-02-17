PRINT 'GvaExSystCertCampaigns'
GO 

CREATE TABLE [dbo].[GvaExSystCertCampaigns] (
    [Name]              NVARCHAR(200)  NOT NULL,
    [Code]              NVARCHAR(200)  NOT NULL UNIQUE,
    [ValidFrom]         DATETIME2      NULL,
    [ValidTo]           DATETIME2      NULL,
    [QualificationCode] NVARCHAR(200)  NOT NULL,
    CONSTRAINT [PK_vaExSystCertCampaigns] PRIMARY KEY ([Code])
)
GO

exec spDescTable  N'GvaExSystCertCampaigns', N'Сертификационни кампании от изпитната система.'
exec spDescColumn N'GvaExSystCertCampaigns', N'Name'                    , N'Наименование.'
exec spDescColumn N'GvaExSystCertCampaigns', N'Code'                    , N'Код.'
exec spDescColumn N'GvaExSystCertCampaigns', N'QualificationCode'       , N'Код на квалификация.'
exec spDescColumn N'GvaExSystCertCampaigns', N'ValidFrom'               , N'Дата на начало на валидност на кампанията.'
exec spDescColumn N'GvaExSystCertCampaigns', N'ValidTo'                 , N'Дата на край на валидност на кампанията.'
GO

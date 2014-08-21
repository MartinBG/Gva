PRINT 'GvaViewOrganizations'
GO 

CREATE TABLE [dbo].[GvaViewOrganizations] (
    [LotId]                INT           NOT NULL,
    [Name]                 NVARCHAR(100) NOT NULL,
    [NameAlt]              NVARCHAR(100) NOT NULL,
    [Cao]                  NVARCHAR(50)  NULL,
    [Valid]                BIT           NOT NULL,
    [OrganizationTypeId]   INT           NOT NULL,
    [Uin]                  NVARCHAR(50)  NULL,
    [DateValidTo]          DATETIME2     NULL,
    [DateCaoValidTo]       DATETIME2     NULL,
    CONSTRAINT [PK_GvaViewOrganizations]            PRIMARY KEY ([LotId]),
    CONSTRAINT [FK_GvaViewOrganizations_Lots]       FOREIGN KEY ([LotId])              REFERENCES [dbo].[Lots] ([LotId]),
    CONSTRAINT [FK_GvaViewOrganizations_NomValues]  FOREIGN KEY ([OrganizationTypeId]) REFERENCES [dbo].[NomValues] ([NomValueId])
)
GO

exec spDescTable  N'GvaViewOrganizations', N'Организации.'
exec spDescColumn N'GvaViewOrganizations', N'LotId'                   , N'Идентификатор на партида на организация.'
exec spDescColumn N'GvaViewOrganizations', N'Name'                    , N'Наменование.'
exec spDescColumn N'GvaViewOrganizations', N'NameAlt'                 , N'Наменование на поддържан език.'
exec spDescColumn N'GvaViewOrganizations', N'Cao'                     , N'CAO номер.'
exec spDescColumn N'GvaViewOrganizations', N'Valid'                   , N'Валидност.'
exec spDescColumn N'GvaViewOrganizations', N'Uin'                     , N'Булстат.'
exec spDescColumn N'GvaViewOrganizations', N'OrganizationTypeId'      , N'Тип организация.'
exec spDescColumn N'GvaViewOrganizations', N'DateValidTo'             , N'Валидност до.'
exec spDescColumn N'GvaViewOrganizations', N'DateCaoValidTo'          , N'САО - дата на валидност.'
GO

PRINT 'GvaViewOrganizations'
GO 

CREATE TABLE [dbo].[GvaViewOrganizations] (
    [LotId]                INT           NOT NULL,
    [Name]                 NVARCHAR(100) NOT NULL,
    [CAO]                  NVARCHAR(50)  NULL,
    [Valid]                NVARCHAR(50)  NOT NULL,
    [OrganizationType]     NVARCHAR(100)  NOT NULL,
    [Uin]                  NVARCHAR(50)  NULL,
    [DateValidTo]          DATETIME2(7)  NULL,
    [DateCAOValidTo]       DATETIME2(7)  NULL,
    CONSTRAINT [PK_GvaViewOrganizations]      PRIMARY KEY ([LotId]),
    CONSTRAINT [FK_GvaViewOrganizations_Lots]  FOREIGN KEY ([LotId]) REFERENCES [dbo].[Lots] ([LotId])
)
GO

exec spDescTable  N'GvaViewOrganizations', N'Организации.'
exec spDescColumn N'GvaViewOrganizations', N'LotId'                   , N'Идентификатор на партида на организация.'
exec spDescColumn N'GvaViewOrganizations', N'Name'                    , N'Наменование.'
exec spDescColumn N'GvaViewOrganizations', N'CAO'                     , N'CAO номер.'
exec spDescColumn N'GvaViewOrganizations', N'Valid'                   , N'Валидност.'
exec spDescColumn N'GvaViewOrganizations', N'Uin'                     , N'Булстат.'
exec spDescColumn N'GvaViewOrganizations', N'OrganizationType'        , N'Тип организация.'
exec spDescColumn N'GvaViewOrganizations', N'DateValidTo'             , N'Валидност до.'
exec spDescColumn N'GvaViewOrganizations', N'DateCAOValidTo'          , N'САО - дата на валидност.'
GO

print 'Correspondents'
GO 

CREATE TABLE Correspondents
(
    CorrespondentId	            INT             IDENTITY (1, 1) NOT NULL,
    CorrespondentGroupId	    INT             NOT NULL,
    RegisterIndexId             INT             NULL,
    Email                       NVARCHAR (200)  NULL,
    DisplayName  AS ([dbo].[fnGetCorrespondentDisplayName](CorrespondentTypeId, BgCitizenFirstName, BgCitizenLastName, BgCitizenUIN, ForeignerFirstName, ForeignerLastName, LegalEntityName, LegalEntityBulstat, FLegalEntityName)),
    CorrespondentTypeId	        INT	            NOT NULL,

    BgCitizenFirstName          NVARCHAR (200)  NULL,
    BgCitizenLastName           NVARCHAR (200)  NULL,
    BgCitizenUIN                NVARCHAR (50)   NULL,
    BgCitizenPosition           NVARCHAR (200)  NULL,
    ForeignerFirstName          NVARCHAR (200)  NULL,
    ForeignerLastName           NVARCHAR (200)  NULL,
    ForeignerCountryId          INT             NULL,
    ForeignerSettlement         NVARCHAR (50)   NULL,
    ForeignerBirthDate          DATETIME        NULL,
    
    LegalEntityName             NVARCHAR (200)  NULL,
    LegalEntityBulstat          NVARCHAR (50)   NULL,
    
    FLegalEntityName            NVARCHAR (200)  NULL,
    FLegalEntityCountryId       INT	            NULL,
    FLegalEntityRegisterName    NVARCHAR (50)   NULL,
    FLegalEntityRegisterNumber  NVARCHAR (50)   NULL,
    FLegalEntityOtherData       NVARCHAR (MAX)  NULL,
    
    ContactDistrictId           INT             NULL,
    ContactMunicipalityId       INT             NULL,
    ContactSettlementId         INT	            NULL,
    ContactPostCode             NVARCHAR (20)   NULL,
    ContactAddress              NVARCHAR (MAX)  NULL,
    ContactPostOfficeBox        NVARCHAR (100)  NULL,
    ContactPhone                NVARCHAR (100)  NULL,
    ContactFax                  NVARCHAR (100)  NULL,
    
    ModifyDate                  DATETIME        NULL,
    ModifyUserId                INT             NULL,
    Alias                       NVARCHAR (200)  NULL,
    IsActive                    BIT             NOT NULL,
    Version                     ROWVERSION      NOT NULL,
    CONSTRAINT PK_Correspondents                       PRIMARY KEY CLUSTERED (CorrespondentId),
    CONSTRAINT [FK_Correspondents_CountryId]           FOREIGN KEY ([ForeignerCountryId])    REFERENCES [dbo].[Countries] ([CountryId]),
    CONSTRAINT [FK_Correspondents_CountryId2]          FOREIGN KEY ([FLegalEntityCountryId]) REFERENCES [dbo].[Countries] ([CountryId]),
    CONSTRAINT [FK_Correspondents_District]            FOREIGN KEY ([ContactDistrictId])     REFERENCES [dbo].[Districts] ([DistrictId]),
    CONSTRAINT [FK_Correspondents_Municipalities]      FOREIGN KEY ([ContactMunicipalityId]) REFERENCES [dbo].[Municipalities] ([MunicipalityId]),
    CONSTRAINT [FK_Correspondents_Settlements]         FOREIGN KEY ([ContactSettlementId])   REFERENCES [dbo].[Settlements] ([SettlementId]),
    CONSTRAINT [FK_Correspondents_CorrespondentGroups] FOREIGN KEY ([CorrespondentGroupId])  REFERENCES [dbo].[CorrespondentGroups] ([CorrespondentGroupId]),
    CONSTRAINT [FK_Correspondents_CorrespondentTypes]  FOREIGN KEY ([CorrespondentTypeId])   REFERENCES [dbo].[CorrespondentTypes] ([CorrespondentTypeId]),
    CONSTRAINT [FK_Correspondents_RegisterIndexes]     FOREIGN KEY ([RegisterIndexId])       REFERENCES [dbo].[RegisterIndexes] ([RegisterIndexId]),
    CONSTRAINT [FK_Correspondents_Users]               FOREIGN KEY ([ModifyUserId])          REFERENCES [dbo].[Users] ([UserId])
)
GO 

exec spDescTable  N'Correspondents', N'Кореспонденти.'
exec spDescColumn N'Correspondents', N'CorrespondentId', N'Уникален системно генериран идентификатор.'

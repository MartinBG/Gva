print 'Persons'
GO

CREATE TABLE [dbo].[Persons] (
    [PersonId]           INT              NOT NULL IDENTITY(1,1),
    [Lin]                NVARCHAR (50)    NULL,
    [Egn]                NVARCHAR (50)    NULL,
    [FirstName]          NVARCHAR (50)    NULL,
    [MiddleName]         NVARCHAR (50)    NULL,
    [LastName]           NVARCHAR (50)    NULL,
    [FirstNameLatin]     NVARCHAR (50)    NULL,
    [MiddleNameLatin]    NVARCHAR (50)    NULL,
    [LastNameLatin]      NVARCHAR (50)    NULL,
    [DateOfBirth]        DATE             NULL,
    [PlaceOfBirthId]     INT              NULL,
    [CountryId]          INT              NULL,
    [GenderId]           INT              NULL,
    [Email]              NVARCHAR (50)    NULL,
    [Fax]                NVARCHAR (50)    NULL,
    [Phone1]             NVARCHAR (50)    NULL,
    [Phone2]             NVARCHAR (50)    NULL,
    [Phone3]             NVARCHAR (50)    NULL,
    [Phone4]             NVARCHAR (50)    NULL,
    [Phone5]             NVARCHAR (50)    NULL,
    [Version]            ROWVERSION       NOT NULL,
    CONSTRAINT [PK_Persons]             PRIMARY KEY ([PersonId]),
    CONSTRAINT [FK_Persons_Settlements] FOREIGN KEY ([PlaceOfBirthId]) REFERENCES [dbo].[Settlements] ([SettlementId]),
    CONSTRAINT [FK_Persons_Countries]   FOREIGN KEY ([CountryId])      REFERENCES [dbo].[Countries]   ([CountryId]),
    CONSTRAINT [FK_Persons_Genders]     FOREIGN KEY ([GenderId])       REFERENCES [dbo].[Genders]     ([GenderId])
);
GO

exec spDescTable  N'Persons'                    , N'Физически лица'
exec spDescColumn N'Persons', N'PersonId'       , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Persons', N'Lin'            , N'Личен идентификационен номер.'
exec spDescColumn N'Persons', N'Egn'            , N'Единен граждански номер.'
exec spDescColumn N'Persons', N'FirstName'      , N'Име.'
exec spDescColumn N'Persons', N'MiddleName'     , N'Презиме.'
exec spDescColumn N'Persons', N'LastName'       , N'Фамилия.'
exec spDescColumn N'Persons', N'FirstNameLatin' , N'Име на латински.'
exec spDescColumn N'Persons', N'MiddleNameLatin', N'Презиме на латински.'
exec spDescColumn N'Persons', N'LastNameLatin'  , N'Фамилия на латински.'
exec spDescColumn N'Persons', N'DateOfBirth'    , N'Дата на раждане.'
exec spDescColumn N'Persons', N'PlaceOfBirthId' , N'Място на раждане.'
exec spDescColumn N'Persons', N'CountryId'      , N'Държава.'
exec spDescColumn N'Persons', N'GenderId'       , N'Пол.'
exec spDescColumn N'Persons', N'Email'          , N'Email.'
exec spDescColumn N'Persons', N'Fax'            , N'Факс.'
exec spDescColumn N'Persons', N'Phone1'         , N'Телефон 1.'
exec spDescColumn N'Persons', N'Phone2'         , N'Телефон 2.'
exec spDescColumn N'Persons', N'Phone3'         , N'Телефон 3.'
exec spDescColumn N'Persons', N'Phone4'         , N'Телефон 4.'
exec spDescColumn N'Persons', N'Phone5'         , N'Телефон 5.'
exec spDescColumn N'Persons', N'Version'        , N'Име на ролята.'
GO

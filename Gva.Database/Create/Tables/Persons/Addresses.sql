print 'Addresses'
GO

CREATE TABLE [dbo].[Addresses] (
    [AddressId]       INT             NOT NULL IDENTITY(1,1),
    [PersonId]        INT             NOT NULL,
    [AddressTypeId]   INT             NULL,
    [SettlementId]    INT             NULL,
    [PostCode]        NVARCHAR (50)   NULL,
    [Address]         NVARCHAR (500)  NULL,
    [AddressLatin]    NVARCHAR (500)  NULL,
    [Phone]           NVARCHAR (50)   NULL,
    [IsValid]         BIT             NOT NULL,
    [Version]         ROWVERSION      NOT NULL,
    CONSTRAINT [PK_Addresses]              PRIMARY KEY ([AddressId]),
    CONSTRAINT [FK_Addresses_Persons]      FOREIGN KEY ([PersonId])      REFERENCES [dbo].[Persons]      ([PersonId]),
    CONSTRAINT [FK_Addresses_AddressTypes] FOREIGN KEY ([AddressTypeId]) REFERENCES [dbo].[AddressTypes] ([AddressTypeId]),
    CONSTRAINT [FK_Addresses_Settlements]  FOREIGN KEY ([SettlementId])  REFERENCES [dbo].[Settlements]  ([SettlementId])
);
GO

exec spDescTable  N'Addresses'                    , N'Адреси.'
exec spDescColumn N'Addresses', N'AddressId'      , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Addresses', N'PersonId'       , N'Физическо лице.'
exec spDescColumn N'Addresses', N'AddressTypeId'  , N'Тип адрес.'
exec spDescColumn N'Addresses', N'SettlementId'   , N'Потребител.'
exec spDescColumn N'Addresses', N'PostCode'       , N'Роля.'
exec spDescColumn N'Addresses', N'Address'        , N'Адрес.'
exec spDescColumn N'Addresses', N'AddressLatin'   , N'Адрес представен на друг език.'
exec spDescColumn N'Addresses', N'Phone'          , N'Телефон(и) за връзка на този адрес.'
exec spDescColumn N'Addresses', N'IsValid'        , N'Валидност.'
GO

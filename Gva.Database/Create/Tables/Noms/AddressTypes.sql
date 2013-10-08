print 'AddressTypes'
GO

CREATE TABLE [dbo].[AddressTypes] (
    [AddressTypeId]  INT             NOT NULL IDENTITY(1,1),
    [Code]           NVARCHAR (50)   NULL,
    [Name]           NVARCHAR (50)   NULL,
    [Version]        ROWVERSION      NOT NULL,
    CONSTRAINT [PK_AddressTypes] PRIMARY KEY ([AddressTypeId])
);
GO

exec spDescTable  N'AddressTypes'                   , N'Типове адреси'
exec spDescColumn N'AddressTypes', N'AddressTypeId' , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'AddressTypes', N'Code'          , N'Код.'
exec spDescColumn N'AddressTypes', N'Name'          , N'Наименование.'
GO

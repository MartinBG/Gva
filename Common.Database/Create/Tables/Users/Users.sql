PRINT 'Users'
GO

CREATE TABLE [dbo].[Users] (
    [UserId]                INT                 NOT NULL IDENTITY,
    [Username]              NVARCHAR (200)      NOT NULL UNIQUE,
    [PasswordHash]          NVARCHAR (200)      NULL,
    [PasswordSalt]          NVARCHAR (200)      NULL,
    [HasPassword]           BIT                 NOT NULL,
    [Fullname]              NVARCHAR (200)      NULL,
    [Notes]                 NVARCHAR (MAX)      NULL,
    [CertificateThumbprint] NVARCHAR (200)      NULL,
    [IsActive]              BIT                 NOT NULL,
    [Version]               ROWVERSION          NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([UserId]),
);
GO

exec spDescTable  N'Users', N'Потребители.'
exec spDescColumn N'Users', N'UserId'               , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Users', N'Username'             , N'Потребителско име.'
exec spDescColumn N'Users', N'PasswordHash'         , N'Криптирана парола.'
exec spDescColumn N'Users', N'PasswordSalt'         , N'SALT за криптираната парола.'
exec spDescColumn N'Users', N'HasPassword'          , N'Маркер за парола.'
exec spDescColumn N'Users', N'Fullname'             , N'Пълно име.'
exec spDescColumn N'Users', N'Notes'                , N'Бележки.'
exec spDescColumn N'Users', N'CertificateThumbprint', N'Сертификат на потребителя.'
exec spDescColumn N'Users', N'IsActive'             , N'Маркер за активност.'
exec spDescColumn N'Users', N'Version'              , N'Версия.'
GO

print 'UnitUsers'
GO

CREATE TABLE dbo.UnitUsers (
    UnitUserId          INT NOT NULL IDENTITY(1,1),
	UserId				INT NOT NULL,
    UnitId				INT NOT NULL,
    IsActive            BIT NOT NULL,
    Version             ROWVERSION NOT NULL,
    CONSTRAINT PK_UnitUsers PRIMARY KEY CLUSTERED (UnitUserId ASC),
	CONSTRAINT FK_Users_UnitUsers FOREIGN KEY (UserId) REFERENCES dbo.Users (UserId),
	CONSTRAINT FK_Units_UnitUsers FOREIGN KEY (UnitId) REFERENCES dbo.Units (UnitId)
);
GO

exec spDescTable  N'UnitUsers', N'Потребитеили за деловодната.'
exec spDescColumn N'UnitUsers', N'UnitUserId', N'Уникален системно генериран идентификатор.'
GO

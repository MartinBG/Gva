print 'RegisterIndexes'
GO 

CREATE TABLE RegisterIndexes
(
    RegisterIndexId	INT            IDENTITY (1, 1) NOT NULL,
    Name			NVARCHAR (200) NOT NULL,
	Alias           NVARCHAR (200) NOT NULL,
    Code			NVARCHAR (200) NOT NULL,
	NumberFormat	NVARCHAR (200) NOT NULL,
    IsActive		BIT            NOT NULL,
    Version			ROWVERSION     NOT NULL,
    CONSTRAINT PK_RegisterIndexes PRIMARY KEY CLUSTERED (RegisterIndexId)
)
GO 

exec spDescTable  N'RegisterIndexes', N'Регистърни индекси.'
exec spDescColumn N'RegisterIndexes', N'RegisterIndexId', N'Уникален системно генериран идентификатор.'

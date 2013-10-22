print 'DocFormatTypes'
GO 

CREATE TABLE DocFormatTypes
(
    DocFormatTypeId	INT            IDENTITY (1, 1) NOT NULL,
    Name			NVARCHAR (200) NOT NULL,
	Alias		NVARCHAR (200) NOT NULL,
    IsActive		BIT            NOT NULL,
    Version			ROWVERSION     NOT NULL,
    CONSTRAINT PK_DocFormatTypes PRIMARY KEY CLUSTERED (DocFormatTypeId),
)
GO 

exec spDescTable  N'DocFormatTypes', N'Типове документ.'
exec spDescColumn N'DocFormatTypes', N'DocFormatTypeId', N'Уникален системно генериран идентификатор.'

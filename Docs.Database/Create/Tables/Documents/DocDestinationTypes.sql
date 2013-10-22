print 'DocDestinationTypes'
GO 

CREATE TABLE DocDestinationTypes
(
    DocDestinationTypeId		INT            IDENTITY (1, 1) NOT NULL,
    Name				NVARCHAR (200) NOT NULL,
	Alias				NVARCHAR (200) NOT NULL,
    IsActive			BIT            NOT NULL,
    Version				ROWVERSION     NOT NULL,
    CONSTRAINT PK_DocDestinationTypes PRIMARY KEY CLUSTERED (DocDestinationTypeId),
)
GO 

exec spDescTable  N'DocDestinationTypes', N'Дестинация на документа, т.е. изпратено чрез'
exec spDescColumn N'DocDestinationTypes', N'DocDestinationTypeId', N'Уникален системно генериран идентификатор.'

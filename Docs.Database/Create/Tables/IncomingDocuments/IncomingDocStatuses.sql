print 'IncomingDocStatuses'
GO 

CREATE TABLE IncomingDocStatuses
(
    IncomingDocStatusId	INT	IDENTITY (1, 1)	NOT NULL,
    Name				NVARCHAR (200)		NOT NULL,
	Alias				NVARCHAR (200)		NOT NULL,
    Version				ROWVERSION			NOT NULL,
    CONSTRAINT PK_IncomingDocStatuses PRIMARY KEY CLUSTERED (IncomingDocStatusId),
)
GO 

exec spDescTable  N'IncomingDocStatuses', N'Статуси на входящи документи.'
exec spDescColumn N'IncomingDocStatuses', N'IncomingDocStatusId', N'Уникален системно генериран идентификатор.'

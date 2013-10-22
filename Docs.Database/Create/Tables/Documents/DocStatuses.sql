print 'DocStatuses'
GO 

CREATE TABLE DocStatuses
(
    DocStatusId		INT            IDENTITY (1, 1) NOT NULL,
    Name				NVARCHAR (200) NOT NULL,
	Alias				NVARCHAR (200) NOT NULL,
    IsActive			BIT            NOT NULL,
    Version				ROWVERSION     NOT NULL,
    CONSTRAINT PK_DocStatuses PRIMARY KEY CLUSTERED (DocStatusId),
)
GO 

exec spDescTable  N'DocStatuses', N'Статус на документ, обработен и др.'
exec spDescColumn N'DocStatuses', N'DocStatusId', N'Уникален системно генериран идентификатор.'
--'', 'CLOSED', 'COMPLETED', 'STARTED')

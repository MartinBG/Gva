print 'DocDirections'
GO 

CREATE TABLE DocDirections
(
    DocDirectionId		INT            NOT NULL IDENTITY (1, 1),
    Name				NVARCHAR (200) NOT NULL,
	Alias				NVARCHAR (200) NOT NULL,
    IsActive			BIT            NOT NULL,
    Version				ROWVERSION     NOT NULL,
    CONSTRAINT PK_DocDirections PRIMARY KEY CLUSTERED (DocDirectionId),
)
GO 

exec spDescTable  N'DocDirections', N'Посока на документ Входящ, Изходящ и т.н.'
exec spDescColumn N'DocDirections', N'DocDirectionId', N'Уникален системно генериран идентификатор.'
--'INTERNALOUTGOING', 'INTERNAL', 'OUTGOING', 'INCOMING', 'INTERORG'

print 'DocRegisters'
GO 

CREATE TABLE DocRegisters
(
    DocRegisterId	INT            IDENTITY (1, 1) NOT NULL,
	RegisterIndexId INT            NULL,
	Alias			NVARCHAR (200) NOT NULL,
	CurrentNumber	int			   NOT NULL,
    --Name			NVARCHAR (200) NOT NULL,
	--NumberFormat	NVARCHAR (200) NOT NULL,
	--RegisterIndex	NVARCHAR (200) NOT NULL,
    --IsActive		BIT            NOT NULL,
    Version			ROWVERSION     NOT NULL,
    CONSTRAINT PK_DocRegisters PRIMARY KEY CLUSTERED (DocRegisterId),
    CONSTRAINT [FK_DocRegisters_RegisterIndexes] FOREIGN KEY ([RegisterIndexId]) REFERENCES [dbo].[RegisterIndexes] ([RegisterIndexId]),
)
GO 

exec spDescTable  N'DocRegisters', N'Документни регистри.'
exec spDescColumn N'DocRegisters', N'DocRegisterId', N'Уникален системно генериран идентификатор.'

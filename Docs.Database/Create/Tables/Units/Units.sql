print 'Units'
GO

CREATE TABLE dbo.Units (
    UnitId                INT                 NOT NULL IDENTITY(1,1),
    Name				  NVARCHAR (200)      NOT NULL,
	UnitTypeId			  INT				  NOT NULL,
	InheritParentClassification	BIT			  NOT NULL DEFAULT (1), 
    IsActive              BIT                 NOT NULL,
    Version               ROWVERSION          NOT NULL,
    CONSTRAINT PK_Units PRIMARY KEY CLUSTERED (UnitId ASC),
	CONSTRAINT FK_UnitTypes_UnitTypes FOREIGN KEY (UnitTypeId) REFERENCES dbo.UnitTypes (UnitTypeId)
);
GO

exec spDescTable  N'Units', N'Звена.'
exec spDescColumn N'Units', N'UnitId', N'Уникален системно генериран идентификатор.'
GO

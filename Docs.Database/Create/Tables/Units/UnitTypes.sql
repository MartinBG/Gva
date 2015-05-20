print 'UnitTypes'
GO 

CREATE TABLE UnitTypes
(
    UnitTypeId	INT            IDENTITY (1, 1) NOT NULL,
    Name			NVARCHAR (200) NOT NULL,
	Alias			NVARCHAR (200) NOT NULL,
    IsActive		BIT            NOT NULL,
    Version			ROWVERSION     NOT NULL,
    CONSTRAINT PK_UnitTypes PRIMARY KEY CLUSTERED (UnitTypeId)
)
GO 

exec spDescTable  N'UnitTypes', N'Типове звена.'
exec spDescColumn N'UnitTypes', N'UnitTypeId', N'Уникален системно генериран идентификатор.'
--Административно звено, Служител
print 'UnitTokens'
GO 

CREATE TABLE UnitTokens
(
      UnitId int NOT NULL, ----fk
	  Token nvarchar(200) NOT NULL, 
	  CreateToken nvarchar(200) NOT NULL, 
	  DocUnitPermissionId int, ----fk
	  CONSTRAINT PK_UnitTokens PRIMARY KEY CLUSTERED (UnitId, Token, CreateToken, DocUnitPermissionId)
)
GO 

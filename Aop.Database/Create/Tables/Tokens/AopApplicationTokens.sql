print 'AopApplicationTokens'
GO 

CREATE TABLE AopApplicationTokens
(
	AopApplicationId int NOT NULL, --fk
	Token nvarchar (200) NOT NULL,
	CreateToken nvarchar(200) NOT NULL, 
	CONSTRAINT PK_AopApplicationTokens PRIMARY KEY CLUSTERED (AopApplicationId, Token, CreateToken)
)
GO 

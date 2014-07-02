print 'DocTokens'
GO 

CREATE TABLE DocTokens
(
	DocId int NOT NULL, --fk
	Token nvarchar (200) NOT NULL,
	CreateToken nvarchar(200) NOT NULL, 
	CONSTRAINT PK_DocTokens PRIMARY KEY CLUSTERED (DocId, Token, CreateToken)
)
GO 

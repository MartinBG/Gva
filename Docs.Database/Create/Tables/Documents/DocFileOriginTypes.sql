print 'DocFileOriginTypes'
GO 

CREATE TABLE DocFileOriginTypes
(
    DocFileOriginTypeId	INT            IDENTITY (1, 1) NOT NULL,
    Name			NVARCHAR (200) NOT NULL,
	Alias		NVARCHAR (200) NOT NULL,
    IsActive		BIT            NOT NULL,
    Version			ROWVERSION     NOT NULL,
    CONSTRAINT PK_DocFileOriginTypes PRIMARY KEY CLUSTERED (DocFileOriginTypeId),
)
GO 



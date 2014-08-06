print 'UnitTokens'
GO 

CREATE TABLE UnitTokens
(
    UnitId                      int           NOT NULL,
    Token                       nvarchar(200) NOT NULL,
    CreateToken                 nvarchar(200) NOT NULL,
    ClassificationPermissionId  int           NOT NULL,
    CONSTRAINT PK_UnitTokens                           PRIMARY KEY (UnitId, Token, CreateToken, ClassificationPermissionId),
    CONSTRAINT FK_UnitTokens_Units                     FOREIGN KEY (UnitId)                     REFERENCES dbo.Units (UnitId),
    CONSTRAINT FK_UnitTokens_ClassificationPermissions FOREIGN KEY (ClassificationPermissionId) REFERENCES dbo.ClassificationPermissions (ClassificationPermissionId)
)
GO 

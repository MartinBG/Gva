print 'DocUsers'
GO 

CREATE TABLE DocUsers
(
    DocId		    INT             NOT NULL,
	UnitId      	INT             NOT NULL,
    DocUnitPermissionId INT         NOT NULL,
    HasRead         BIT             NOT NULL,
    IsActive        BIT             NOT NULL,
    ActivateDate    Datetime        NULL,
    DeactivateDate    Datetime        NULL,
    CONSTRAINT PK_DocUsers PRIMARY KEY CLUSTERED (DocId, UnitId, DocUnitPermissionId),
	CONSTRAINT [FK_DocUsers_Docs] FOREIGN KEY ([DocId]) REFERENCES [dbo].[Docs] ([DocId]),
    CONSTRAINT [FK_DocUsers_Units] FOREIGN KEY ([UnitId]) REFERENCES [dbo].[Units] ([UnitId]),
    CONSTRAINT [FK_DocUsers_DocUnitPermissions] FOREIGN KEY ([DocUnitPermissionId]) REFERENCES [dbo].[DocUnitPermissions] (DocUnitPermissionId),
)
GO 

print 'DocTypeUnitRoles' 
GO 

CREATE TABLE DocTypeUnitRoles
(
    DocTypeUnitRoleId		    INT            IDENTITY (1, 1) NOT NULL,
	DocTypeId	            INT            NOT NULL,
    DocDirectionId	        INT            NOT NULL,
    DocUnitRoleId	        INT            NOT NULL,
    UnitId	                INT            NOT NULL,
    IsActive		        BIT            NOT NULL,
    Version			        ROWVERSION     NOT NULL,
    CONSTRAINT PK_DocTypeUnitRoles PRIMARY KEY CLUSTERED (DocTypeUnitRoleId),
	CONSTRAINT [FK_DocTypeUnitRoles_DocTypes] FOREIGN KEY ([DocTypeId]) REFERENCES [dbo].[DocTypes] ([DocTypeId]),
    CONSTRAINT [FK_DocTypeUnitRoles_DocDirections] FOREIGN KEY ([DocDirectionId]) REFERENCES [dbo].[DocDirections] ([DocDirectionId]),
    CONSTRAINT [FK_DocTypeUnitRoles_DocUnitRoles] FOREIGN KEY ([DocUnitRoleId]) REFERENCES [dbo].[DocUnitRoles] ([DocUnitRoleId]),
    CONSTRAINT [FK_DocTypeUnitRoles_Units] FOREIGN KEY ([UnitId]) REFERENCES [dbo].[Units] ([UnitId]),
)
GO 

exec spDescTable  N'DocTypeUnitRoles', N'Указания за автоматично попълване на полета на видове документи.'
exec spDescColumn N'DocTypeUnitRoles', N'DocTypeUnitRoleId', N'Уникален системно генериран идентификатор.'

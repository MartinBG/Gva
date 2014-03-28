print 'DocUnits'
GO 

CREATE TABLE DocUnits
(
    DocUnitId			INT				IDENTITY (1, 1) NOT NULL,
	DocId					INT				NOT NULL,
	UnitId				INT				NOT NULL,
	DocUnitRoleId				INT				NOT NULL,
	AddDate      datetime        NOT NULL,
    AddUserId    int             NOT NULL,
    Version					ROWVERSION     NOT NULL,
    CONSTRAINT PK_DocUnits PRIMARY KEY CLUSTERED (DocUnitId),
	CONSTRAINT [FK_DocUnits_Docs] FOREIGN KEY ([DocId]) REFERENCES [dbo].[Docs] ([DocId]),
	CONSTRAINT [FK_DocUnits_Units] FOREIGN KEY ([UnitId]) REFERENCES [dbo].[Units] ([UnitId]),
	CONSTRAINT [FK_DocUnits_DocUnitRoles] FOREIGN KEY ([DocUnitRoleId]) REFERENCES [dbo].[DocUnitRoles] ([DocUnitRoleId]),
	CONSTRAINT [FK_DocUnits_Users] FOREIGN KEY ([AddUserId]) REFERENCES [dbo].[Users] ([UserId])
)
GO 

exec spDescTable  N'DocUnits', N'Звена по документ.'
exec spDescColumn N'DocUnits', N'DocUnitId', N'Уникален системно генериран идентификатор.'

print 'UnitRelations'
GO 

CREATE TABLE UnitRelations
(
    UnitRelationId			INT				IDENTITY (1, 1) NOT NULL,
	UnitId					INT				NOT NULL,
	ParentUnitId			INT				NULL,
	RootUnitId				INT				NOT NULL,
    Version					ROWVERSION     NOT NULL,
    CONSTRAINT PK_UnitRelations PRIMARY KEY CLUSTERED (UnitRelationId),
	CONSTRAINT [FK_UnitRelations_Units] FOREIGN KEY ([UnitId]) REFERENCES [dbo].[Units] ([UnitId]),
	CONSTRAINT [FK_UnitRelations_UnitsParent] FOREIGN KEY ([ParentUnitId]) REFERENCES [dbo].[Units] ([UnitId]),
	CONSTRAINT [FK_UnitRelations_UnitsRoot] FOREIGN KEY ([RootUnitId]) REFERENCES [dbo].[Units] ([UnitId])
)
GO 

exec spDescTable  N'UnitRelations', N'Свързаност на звената.'
exec spDescColumn N'UnitRelations', N'UnitRelationId', N'Уникален системно генериран идентификатор.'

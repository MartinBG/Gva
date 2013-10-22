print 'IrregularityTypes'
GO 

CREATE TABLE IrregularityTypes
(
    IrregularityTypeId		INT            IDENTITY (1, 1) NOT NULL,
	DocTypeId		INT NOT NULL,
    Name			NVARCHAR (200) NOT NULL,
	Alias			NVARCHAR (200) NULL,
    Description			NVARCHAR (MAX) NULL,
    Version			ROWVERSION     NOT NULL,
    CONSTRAINT PK_IrregularityTypes PRIMARY KEY CLUSTERED (IrregularityTypeId),
	CONSTRAINT [FK_IrregularityTypes_DocTypes] FOREIGN KEY ([DocTypeId]) REFERENCES [dbo].[DocTypes] ([DocTypeId]),
)
GO 

exec spDescTable  N'IrregularityTypes', N'Типове нередности.'
exec spDescColumn N'IrregularityTypes', N'IrregularityTypeId', N'Уникален системно генериран идентификатор.'

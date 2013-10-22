print 'AssignmentTypes'
GO 

CREATE TABLE AssignmentTypes
(
    AssignmentTypeId	INT            IDENTITY (1, 1) NOT NULL,
    Name				NVARCHAR (200) NOT NULL,
	Alias				NVARCHAR (200) NOT NULL,
    IsActive			BIT            NOT NULL,
    Version				ROWVERSION     NOT NULL,
    CONSTRAINT PK_AssignmentTypes PRIMARY KEY CLUSTERED (AssignmentTypeId),
)
GO 

exec spDescTable  N'AssignmentTypes', N'Типове възлагане.'
exec spDescColumn N'AssignmentTypes', N'AssignmentTypeId', N'Уникален системно генериран идентификатор.'


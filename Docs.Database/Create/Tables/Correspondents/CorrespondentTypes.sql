print 'CorrespondentTypes'
GO 

CREATE TABLE CorrespondentTypes
(
    CorrespondentTypeId	INT            IDENTITY (1, 1) NOT NULL,
    Name			NVARCHAR (200) NOT NULL,
	Alias			NVARCHAR (200) NOT NULL,
    IsActive		BIT            NOT NULL,
    Version			ROWVERSION     NOT NULL,
    CONSTRAINT PK_CorrespondentTypes PRIMARY KEY CLUSTERED (CorrespondentTypeId)
)
GO 

exec spDescTable  N'CorrespondentTypes', N'Типове кореспонденти.'
exec spDescColumn N'CorrespondentTypes', N'CorrespondentTypeId', N'Уникален системно генериран идентификатор.'

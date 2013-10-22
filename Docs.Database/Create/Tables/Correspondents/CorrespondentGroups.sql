print 'CorrespondentGroups'
GO 

CREATE TABLE CorrespondentGroups
(
    CorrespondentGroupId	INT            IDENTITY (1, 1) NOT NULL,
    Name			NVARCHAR (200) NOT NULL,
	Alias			NVARCHAR (200) NOT NULL,
    IsActive		BIT            NOT NULL,
    Version			ROWVERSION     NOT NULL,
    CONSTRAINT PK_CorrespondentGroups PRIMARY KEY CLUSTERED (CorrespondentGroupId)
)
GO 

exec spDescTable  N'CorrespondentGroups', N'Групи кореспонденти.'
exec spDescColumn N'CorrespondentGroups', N'CorrespondentGroupId', N'Уникален системно генериран идентификатор.'

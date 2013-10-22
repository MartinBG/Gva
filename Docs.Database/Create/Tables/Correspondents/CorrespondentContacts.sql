print 'CorrespondentContacts'
GO 

CREATE TABLE CorrespondentContacts
(
    CorrespondentContactId			INT				IDENTITY (1, 1) NOT NULL,
	CorrespondentId	INT				NOT NULL,
	Name							NVARCHAR (200)	NOT NULL,
	UIN								NVARCHAR (50)	NULL,
	Note						NVARCHAR (MAX)	NULL,

    IsActive		BIT            NOT NULL,
    Version			ROWVERSION     NOT NULL,
    CONSTRAINT PK_CorrespondentContacts PRIMARY KEY CLUSTERED (CorrespondentContactId),
	CONSTRAINT [FK_CorrespondentContacts_Correspondents] FOREIGN KEY ([CorrespondentId]) REFERENCES [dbo].[Correspondents] ([CorrespondentId]),
)
GO 

exec spDescTable  N'CorrespondentContacts', N'Контакти на кореспонденти.'
exec spDescColumn N'CorrespondentContacts', N'CorrespondentContactId', N'Уникален системно генериран идентификатор.'
